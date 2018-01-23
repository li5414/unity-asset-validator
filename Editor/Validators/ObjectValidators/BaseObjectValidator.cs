/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JCMG.AssetValidator.Editor.Validators.ObjectValidators
{
    public class BaseObjectValidator : AbstractInstanceValidator
    {
        private UnityTarget _targetType;

        public BaseObjectValidator()
        {
            var vObjectTargets = (ObjectTargetAttribute[])GetType().GetCustomAttributes(typeof(ObjectTargetAttribute), true);

            if (vObjectTargets.Length == 0)
                throw new Exception("Subclasses of VObjectValidator are required to be " +
                                    "decorated with a VValidate to determine what " +
                                    "VObjectTarget they are validating");
            
            _typeToTrack = vObjectTargets[0].TargetType;
            _targetType = vObjectTargets[0].TargetAttribute.UnityTarget;
        }

        public override bool Validate(Object o)
        {
            throw new NotImplementedException();
        }

        public override bool AppliesTo(Object obj)
        {
            return AppliesTo(obj.GetType());
        }

        public override bool AppliesTo(Type type)
        {
            return HasRelevantAttribute(type) && DerivesFromCorrectTargetType(type);
        }

        private bool HasRelevantAttribute(Type type)
        {
            var attr = type.GetCustomAttributes(_typeToTrack, true);
            return attr.Length > 0;
        }

        private bool DerivesFromCorrectTargetType(Type type)
        {
            switch (_targetType)
            {
                case UnityTarget.Monobehavior:
                    return type.IsSubclassOf(typeof(MonoBehaviour));
                case UnityTarget.ScriptableObject:
                    return type.IsSubclassOf(typeof(ScriptableObject));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
