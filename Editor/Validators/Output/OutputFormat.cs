/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;
using System.Runtime.Serialization;

namespace JCMG.AssetValidator.Editor.Validators.Output
{
    [Serializable]
    public enum OutputFormat : byte
    {
        [EnumMember(Value = "No Output")]
        None = 0,

        [EnumMember(Value = "Html")]
        Html = 1,

        [EnumMember(Value = "Csv")]
        Csv = 2,

        [EnumMember(Value = "Text")]
        Text = 3
    }
}