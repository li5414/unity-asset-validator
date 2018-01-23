/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Config;
using JCMG.AssetValidator.Editor.Utility;
using JCMG.AssetValidator.Editor.Validators;
using JCMG.AssetValidator.Editor.Validators.ProjectValidators;
using System.Collections.Generic;
using System.Linq;

namespace JCMG.AssetValidator.Editor.Meta
{
    /// <summary>
    /// ProjectValidatorCache is a wrapper around all validator types that exclusively target
    /// objects and assets in the project.
    /// </summary>
    public class ProjectValidatorCache
    {
        private List<BaseProjectValidator> _validators;

        public ProjectValidatorCache()
        {
            _validators = new List<BaseProjectValidator>();

            // Make sure any overriden disabled types are not included
            var overrideConfig = AssetValidatorOverrideConfig.FindOrCreate();

            // Get and add all field validators, excluding override disabled ones.
            var pvs = ReflectionUtility.GetAllDerivedInstancesOfTypeWithAttribute<BaseProjectValidator, ValidatorTargetAttribute>().ToArray();
            for (var index = 0; index < pvs.Length; index++)
            {
                AssetValidatorOverrideConfig.OverrideItem item = null;
                var projectValidator = pvs[index];
                if (overrideConfig.TryGetOverrideConfigItem(projectValidator.GetType(), out item))
                {
                    if (item.enabled)
                        _validators.Add(projectValidator);
                }
                else
                    _validators.Add(projectValidator);
            }
        }

        public int Count
        {
            get { return _validators.Count; }
        }

        public BaseProjectValidator this[int index]
        {
            get { return _validators[index]; }
        }
    }
}