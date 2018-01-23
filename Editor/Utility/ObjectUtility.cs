/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using UnityEngine;
using UnityEditor;

namespace JCMG.AssetValidator.Editor.Utility
{
    public static class ObjectUtility
    {
        public static string GetObjectPath(Object obj)
        {
            return AssetDatabase.GetAssetPath(obj) != string.Empty
                ? AssetDatabase.GetAssetPath(obj)
                : (obj as Component) != null
                    ? ((Component) obj).transform.GetPath()
                    : (obj as GameObject) != null
                        ? ((GameObject) obj).transform.GetPath()
                        : string.Empty;
        }

        public static bool IsProjectReference(Object obj)
        {
            var assetPath = AssetDatabase.GetAssetPath(obj);

            return !string.IsNullOrEmpty(assetPath);
        }

        public static bool IsSceneReference(Object obj)
        {
            return !IsProjectReference(obj);
        }

        public static bool IsNullReference(object obj)
        {
            return obj == null;
        }

        public static bool IsNullReference(Object obj)
        {
            return obj == null || obj.ToString() == "null";
        }
    }
}

