/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Meta;
using JCMG.AssetValidator.Editor.Validators.Output;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Validators
{
    public class CrossSceneValidatorManager : BaseValidatorManager
    {
        private readonly CrossSceneValidatorCache _crossSceneValidatorCache;

        public CrossSceneValidatorManager(AssetValidatorLogger logger) 
            : base(logger)
        {
            _crossSceneValidatorCache = new CrossSceneValidatorCache();

            for (var i = 0; i < _crossSceneValidatorCache.Count; i++)
                _crossSceneValidatorCache[i].OnLogEvent += _logger.OnLogEvent;
        }

        public override bool IsComplete()
        {
            return _continousProgress >= _crossSceneValidatorCache.Count;
        }

        public override float GetProgress()
        {
            return Mathf.Clamp01(_continousProgress / (float)_crossSceneValidatorCache.Count);
        }

        public override void Search()
        {
            for (var i = 0; i < _crossSceneValidatorCache.Count; i++)
                _crossSceneValidatorCache[i].Search();
        }

        public override void ValidateAll()
        {
            for (; _continousProgress < _crossSceneValidatorCache.Count; _continousProgress++)
                _crossSceneValidatorCache[_continousProgress].Validate();
        }

        public override bool ContinueValidation()
        {
            if (_continousProgress >= _crossSceneValidatorCache.Count) return false;

            var nextStep = _continousProgress + _continuousObjectsPerStep >= _crossSceneValidatorCache.Count
                ? _crossSceneValidatorCache.Count
                : _continousProgress + _continuousObjectsPerStep;

            for (; _continousProgress < nextStep; _continousProgress++)
                _crossSceneValidatorCache[_continousProgress].Validate();

            return _continousProgress < _crossSceneValidatorCache.Count;
        }

        #region IDisposable

        public sealed override void Dispose()
        {
            for (var i = 0; i < _crossSceneValidatorCache.Count; i++)
                _crossSceneValidatorCache[i].OnLogEvent -= _logger.OnLogEvent;

            base.Dispose();
        }

        #endregion
    }
}
