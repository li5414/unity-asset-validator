/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using UnityEngine;
using Object = UnityEngine.Object;

namespace JCMG.AssetValidator.Editor.Validators.FieldValidators
{
    [FieldTarget("field_resource_path_validator", typeof(ResourcePathAttribute))]
    [ValidatorDescription("Uses the ```[ResourcePath]``` field attribute to ensure that the ToString result from this field results in a reference" +
                          "to an object that could be loaded from the Resources folder using Resources.Load.")]
    [ValidatorExample(@"
/// <summary>
/// ResourcePathComponent is a Monobehavior derived class that has been marked as a [Validate]
/// target. Any fields of [ResourcePath] on it will be used in an attempt to load an object from
/// the project using Resources.Load where the path will be the result of the ToString method of
/// the field value.
/// </summary>
[Validate]
public class ResourcePathComponent : MonoBehaviour
{
    [ResourcePath]
    public string resourcePath;

    [ResourcePath]
    public ResourcePathObjectExample resourcePathObject = new ResourcePathObjectExample()
    {
        folder = ""hero_icon"",
        item = ""warrior_icon""
    };
}

/// <summary>
/// When a [ResourcePath] is used on an object, its ToString method will be used to
/// get the resource path to be validated
/// </summary>
public class ResourcePathObjectExample
{
    public string folder;
    public string item;

    public override string ToString()
    {
        return string.Format(""{0}/{1}"", folder, item);
    }
}
")]
    public class ResourcePathValidator : BaseFieldValidator
    {
        public override bool Validate(Object obj)
        {
            var fields = GetFieldInfosApplyTo(obj);

            var isValidated = true;

            foreach (var fieldInfo in fields)
            {
                var value = fieldInfo.GetValue(obj);

                if (value == null)
                {
                    isValidated = false;
                    break;
                }

                var strValue = value.ToString();

                var resourceObj = Resources.Load(strValue);

                if(resourceObj != null) continue;

                isValidated = false;
                break;
            }

            return isValidated;
        }
    }
}
