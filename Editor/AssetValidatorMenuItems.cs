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
using JCMG.AssetValidator.Editor.Validators.Output;
using JCMG.AssetValidator.Editor.Validators;
using JCMG.AssetValidator.Editor.Window;
using UnityEditor;

namespace JCMG.AssetValidator.Editor
{
    public static class AssetValidatorMenuItems
    {
        [MenuItem("Tools/AssetValidator/Validate Project Assets", false, priority = 11)]
        public static void ValidateAllAssetsInAssetFolder()
        {
            AssetValidatorEditorWindow.LaunchWindowWithValidation(SceneValidationMode.None, 
                                                                  OutputFormat.None,
                                                                  doValidateProjectAssets:true,
                                                                  doValidateAcrossScenes:false);
        }

        [MenuItem("Tools/AssetValidator/Validate Active Scene", priority = 12)]
        public static void ValidateAllAssetsInActiveScene()
        {
            AssetValidatorEditorWindow.LaunchWindowWithValidation(SceneValidationMode.ActiveScene,
                                                                  OutputFormat.None,
                                                                  doValidateProjectAssets: false,
                                                                  doValidateAcrossScenes: true);
        }

        [MenuItem("Tools/AssetValidator/Validate All Scenes", priority = 15)]
        public static void ValidateAllAssetsInAllScenes()
        {
            AssetValidatorEditorWindow.LaunchWindowWithValidation(SceneValidationMode.AllScenes,
                                                                  OutputFormat.None,
                                                                  doValidateProjectAssets: false,
                                                                  doValidateAcrossScenes: true);
        }

        [MenuItem("Tools/AssetValidator/Validate All Scenes in Build Settings", priority = 13)]
        public static void ValidateAllAssetsInAllScenesInBuildSettings()
        {
            AssetValidatorEditorWindow.LaunchWindowWithValidation(SceneValidationMode.AllBuildScenes,
                                                                  OutputFormat.None,
                                                                  doValidateProjectAssets: false,
                                                                  doValidateAcrossScenes: true);
        }

        [MenuItem("Tools/AssetValidator/Validate All Scenes in Build Settings and Asset Bundles", priority = 14)]
        public static void ValidateAllAssetsInAllScenesInBuildSettingsAndAssetBundles()
        {
            AssetValidatorEditorWindow.LaunchWindowWithValidation(SceneValidationMode.AllBuildAndAssetBundleScenes,
                                                                  OutputFormat.None,
                                                                  doValidateProjectAssets: false,
                                                                  doValidateAcrossScenes: true);
        }
    }
}
