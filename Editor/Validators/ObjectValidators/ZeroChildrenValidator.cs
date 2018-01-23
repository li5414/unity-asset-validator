/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Validators.Output;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JCMG.AssetValidator.Editor.Validators.ObjectValidators
{
    [ObjectTarget("object_zero_children_validator", typeof(ZeroChildenAttribute))]
    [ValidatorDescription("Uses the ```[ZeroChilden]``` class attribute to ensure that the Gameobject has zero children.")]
    [ValidatorExample(@"
/// <summary>
/// ZeroChildrenComponent is a Monobehavior derived class that has been marked as a [Validate]
/// target due to [ZeroChilden] (a subclass attribute of [Validate]). All instances of ZeroChildrenComponent 
/// will be found in any scenes searched and in the project on prefabs if searched. If any child gameobjects
/// are found on the ZeroChildrenComponent instance, a validation error will be dispatched.
/// </summary>
[ZeroChilden]
public class ZeroChildrenComponent : MonoBehaviour
{

}
")]
    public class ZeroChildrenValidator : BaseObjectValidator
    {
        public override bool Validate(Object obj)
        {
            var monoBehaviour = obj as MonoBehaviour;
            if (monoBehaviour == null)
            {
                DispatchVLogEvent(obj, VLogType.Warning, string.Format("'{0}' could not be cast to a Monobehavior.", obj.name));

                return false;
            }

            var childCount = monoBehaviour.transform.childCount;
            if (childCount > 0)
                DispatchVLogEvent(obj, VLogType.Error, string.Format("'{0}' has one or more children when it should have zero.", obj.name));

            return childCount <= 0;
        }
    }
}
