/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Validators.CrossSceneValidators
{
    /// <summary>
    /// This validator is used to ensure that there is only one camera tagged as 
    /// the MainCamera across all Scene(s) searched.
    /// </summary>
    [ValidatorTarget("cross_scene_multiple_cameras")]
    [ValidatorDescription("Checks one or more Scenes to ensure that there is only one Camera tagged as the MainCamera.")]
    public class MultipleMainCamerasValidator : EnsureComponentIsUniqueValidator<Camera>
    {
        private const string MAIN_CAMERA_TAG = "MainCamera";
        public override bool ShouldAddComponent(Camera component)
        {
            return component.CompareTag(MAIN_CAMERA_TAG);
        }

        public override string UniqueTarget
        {
            get { return "Camera Tagged as \"MainCamera\""; }
        }
    }
}
