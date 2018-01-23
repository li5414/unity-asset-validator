/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Utility;
using JCMG.AssetValidator.Editor.Validators.Output;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace JCMG.AssetValidator.Editor.Validators.ProjectValidators
{
    [ValidatorTarget("project_asset_bundle_path_validator")]
    [ValidatorDescription("Searches the project for all subclasses of AssetBundlePathContract and ensures that there is a file " +
                          "present at all bundle and bundle item paths acquired using the GetPath method on those contracts.")]
    [ValidatorExample(@"
public enum HeroClass
{
    Warrior,
    Rogue,
    Sorceror
}

/// <summary>
/// This example uses static data (in this case an enum of hero class types) to determine 
/// which icons should be in a particular bundle and which name they are expected to be in. 
/// If they are not present, a validation error will be created with the details of the file
/// it could not find.
/// </summary>
public class HeroClassAssetBundleContract : AssetBundlePathContract
{
    public override Dictionary<string, List<string>> GetPaths()
    {
        var heroValues = (HeroClass[]) Enum.GetValues(typeof(HeroClass));
        var dict = new Dictionary<string, List<string>>();
        var bundleName = ""hero_icon"";
        dict.Add(bundleName, new List<string>());

        foreach (var heroValue in heroValues)
            dict[bundleName].Add(string.Format(""{0}_icon.png"", heroValue.ToString().ToLower()));

        return dict;
    }
}
")]
    public class AssetBundlePathProjectValidator : BaseProjectValidator
    {
        private Dictionary<string, List<string>> AssetBundleValidationCache { get; set; }

        public AssetBundlePathProjectValidator()
        {
            AssetBundleValidationCache = new Dictionary<string, List<string>>();
        }

        public override int GetNumberOfResults()
        {
            return AssetBundleValidationCache.Sum(x => x.Value.Count);
        }

        public override void Search()
        {
            if (AssetBundleValidationCache.Count > 0) return;

            var contracts = ReflectionUtility.GetAllDerivedInstancesOfType<AssetBundlePathContract>();

            foreach (var contract in contracts)
            {
                var dict = contract.GetPaths();

                foreach (var kvp in dict)
                {
                    if (AssetBundleValidationCache.ContainsKey(kvp.Key))
                    {
                        // TODO Iterate through the existing bundle contents and add any bundle items not present

                    }
                    else
                        AssetBundleValidationCache.Add(kvp.Key, kvp.Value);
                }
            }
        }

        public override bool Validate()
        {
            var allPathsValidated = true;
            foreach (var assetBundle in AssetBundleValidationCache)
            {
                var validatedassetBundleName = assetBundle.Key;
                var validatedAssetBundleContents = assetBundle.Value;
                var assetBundleContents = new List<string>(AssetDatabase.GetAssetPathsFromAssetBundle(validatedassetBundleName));

                FileUtility.ReduceAssetPathsToFileNames(assetBundleContents);

                for (var i = 0; i < validatedAssetBundleContents.Count; i++)
                {
                    if (assetBundleContents.Contains(validatedAssetBundleContents[i])) continue;

                    allPathsValidated = false;
                    DispatchVLogEvent(null, VLogType.Error,
                        string.Format("Could not find asset for bundle [{0}] with asset name of [{1}].", 
                        validatedassetBundleName, validatedAssetBundleContents[i]));
                }
            }

            return allPathsValidated;
        }
    }
}
