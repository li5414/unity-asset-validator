/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Utility
{
    public static class FileUtility
    {
        public static void ReduceAssetPathsToFileNames(List<string> bundleNames)
        {
            for (int i = 0; i < bundleNames.Count; i++)
                bundleNames[i] = bundleNames[i].Split('/').Last();
        }

        /// <summary>
        /// Returns AssetDatabase compatible file paths (relative to the Asset folder) for all files 
        /// that have a matching extension in extensions
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public static List<string> GetUnityFilePaths(string[] extensions)
        {
            var list = new List<string>();
            for (var i = 0; i < extensions.Length; i++)
            {
                extensions[i] = extensions[i].Replace(".", string.Empty);

                var filePaths = Directory.GetFiles(Application.dataPath, string.Format("*.{0}", extensions[i]), SearchOption.AllDirectories);

                for (var j = 0; j < filePaths.Length; j++)
                    list.Add(GetUnityRelativePath(filePaths[j]));
            }

            return list;
        }

        /// <summary>
        /// Returns AssetDatabase compatible file paths (relative to the Asset folder) for all files 
        /// that have a matching extension to extension
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static List<string> GetUnityFilePaths(string extension)
        {
            var list = new List<string>();
            extension = extension.Replace(".", string.Empty);

            var filePaths = Directory.GetFiles(Application.dataPath, string.Format("*.{0}", extension), SearchOption.AllDirectories);

            for (var j = 0; j < filePaths.Length; j++)
                list.Add(GetUnityRelativePath(filePaths[j]));

            return list;
        }

        private static readonly StringBuilder AssetStringBuilder = new StringBuilder();
        private static string GetUnityRelativePath(string absoluteFilePath)
        {
            AssetStringBuilder.Remove(0, AssetStringBuilder.Length);

            absoluteFilePath = absoluteFilePath.Replace("\\", "/");
            var splitFileName = absoluteFilePath.Split('/');

            var assetIndex = FindIndex(splitFileName, "Assets");

            for (var i = assetIndex; i < splitFileName.Length; i++)
            {
                if (i == assetIndex)
                    AssetStringBuilder.Append(splitFileName[i]);
                else
                    AssetStringBuilder.Append(string.Format("/{0}", splitFileName[i]));
            }

            return AssetStringBuilder.ToString();
        }

        private static int FindIndex(IList<string> strArray, string value)
        {
            for (var i = 0; i < strArray.Count; i++)
                if (strArray[i] == value)
                    return i;

            return strArray.Count;
        }
    }
}
