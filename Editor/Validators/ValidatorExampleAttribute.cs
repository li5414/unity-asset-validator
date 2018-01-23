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
    /// <summary>
    /// Describes a code example about a particular validator
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ValidatorExampleAttribute : Attribute
    {
        private string _example;
        public string ExampleContent
        {
            get { return _example; }
        }

        public ValidatorExampleAttribute(string example)
        {
            _example = example;
        }
    }
}