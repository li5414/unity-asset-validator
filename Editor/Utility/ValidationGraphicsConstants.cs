/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Utility
{
    public static class ValidationGraphicsConstants
    {
        public const string CrossIconPath = "Assets/JCMG/AssetValidator/Editor/Graphics/cross_icon.png";
        public const string CheckIconPath = "Assets/JCMG/AssetValidator/Editor/Graphics/check_icon.png";
        public const string QuestionIconPath = "Assets/JCMG/AssetValidator/Editor/Graphics/question_icon.png";

        public static GUIStyle ErrorStyle = new GUIStyle
        {
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.red },
        };
    }
}