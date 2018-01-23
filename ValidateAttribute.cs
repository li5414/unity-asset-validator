/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;

namespace JCMG.AssetValidator
{
    /// <summary>
    /// [Validate] is used by the validator editor tools to determine which Monobehaviour 
    /// or ScriptableObject classes should be targeted for validation. It is necessary when 
    /// using Field Validation Attributes that [Validate] is on the class whose Fields are 
    /// being decorated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidateAttribute : Attribute
    {
        public UnityTarget UnityTarget { get; private set; }

        public ValidateAttribute()
        {
            UnityTarget = UnityTarget.Monobehavior;
        }

        protected ValidateAttribute(UnityTarget unityTarget)
        {
            UnityTarget = unityTarget;
        }
    }
}
