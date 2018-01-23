/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Config;
using JCMG.AssetValidator.Editor.Utility;
using JCMG.AssetValidator.Editor.Validators.CrossSceneValidators;
using System.Collections.Generic;
using System.Linq;

namespace JCMG.AssetValidator.Editor.Meta
{
    public class CrossSceneValidatorCache
    {
        private readonly List<BaseCrossSceneValidator> _validators;

        public CrossSceneValidatorCache()
        {
            var validators = ReflectionUtility.GetAllDerivedInstancesOfType<BaseCrossSceneValidator>().ToArray();
            _validators = new List<BaseCrossSceneValidator>();

            // Make sure any overriden disabled types are not included
            var overrideConfig = AssetValidatorOverrideConfig.FindOrCreate();
            for (var index = 0; index < validators.Length; index++)
            {
                AssetValidatorOverrideConfig.OverrideItem item = null;
                var baseCrossSceneValidator = validators[index];
                if (overrideConfig.TryGetOverrideConfigItem(baseCrossSceneValidator.GetType(), out item))
                {
                    if (item.enabled)
                        _validators.Add(baseCrossSceneValidator);
                }
                else
                    _validators.Add(baseCrossSceneValidator);
            }
        }

        public int Count
        {
            get { return _validators.Count; }
        }

        public BaseCrossSceneValidator this[int index]
        {
            get { return _validators[index]; }
        }
    }
}
