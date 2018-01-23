/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Validators
{
    /// <summary>
    /// SceneValidatorManager handles the loading, validation, and unloading of scenes.
    /// </summary>
    public sealed class SceneValidatorManager : IDisposable
    {
        private readonly BaseValidatorManager _validatorManager;
        private readonly IList<string> _scenePaths;

        private int progress = 0;

        public SceneValidatorManager(BaseValidatorManager validatorManager, IList<string> scenePaths)
        {
            _validatorManager = validatorManager;
            _scenePaths = scenePaths;
        }

        public bool CanContinueValidating()
        {
            if (progress < _scenePaths.Count)
                return true;

            return false;
        }

        public void ContinueValidating()
        {
            var path = _scenePaths[progress++];

            UpdateProgress(path);

            if (path == string.Empty)
            {
                Debug.LogWarning("The current scene must be saved in the project before it can be validated.");
                return;
            }

            EditorSceneManager.OpenScene(path);

            _validatorManager.Search();
            _validatorManager.ValidateAll();
        }

        private void UpdateProgress(string path)
        {
            EditorUtility.DisplayProgressBar("AssetValidator",
                string.Format("Searching and Validating Scene: [{0}]", path),
                progress / (float)_scenePaths.Count);
        }

        public void Dispose()
        {
            EditorUtility.ClearProgressBar();
        }
    }
}