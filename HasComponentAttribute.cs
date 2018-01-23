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
    /// Any Monobehavior decorated with HasComponentAttribute must have the described component type 
    /// directly on it or optionally on a child object.
    /// </summary>
    public class HasComponentAttribute : ValidateAttribute
    {
        public Type[] TargetTypes { get; private set; }
        public bool CanBeOnChildObject { get; private set; }
        public bool CanBeOnParentObject { get; private set; }

        /// <summary>
        /// Do Not Use. Required for Activator.CreateInstance usage only.
        /// </summary>
        public HasComponentAttribute() { }

        public HasComponentAttribute(Type type, 
                                          bool canBeOnChildObject = false,
                                          bool canBeOnParentObject = false)
        {
            TargetTypes = new []{type};
            CanBeOnChildObject = canBeOnChildObject;
            CanBeOnParentObject = canBeOnParentObject;
        }

        public HasComponentAttribute(Type[] types,
                                          bool canBeOnChildObject = false,
                                          bool canBeOnParentObject = false)
        {
            TargetTypes = types;
            CanBeOnChildObject = canBeOnChildObject;
            CanBeOnParentObject = canBeOnParentObject;
        }
    }
}
