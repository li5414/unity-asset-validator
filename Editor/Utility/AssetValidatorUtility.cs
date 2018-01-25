/*
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
using JCMG.AssetValidator.Editor.Config;
using JCMG.AssetValidator.Editor.Meta;
using JCMG.AssetValidator.Editor.Validators;
using JCMG.AssetValidator.Editor.Validators.Output;
using JCMG.AssetValidator.UnitTestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Utility
{
    public static class AssetValidatorUtility
    {
        public static OutputFormat EditorOuputFormat = OutputFormat.None;
        public static string EditorFilename = "asset_validator_results";

        public static bool IsDebugging
        {
            get { return EditorPrefs.GetBool(ASSET_VALIDATOR_IS_DEBUGGING, false); }
            set
            {
                EditorPrefs.SetBool(ASSET_VALIDATOR_IS_DEBUGGING, value);
            }
        }
        public const string ASSET_VALIDATOR_IS_DEBUGGING = "ASSET_VALIDATOR_IS_DEBUGGING";

        private static GUIStyle _bodyBackground;
        public static GUIStyle BodyBackground
        {
            get { return _bodyBackground ?? (_bodyBackground = "RL Background"); }
        }

        private static GUIStyle _headerBackground;
        public static GUIStyle HeaderBackground
        {
            get { return _headerBackground ?? (_headerBackground = "RL Header"); }
        }

        public static void ValidateAllAssetsInAssetsFolder(AssetValidatorLogger logger)
        {
            AssetValidatorOverrideConfig.FindOrCreate().AddDisabledLogs(logger);

            using (var projValidator = new ProjectAssetValidatorManager(GetDefaultClassCache(), logger))
            {
                while (projValidator.ContinueSearch())
                {
                    var searchProgress = projValidator.GetSearchProgress();
                    EditorUtility.DisplayProgressBar("AssetValidator", 
                                                     string.Format("Searching Project Assets: [{0:P2}%]", searchProgress),
                                                     searchProgress);
                }

                while (projValidator.ContinueValidation())
                {
                    var searchProgress = projValidator.GetProgress();
                    EditorUtility.DisplayProgressBar("AssetValidator",
                                                     string.Format("Validating Project Assets: [{0:P2}%]", searchProgress),
                                                     searchProgress);
                }
            }

            EditorUtility.ClearProgressBar();
        }

        public static void ValidateAllAssetsInActiveScene(AssetValidatorLogger logger)
        {
            AssetValidatorOverrideConfig.FindOrCreate().AddDisabledLogs(logger);

            var paths = new[] { EditorSceneManager.GetActiveScene().path };
            using (var sceneValidator = new ActiveSceneValidatorManager(GetDefaultClassCache(), logger))
                using (var validatorManager = new SceneValidatorManager(sceneValidator, paths))
                    while (validatorManager.CanContinueValidating())
                        validatorManager.ContinueValidating();
        }
        
        public static void ValidateAllAssetsInAllScenes(AssetValidatorLogger logger)
        {
            AssetValidatorOverrideConfig.FindOrCreate().AddDisabledLogs(logger);

            var allScenes = GetAllScenePathsInProject();
            using (var sceneValidator = new ActiveSceneValidatorManager(GetDefaultClassCache(), logger))
                using (var validatorManager = new SceneValidatorManager(sceneValidator, allScenes))
                    while (validatorManager.CanContinueValidating())
                        validatorManager.ContinueValidating();
        }
        
        public static void ValidateAllAssetsInAllScenesInBuildSettings(AssetValidatorLogger logger)
        {
            AssetValidatorOverrideConfig.FindOrCreate().AddDisabledLogs(logger);

            var buildScenes = GetAllScenePathsInBuildSettings();
            using (var sceneValidator = new ActiveSceneValidatorManager(GetDefaultClassCache(), logger))
                using (var validatorManager = new SceneValidatorManager(sceneValidator, buildScenes))
                    while (validatorManager.CanContinueValidating())
                        validatorManager.ContinueValidating();
        }

        public static void ValidateAllAssetsInAllScenesInBuildSettingsAndAssetBundles(AssetValidatorLogger logger)
        {
            AssetValidatorOverrideConfig.FindOrCreate().AddDisabledLogs(logger);

            var scenePaths = GetAllScenePathsInAssetBundles();
            scenePaths.AddRange(GetAllScenePathsInBuildSettings());
            using (var sceneValidator = new ActiveSceneValidatorManager(GetDefaultClassCache(), logger))
                using (var validatorManager = new SceneValidatorManager(sceneValidator, scenePaths))
                    while (validatorManager.CanContinueValidating())
                        validatorManager.ContinueValidating();
        }

        public static IList<string> GetScenePaths(SceneValidationMode vmode)
        {
            switch (vmode)
            {
                case SceneValidationMode.None:
                    return new string[0];
                case SceneValidationMode.ActiveScene:
                    return new[] {EditorSceneManager.GetActiveScene().path};
                case SceneValidationMode.AllScenes:
                    return GetAllScenePathsInProject();
                case SceneValidationMode.AllBuildScenes:
                    return GetAllScenePathsInBuildSettings();
                case SceneValidationMode.AllBuildAndAssetBundleScenes:
                    var finalScenes = GetAllScenePathsInAssetBundles();
                    finalScenes.AddRange(GetAllScenePathsInBuildSettings());

                    return finalScenes;
                default:
                    throw new ArgumentOutOfRangeException("vmode", vmode, null);
            }
        }

        public static List<string> GetAllScenePathsInProject()
        {
            var assetPaths = AssetDatabase.FindAssets("t:scene");
            var scenePaths = new List<string>();

            for (var i = 0; i < assetPaths.Length; i++)
                scenePaths.Add(AssetDatabase.GUIDToAssetPath(assetPaths[i]));

            return scenePaths;
        }

        public static List<string> GetAllScenePathsInAssetBundles()
        {
            var sceneNames = new List<string>();
            var allAssetBundleNames = AssetDatabase.GetAllAssetBundleNames();
            for (var i = 0; i < allAssetBundleNames.Length; i++)
            {
                var assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(allAssetBundleNames[i]);
                for (var j = 0; j < assetNames.Length; j++)
                {
                    if (assetNames[j].Contains(".unity"))
                        sceneNames.Add(assetNames[j]);
                }
            }

            return sceneNames;
        }

        public static IList<string> GetAllScenePathsInBuildSettings()
        {
            return EditorBuildSettings.scenes.Select(x => x.path).ToList();
        }

        public static string GetValidatorDescription(Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(ValidatorDescriptionAttribute), false) as ValidatorDescriptionAttribute[];
            return attrs != null && attrs.Length > 0
                ? attrs[0].Description
                : string.Empty;
        }

        private static ClassTypeCache GetDefaultClassCache()
        {
            var coreCache = new ClassTypeCache();

            // Ensure any unit test types do not get picked up for validation.
            coreCache.IgnoreType<Monobehavior2>();
            coreCache.IgnoreAttribute<OnlyIncludeInTestsAttribute>();

            // Find all objects for validation
            coreCache.AddTypeWithAttribute<MonoBehaviour, ValidateAttribute>();
            return coreCache;
        }
    }
}
