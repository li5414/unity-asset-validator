/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Validators.Output;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Validators.CrossSceneValidators
{
    [ValidatorTarget("cross_scene_missing_component_reference")]
    [ValidatorDescription("Checks one or more Scenes to see if there are any missing Component references on every Gameobject " +
                          "and and their SerializedProperties in that scene.")]
    public class MissingReferenceValidator : BaseCrossSceneValidator
    {
        private bool _foundMissingReferenceComponent;

        private const string _missingReferenceComponentError = "There is a missing component on Gameobject [{0}]";
        private const string _missingReferencePropertyError = "There is a missing component reference on Gameobject [{0}] on component [{1}] for property [{2}].";

        public override void Search()
        {
            var objects = Object.FindObjectsOfType<GameObject>();
            var currentScene = EditorSceneManager.GetActiveScene().path;

            foreach (var obj in objects)
            {
                var components = obj.GetComponents<Component>();

                foreach (var c in components)
                {
                    if (!c)
                    {
                        DispatchVLogEvent(obj,
                                          VLogType.Error,
                                          string.Format(_missingReferenceComponentError, obj.name),
                                          currentScene);

                        _foundMissingReferenceComponent = true;
                    }
                    else
                    {
                        var so = new SerializedObject(c);
                        var sp = so.GetIterator();

                        while (sp.NextVisible(true))
                        {
                            if (sp.propertyType != SerializedPropertyType.ObjectReference) continue;

                            if (sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)
                            {
                                DispatchVLogEvent(obj,
                                                  VLogType.Error,
                                                  string.Format(_missingReferencePropertyError,
                                                                obj.name,
                                                                c.GetType().Name,
                                                                ObjectNames.NicifyVariableName(sp.name)),
                                                  currentScene);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Return true only if we did not find any missing 
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (_foundMissingReferenceComponent)
            {
                DispatchVLogEvent(new VLog()
                {
                    vLogType = VLogType.Error,
                    source = VLogSource.None,
                    validatorName = TypeName,
                    scenePath = string.Empty,
                    objectPath = string.Empty,
                    message = "Missing component references were found in the Scene(s) searched..."
                });
            }

            return !_foundMissingReferenceComponent;
        }
    }
}