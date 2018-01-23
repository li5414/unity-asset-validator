/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;

namespace JCMG.AssetValidator.Editor.Validators.FieldValidators
{
    /// <summary>
    /// VFieldTarget is used by VFieldValidator to determine whether 
    /// or not it applies to a particular object or type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FieldTargetAttribute : ValidatorTargetAttribute
    {
        public Type TargetType { get; private set; }

        public FieldTargetAttribute(string symbol, Type targetType) : base(symbol)
        {
            if (!targetType.IsSubclassOf(typeof(FieldAttribute)))
                throw new Exception("VFieldTarget must target an attribute deriving from VFieldAttribute");

            TargetType = targetType;
        }
    }
}
