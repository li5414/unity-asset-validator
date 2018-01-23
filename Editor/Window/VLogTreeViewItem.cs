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

namespace JCMG.AssetValidator.Editor.Window
{
    public class VLogTreeViewItem : TreeViewItem
    {
        public VLog Log { get; private set; }

        public VLogTreeViewItem(VLog log, int id, int depth, string displayName = "")
            : base(id, depth, displayName)
        {
            Log = log;
        }
    }
}
#endif