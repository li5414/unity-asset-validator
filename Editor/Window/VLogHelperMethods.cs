/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using UnityEditor.SceneManagement;

// ReSharper disable once CheckNamespace
namespace JCMG.AssetValidator.Editor.Validators.Output
{
    public partial class VLog
    {
        public string GetSourceDescription()
        {
            switch (source)
            {

                case VLogSource.Scene:
                    return scenePath;
                case VLogSource.Project:
                    return "Project";
                default:
                case VLogSource.None:
                    return "None";
            }
        }

        public bool HasObjectPath()
        {
            return !string.IsNullOrEmpty(objectPath);
        }

        public bool CanLoadScene()
        {
            return !string.IsNullOrEmpty(scenePath) &&
                   EditorSceneManager.GetActiveScene().path != scenePath;
        }

        public bool CanPingObject()
        {
            return HasObjectPath() &&
                   (source == VLogSource.Scene && EditorSceneManager.GetActiveScene().path == scenePath ||
                    source == VLogSource.Project);
        }
    }
}