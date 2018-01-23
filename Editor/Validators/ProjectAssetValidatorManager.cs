/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Meta;
using JCMG.AssetValidator.Editor.Validators.Output;
using UnityEditor;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Validators
{
    public class ProjectAssetValidatorManager : BaseInstanceValidatorManager
    {
        private readonly string[] _allPrefabGUIDs;
        private int _continueSearchProgress;
        private int _projectSearchProgress;
        private int _projectValidationProgress;
        private bool _hasSearchedProjectValidatorCache;
        private bool _hasValidatedUsingProjectValidatorCache;
        private ProjectValidatorCache _projectValidatorCache;

        public ProjectAssetValidatorManager(ClassTypeCache cache, AssetValidatorLogger logger) 
            : base(cache, logger)
        {
            _continueSearchProgress = 0;
            _allPrefabGUIDs = AssetDatabase.FindAssets("t:Prefab");
        }

        protected override void InitValidatorLookup()
        {
            base.InitValidatorLookup();

            _projectValidatorCache = new ProjectValidatorCache();
            for (var i = 0; i < _projectValidatorCache.Count; i++)
                _projectValidatorCache[i].OnLogEvent += OnLogEvent;
        }

        public override void Search()
        {
            _objectsToValidate.Clear();

            // Get all prefab locations for instance validators
            for (var i = 0; i < _allPrefabGUIDs.Length; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(_allPrefabGUIDs[i]);
                var prefabObject = AssetDatabase.LoadMainAssetAtPath(assetPath);

                // 1. Test as Gameobject for components
                var prefabGameObject = prefabObject as GameObject;
                if (prefabGameObject == null) continue;

                for (var j = 0; j < _cache.Count; j++)
                {
                    var components = prefabGameObject.GetComponentsInChildren(_cache[j]);
                    if (components.Length == 0) continue;

                    for (var h = 0; h < components.Length; h++)
                        _objectsToValidate.Add(components[h]);
                }
            }

            // Map all project asset validators and Search 
            for (var i = 0; i < _projectValidatorCache.Count; i++)
                _projectValidatorCache[i].Search();

            _hasSearchedProjectValidatorCache = true;
        }

        public bool ContinueSearch()
        {
            var nextStep = _continueSearchProgress + _continuousObjectsPerStep >= _allPrefabGUIDs.Length
                ? _allPrefabGUIDs.Length
                : _continueSearchProgress + _continuousObjectsPerStep;

            for (; _continueSearchProgress < nextStep; _continueSearchProgress++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(_allPrefabGUIDs[_continueSearchProgress]);
                var prefabObject = AssetDatabase.LoadMainAssetAtPath(assetPath);

                // 1. Test as Gameobject for components
                var prefabGameObject = prefabObject as GameObject;
                if (prefabGameObject == null) continue;

                for (var j = 0; j < _cache.Count; j++)
                {
                    var components = prefabGameObject.GetComponentsInChildren(_cache[j]);
                    if (components.Length == 0) continue;

                    for (var h = 0; h < components.Length; h++)
                        _objectsToValidate.Add(components[h]);
                }
            }

            // Iterate one at a time through all project asset validators and validate
            nextStep = _projectSearchProgress + 1 >= _projectValidatorCache.Count
                ? _projectValidatorCache.Count
                : _projectSearchProgress + 1;
            for (; _projectSearchProgress < nextStep; _projectSearchProgress++)
                _projectValidatorCache[_projectSearchProgress].Search();

            _hasSearchedProjectValidatorCache = _projectSearchProgress >= _projectValidatorCache.Count;

            // Return false once we've run searching through all prefabs and project validators
            if (_continueSearchProgress < _allPrefabGUIDs.Length)
                return false;

            if (!_hasSearchedProjectValidatorCache)
                return false;

            return true;
        }

        public float GetSearchProgress()
        {
            return Mathf.Clamp01((_continueSearchProgress + _projectSearchProgress) / 
                                 ((float)_allPrefabGUIDs.Length + _projectValidatorCache.Count));
        }

        public bool IsSearchComplete()
        {
            return GetSearchProgress() >= 1f && _hasSearchedProjectValidatorCache;
        }

        public override void ValidateAll()
        {
            base.ValidateAll();

            // Map all project asset validators and Search 
            for (var i = 0; i < _projectValidatorCache.Count; i++)
                _projectValidatorCache[i].Validate();

            _hasValidatedUsingProjectValidatorCache = true;
        }

        public override bool ContinueValidation()
        {
            // Iterate one at a time through all project asset validators and validate
            var nextStep = _projectValidationProgress + 1 >= _projectValidatorCache.Count
                ? _projectValidatorCache.Count
                : _projectValidationProgress + 1;
            for (; _projectValidationProgress < nextStep; _projectValidationProgress++)
                _projectValidatorCache[_projectValidationProgress].Validate();

            _hasValidatedUsingProjectValidatorCache = _projectValidationProgress >= _projectValidatorCache.Count;

            // Return true once we've run through all prefabs and project validators
            return base.ContinueValidation() || _projectValidationProgress < _projectValidatorCache.Count;
        }

        public override float GetProgress()
        {
            return (_continousProgress + _projectValidationProgress) /
                   ((float) _objectsToValidate.Count + _projectValidatorCache.Count);
        }

        public override bool IsComplete()
        {
            return base.IsComplete() && _hasValidatedUsingProjectValidatorCache;
        }

        protected override void OnLogEvent(VLog vLog)
        {
            vLog.source = VLogSource.Project;

            base.OnLogEvent(vLog);
        }

        #region IDisposable

        public sealed override void Dispose()
        {
            for (var i = 0; i < _projectValidatorCache.Count; i++)
                _projectValidatorCache[i].OnLogEvent -= OnLogEvent;

            base.Dispose();
        }

        #endregion
    }
}
