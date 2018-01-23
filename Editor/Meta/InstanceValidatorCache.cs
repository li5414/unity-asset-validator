/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Utility;
using JCMG.AssetValidator.Editor.Validators;
using JCMG.AssetValidator.Editor.Validators.FieldValidators;
using JCMG.AssetValidator.Editor.Validators.ObjectValidators;
using System.Collections.Generic;
using System.Linq;
using JCMG.AssetValidator.Editor.Config;

namespace JCMG.AssetValidator.Editor.Meta
{
    /// <summary>
    /// InstanceValidatorCache is a wrapper around all validator types that target individual
    /// class or object instances.
    /// </summary>
    public class InstanceValidatorCache
    {
        private readonly List<AbstractInstanceValidator> _validators;
        
        public InstanceValidatorCache()
        {
            _validators = new List<AbstractInstanceValidator>();

            // Make sure any overriden disabled types are not included
            var overrideConfig = AssetValidatorOverrideConfig.FindOrCreate();

            // Get and add all field validators, excluding override disabled ones.
            var fieldValidators = ReflectionUtility.GetAllDerivedInstancesOfType<BaseFieldValidator>().ToArray();
            for (var index = 0; index < fieldValidators.Length; index++)
            {
                AssetValidatorOverrideConfig.OverrideItem item = null;
                var vFieldValidator = fieldValidators[index];
                if (overrideConfig.TryGetOverrideConfigItem(vFieldValidator.GetType(), out item))
                {
                    if(item.enabled)
                        _validators.Add(vFieldValidator);
                }
                else
                    _validators.Add(vFieldValidator);
            }

            // Get and add all object validators, excluding override disabled ones.
            var objectValidators = ReflectionUtility.GetAllDerivedInstancesOfType<BaseObjectValidator>().ToArray();
            for (var index = 0; index < objectValidators.Length; index++)
            {
                AssetValidatorOverrideConfig.OverrideItem item = null;
                var vObjectValidator = objectValidators[index];
                if (overrideConfig.TryGetOverrideConfigItem(vObjectValidator.GetType(), out item))
                {
                    if (item.enabled)
                        _validators.Add(vObjectValidator);
                }
                else
                    _validators.Add(vObjectValidator);
            }
        }

        public int Count
        {
            get { return _validators.Count; }
        }

        public AbstractInstanceValidator this[int index]
        {
            get { return _validators[index]; }
        }
    }
}
