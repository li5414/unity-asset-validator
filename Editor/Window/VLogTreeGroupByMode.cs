/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System.Runtime.Serialization;

namespace JCMG.AssetValidator.Editor.Window
{
    public enum VLogTreeGroupByMode
    {
        [EnumMember(Value = "Group by Validator")]
        CollapseByValidatorType = 0,

        [EnumMember(Value = "Group by Source (Scene and Project)")]
        CollapseByArea = 1,
    }
}