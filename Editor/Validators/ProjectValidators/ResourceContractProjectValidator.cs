﻿/*
AssetValidator 
Copyright (c) 2018 Jeff Campbell

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
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
