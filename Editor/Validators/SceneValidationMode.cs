/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;
using System.Runtime.Serialization;

namespace JCMG.AssetValidator.Editor.Validators
{
    [Serializable]
    public enum SceneValidationMode
    {
        [EnumMember(Value = "None")] None,
        [EnumMember(Value = "Active Scene Only")] ActiveScene,
        [EnumMember(Value = "All Scenes")] AllScenes,
        [EnumMember(Value = "All Build Scenes")] AllBuildScenes,
        [EnumMember(Value = "All Build and Asset Bundle Scenes")] AllBuildAndAssetBundleScenes
    }
}