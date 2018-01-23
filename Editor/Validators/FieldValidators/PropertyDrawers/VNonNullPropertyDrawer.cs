/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Validators.FieldValidators.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(NonNullAttribute))]
    public class VNonNullPropertyDrawer : PropertyDrawer
    {
        private bool isInvalid;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var targetObject = property.serializedObject.targetObject as object;
            var targetObjectClassType = targetObject.GetType();
            var field = targetObjectClassType.GetField(property.propertyPath);
            var value = field.GetValue(targetObject);

            isInvalid = value == null || value.ToString() == "null";
            if (isInvalid)
            {
                label = EditorGUI.BeginProperty(position, label, property);
                var contentPosition = EditorGUI.PrefixLabel(position, label, ValidationGraphicsConstants.ErrorStyle);
                EditorGUI.indentLevel = 0;
                EditorGUI.PropertyField(contentPosition, property, GUIContent.none);
                EditorGUI.EndProperty();

                //EditorGUI.LabelField(new Rect(position.x, position.y, position.width, position.height / 2f), "Missing Ref!", ValidationGraphicsConstants.ErrorStyle);
                //EditorGUI.PropertyField(new Rect(position.x, position.y + position.height, position.width, position.height / 2f), property);
            }
            else
            {
                EditorGUI.PropertyField(position, property);
            }
        }
    }
}
