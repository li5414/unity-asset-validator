/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Validators.Output;
using System;

namespace JCMG.AssetValidator.Editor.Validators
{
    /// <summary>
    /// The BaseValidatorManager class is used as the base class for any manager that is
    /// validating using types held in a ClassTypeCache by using VFieldValidators.
    /// </summary>
    public class BaseValidatorManager : IDisposable
    {
        protected readonly AssetValidatorLogger _logger;

        protected int _continousProgress;
        protected const int _continuousObjectsPerStep = 5;

        protected BaseValidatorManager(AssetValidatorLogger logger)
        {
            _logger = logger;
        }

        public virtual void Search()
        {
            throw new NotImplementedException();
        }

        public virtual void ValidateAll()
        {
            throw new NotImplementedException();
        }

        public virtual bool ContinueValidation()
        {
            throw new NotImplementedException();
        }

        public virtual bool IsComplete()
        {
            throw new NotImplementedException();
        }

        public virtual float GetProgress()
        {
            throw new NotImplementedException();
        }

        protected virtual void OnLogEvent(VLog vLog)
        {
            _logger.OnLogEvent(vLog);
        }

        #region IDisposable

        public virtual void Dispose() { }

        #endregion
    }
}