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
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Validators.ProjectValidators
{
    [ValidatorTarget("project_resource_path_validator")]
    [ValidatorDescription("Searches the project for all subclasses of ResourcePathContract and ensures that the accumulated " +
                          "paths result in a non-null object when loaded using Resources.Load")]
    [ValidatorExample(@"
public enum HeroClass
{
    Warrior,
    Rogue,
    Sorceror
}

/// <summary>
/// This example uses static data (in this case an enum of hero class types) to determine 
/// which icons should be able to be loaded at a particular resources folder. If they are 
/// not present, a validation error will be created with the details of the file it could not find.
/// </summary>
public class HeroClassResourcePathContract : ResourcePathContract
{
    private const string HERO_ICON_FOLDER = ""hero_icon"";

    public override IEnumerable<string> GetPaths()
    {
        var heroValues = (HeroClass[])Enum.GetValues(typeof(HeroClass));
        var list = new List<string>();

        foreach (var heroValue in heroValues)
            list.Add(string.Format(""{0}/{1}_icon"", HERO_ICON_FOLDER, heroValue.ToString().ToLower()));

        return list;
    }
}
")]
    public class ResourceContractProjectValidator : BaseProjectValidator
    {
        private List<string> ResourcePaths { get; set; }

        public ResourceContractProjectValidator()
        {
            ResourcePaths = new List<string>();
        }

        public override int GetNumberOfResults()
        {
            return ResourcePaths.Count;
        }

        public override void Search()
        {
            if (ResourcePaths.Count > 0) return;

            var contracts = ReflectionUtility.GetAllDerivedInstancesOfType<ResourcePathContract>();

            foreach (var contract in contracts)
                ResourcePaths.AddRange(contract.GetPaths());
        }

        public override bool Validate()
        {
            var allPathsValidated = true;
            foreach (var resourcePath in ResourcePaths)
            {
                var rObj = Resources.Load(resourcePath);
                if (rObj != null) continue;

                allPathsValidated = false;
                DispatchVLogEvent(null, VLogType.Error, 
                    string.Format("Could not find object at Resources path [{0}].", resourcePath));
            }

            return allPathsValidated;
        }
    }
}
