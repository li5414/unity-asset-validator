/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Validators.Output;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Validators.FieldValidators
{
    [FieldTarget("field_non_null_validator", typeof(NonNullAttribute))]
    [ValidatorDescription("Uses the ```[NonNull]``` field attribute to ensure that the assigned reference is not null.")]
    [ValidatorExample(@"
/// <summary>
/// NonNullFieldComponent is a Monobehavior derived class that has been marked as a [Validate]
/// target. Any fields of [NonNull] on it will be checked to see if it has a null value. If the 
/// value of the field checked is null, a validation error will be dispatched. 
/// </summary>
[Validate]
public class NonNullFieldComponent : MonoBehaviour
{
    [NonNull]
    public GameObject nullFieldExample;
}
")]
    public class NonNullValidator : BaseFieldValidator
    {
        public override bool Validate(Object obj)
        {
            var isValidated = true;
            var fields = GetFieldInfosApplyTo(obj);

            foreach (var field in fields)
            {
                var value = field.GetValue(obj);

                // If the value is null or is equal to string "null" in the case of a gameobject ref
                if (value != null && (value.ToString() != "null")) continue;

                DispatchVLogEvent(obj, VLogType.Error, string.Format("'{0}' has a null assignment for field '{1}'", obj.name, field.Name));
                isValidated = false;
            }

            return isValidated;
        }
    }
}
