/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Meta;
using JCMG.AssetValidator.Editor.Validators.Output;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Validators
{
    /// <summary>
    /// A ActiveSceneValidator inspects components in a single scene whose type 
    /// is contained in the passed ClassTypeCache
    /// </summary>
    public class ActiveSceneValidatorManager : BaseInstanceValidatorManager
    {
        public ActiveSceneValidatorManager(ClassTypeCache cache, AssetValidatorLogger logger) 
            : base(cache, logger)
        {
        }

        public override void Search()
        {
            _objectsToValidate.Clear();
            for (var i = 0; i < _cache.Count; i++)
                _objectsToValidate.AddRange(Object.FindObjectsOfType(_cache[i]));
        }

        protected override void OnLogEvent(VLog vLog)
        {
            vLog.scenePath = EditorSceneManager.GetActiveScene().path;
            vLog.source = VLogSource.Scene;

            base.OnLogEvent(vLog);
        }
    }
}
