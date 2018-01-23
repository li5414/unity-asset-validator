﻿/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;
using System.Collections.Generic;
using JCMG.AssetValidator.Editor.Utility;
using UnityEditor;
using Object = UnityEngine.Object;

namespace JCMG.AssetValidator.Editor.Validators.ProjectValidators
{
    [ValidatorDescription("AssetProjectValidator should be derived from when there are specific file types " +
                          "or extensions in the project that need to be validated.")]
    [ValidatorExample(@"
/// <summary>
/// This is an example of an AssetProjectValidator that targets all files with a file extension of ""unity"". 
/// When this validator is fired, it will find all files of that extension and attempt to load them using 
/// AssetDatabase.Load and pass the loaded object and the path to ValidateObject to see whether or not
/// the asset passes validation or not. As validators of any type are an editor only feature, they 
/// must be placed into an Editor folder.
/// </summary>
[ValidatorTarget(""test_project_asset_ext_validator"")]
public class NoWhiteSpaceInSceneNamesAssetProjectValidator : AssetProjectValidator
{
    protected override string[] GetApplicableFileExtensions()
    {
        return new[] {""unity""};
    }

    protected override bool ValidateObject(Object assetObj, string path)
    {
        var sceneName = path.Split('/').Last();

        if (!sceneName.Contains("" "")) return true;

        DispatchVLogEvent(assetObj, 
                          VLogType.Error, 
                          string.Format(""Scene [{0}] should not have any whitespace in its name"", 
                                        sceneName));

        return false;
    }
}
")]
    public class AssetProjectValidator : BaseProjectValidator
    {
        protected List<string> _assetPaths;
        protected bool _successfullyValidated;

        public AssetProjectValidator()
        {
            _assetPaths = new List<string>();
            _successfullyValidated = true;
        }

        public override int GetNumberOfResults()
        {
            return _assetPaths.Count;
        }

        public override void Search()
        {
            var extensions = GetApplicableFileExtensions();

            _assetPaths.AddRange(FileUtility.GetUnityFilePaths(extensions));
        }

        /// <summary>
        /// Override this in subclasses of AssetProjectValidator to return an array of file extensions with
        /// or without a period at the beginning. All files of that extension will be loaded one by one using
        /// AssetDatabase.LoadAssetAtPath and passed to ValidateObject in order to validate. 
        /// </summary>
        /// <returns></returns>
        protected virtual string[] GetApplicableFileExtensions()
        {
            throw new NotImplementedException();
        }

        public override bool Validate()
        {
            for (var i = 0; i < _assetPaths.Count; i++)
            {
                var obj = AssetDatabase.LoadAssetAtPath<Object>(_assetPaths[i]);
                _successfullyValidated &= ValidateObject(obj, _assetPaths[i]);
            }

            return _successfullyValidated;
        }

        /// <summary>
        /// Returns true on successful validation of the object, otherwise false
        /// </summary>
        /// <param name="assetObj">The object loaded by the AssetDatabase at this path.</param>
        /// <param name="path">The unity path at which this asset can be found.</param>
        /// <returns></returns>
        protected virtual bool ValidateObject(Object assetObj, string path)
        {
            throw new NotImplementedException();
        }
    }
}
