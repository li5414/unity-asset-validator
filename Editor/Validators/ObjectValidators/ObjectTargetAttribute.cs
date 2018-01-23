/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;

namespace JCMG.AssetValidator.Editor.Validators.ObjectValidators
{
    /// <summary>
    /// VObjectTarget is used by a VObjectValidator to determine whether 
    /// or not it applies to a particular object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ObjectTargetAttribute : ValidatorTargetAttribute
    {
        public Type TargetType { get; private set; }
        public ValidateAttribute TargetAttribute { get; private set; }

        public ObjectTargetAttribute(string symbol, Type targetType) : base(symbol)
        {
            if (!targetType.IsSubclassOf(typeof(ValidateAttribute)))
                throw new Exception("VObjectTarget must target an attribute deriving from VValidate");

            TargetType = targetType;
            TargetAttribute = Activator.CreateInstance(TargetType) as ValidateAttribute;
        }
    }
}
