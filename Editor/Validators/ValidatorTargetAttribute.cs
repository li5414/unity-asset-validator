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
    /// Identifies a Validator by way of a unique symbol.
    /// </summary>
    public class ValidatorTargetAttribute : Attribute
    {
        public string Symbol { get; private set; }

        /// <summary>
        /// Identifies a Validator by way of a unique symbol.
        /// </summary>
        /// <param name="symbol"></param>
        public ValidatorTargetAttribute(string symbol) { Symbol = symbol; }
    }
}
