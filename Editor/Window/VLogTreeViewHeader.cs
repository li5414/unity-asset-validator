/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
#if UNITY_5_6_OR_NEWER
using JCMG.AssetValidator.Editor.Validators.Output;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Window
{
    public class VLogTreeViewHeader : TreeViewItem
    {
        public int errorCount;
        public int warningCount;
        public int infoCount;
        public bool hasLogCounts;

        public VLogTreeViewHeader(int id, int depth, string displayName) : base(id, depth, displayName)
        {
        }

        public void SetLogCounts(int errors, int warnings, int infos)
        {
            errorCount = Mathf.Clamp(errors, 0, 999);
            warningCount = Mathf.Clamp(warnings, 0, 999);
            infoCount = Mathf.Clamp(infos, 0, 999);

            hasLogCounts = true;
        }
    }
}
#endif