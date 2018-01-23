/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using UnityEngine.EventSystems;

namespace JCMG.AssetValidator.Editor.Validators.CrossSceneValidators
{
    [ValidatorTarget("cross_scene_multiple_event_systems")]
    [ValidatorDescription("Checks one or more Scenes to ensure that there is only one EventSystem.")]
    public class MultipleEventSystemsValidator : EnsureComponentIsUniqueValidator<EventSystem> { }
}