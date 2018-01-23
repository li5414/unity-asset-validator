/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;

namespace JCMG.AssetValidator.Editor.Validators
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ValidatorDescriptionAttribute : Attribute
    {
        public string Description { get; private set; }

        public ValidatorDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
