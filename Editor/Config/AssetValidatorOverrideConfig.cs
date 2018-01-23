/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Meta;
using JCMG.AssetValidator.Editor.Validators;
using JCMG.AssetValidator.Editor.Validators.CrossSceneValidators;
using JCMG.AssetValidator.Editor.Validators.FieldValidators;
using JCMG.AssetValidator.Editor.Validators.ObjectValidators;
using JCMG.AssetValidator.Editor.Validators.Output;
using JCMG.AssetValidator.Editor.Validators.ProjectValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Config
{
    public class AssetValidatorOverrideConfig : ScriptableObject
    {
        public List<OverrideItem> OverrideItems = new List<OverrideItem>();

        private const string ASSET_PATH = "ValidatorOverrideConfig";

        [Serializable]
        public class OverrideItem
        {
            public bool enabled;
            public string symbol;

            [NonSerialized]
            public Type type;

            public override string ToString()
            {
                return string.Format("Validator with Symbol [{0}] is Enabled: [{1}]", symbol, enabled);
            }
        }

        private static AssetValidatorOverrideConfig _config;
        public static AssetValidatorOverrideConfig FindOrCreate()
        {
            if(_config == null)
                _config = Resources.Load<AssetValidatorOverrideConfig>(ASSET_PATH);

            if(_config != null)
            {
                _config.FindAndAddMissingTypes();
                return _config;
            }

            Debug.LogWarningFormat("Could not find an AssetValidatorOverrideConfig named [{0}] at the root of a Resources folder. " +
                                   "Overriding will be ignored in its absence.", ASSET_PATH);

            return CreateInstance<AssetValidatorOverrideConfig>();            
        }

        private static AssetValidatorOverrideConfig CreateAsset()
        {
            var asset = CreateInstance(typeof(AssetValidatorOverrideConfig));
            AssetDatabase.CreateAsset(asset, ASSET_PATH);
            AssetDatabase.SaveAssets();

            return asset as AssetValidatorOverrideConfig;
        }

        public void FindAndAddMissingTypes()
        {
            var classCache = new ClassTypeCache();
            classCache.IgnoreAttribute<OnlyIncludeInTestsAttribute>();
            classCache.AddTypeWithAttribute<BaseFieldValidator, ValidatorTargetAttribute>();
            classCache.AddTypeWithAttribute<BaseObjectValidator, ValidatorTargetAttribute>();
            classCache.AddTypeWithAttribute<BaseCrossSceneValidator, ValidatorTargetAttribute>();
            classCache.AddTypeWithAttribute<BaseProjectValidator, ValidatorTargetAttribute>();

            var validatorTargets = classCache.Types.Select(x =>
            {
                var vValidatorAttr = (ValidatorTargetAttribute)x.GetCustomAttributes(typeof(ValidatorTargetAttribute), false)[0];
                return vValidatorAttr;
            }).ToArray();

            if (OverrideItems == null)
                OverrideItems = new List<OverrideItem>();

            // Remove any missing override items that no longer exist
            for (var i = OverrideItems.Count - 1; i > 0; i--)
            {
                if (validatorTargets.Any(x => x.Symbol == OverrideItems[i].symbol)) continue;

                OverrideItems.Remove(OverrideItems[i]);
            }

            for (var i = 0; i < validatorTargets.Length; i++)
            {
                var vValidatorAttr = validatorTargets[i];

                // If we have never cached this type before, create a reference to it by way of symbol
                // Otherwise grab the existing reference and reassign the type.
                if (OverrideItems.All(x => x.symbol != vValidatorAttr.Symbol))
                {
                    var oItem = new OverrideItem()
                    {
                        enabled = true,
                        symbol = vValidatorAttr.Symbol,
                        type = classCache[i]
                    };
                    OverrideItems.Add(oItem);
                }
                else
                {
                    var overrideItem = OverrideItems.First(x => x.symbol == vValidatorAttr.Symbol);
                    overrideItem.type = classCache[i];
                }
            }
        }

        public bool TryGetOverrideConfigItem(Type type, out OverrideItem overrideItem)
        {
            overrideItem = null;

            for (var i = 0; i < OverrideItems.Count; i++)
                if (OverrideItems[i].type == type)
                {
                    overrideItem = OverrideItems[i];
                    return true;
                }

            return false;
        }

        public void AddDisabledLogs(AssetValidatorLogger logger)
        {
            for (var i = 0; i < OverrideItems.Count; i++)
            {
                if (OverrideItems[i].enabled) continue;

                logger.OnLogEvent(new VLog()
                {
                    source = VLogSource.None,
                    vLogType = VLogType.Warning,
                    message = string.Format("Validator of type [{0}] is disabled in the AssetValidatorOverrideConfig at [{1}]",
                                            OverrideItems[i].type.Name, AssetDatabase.GetAssetPath(this))
                });
            }
        }
    }
}
