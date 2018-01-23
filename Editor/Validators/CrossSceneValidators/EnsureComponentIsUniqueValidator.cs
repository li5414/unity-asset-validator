/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Validators.Output;
using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JCMG.AssetValidator.Editor.Validators.CrossSceneValidators
{
    /// <summary>
    /// This validator should be derived from where there should only ever be one instance of a 
    /// component present in the scenes searched.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ValidatorDescription("This validator should be derived from where there should only ever be " +
                          "one instance of a component present in the scenes searched.")]
    [ValidatorExample(@"
/// <summary>
/// FooComponent is a Monobehavior derived component. There should only be 
/// one FooComponent for any of the scenes searched.
/// </summary>
public class FooComponent : MonoBehaviour
{

}

/// <summary>
/// This is a subclass of EnsureComponentIsUniqueValidator; as the generic type is of FooComponent,  
/// when this validator searches across one or more scenes, if it finds more than one instance of 
/// FooComponent a validation warning will be dispatched per instance and an error noting that there 
/// should only be one instance.
/// </summary>
public class EnsureFooComponentIsUniqueValidator : EnsureComponentIsUniqueValidator<FooComponent>
{
        
}
")]
    public class EnsureComponentIsUniqueValidator<T> : BaseCrossSceneValidator 
        where T : Component
    {
        private readonly List<VLog> _vLogList = new List<VLog>();
        private readonly Type _typeToTrack;

        public virtual string UniqueTarget
        {
            get { return _typeToTrack.Name; }
        }

        public EnsureComponentIsUniqueValidator()
        {
            _typeToTrack = typeof(T);
        }

        public override void Search()
        {
            var scenePath = EditorSceneManager.GetActiveScene().path;
            var component = Object.FindObjectsOfType(_typeToTrack);

            // Create a warning log for each event system found. These are 
            // only used if more than one is found across all scenes
            for (var i = 0; i < component.Length; i++)
                if(ShouldAddComponent((T)component[i]))
                    _vLogList.Add(CreateVLog(component[i], 
                                  VLogType.Warning,
                                  string.Format("Scene at path [{0}] is not the only Scene to contain an [{1}]...", 
                                                EditorSceneManager.GetActiveScene().path, UniqueTarget),
                                  scenePath));
        }

        public virtual bool ShouldAddComponent(T component)
        {
            return true;
        }

        public override bool Validate()
        {
            if (_vLogList.Count == 0) return true;

            for (var i = 0; i < _vLogList.Count; i++)
                DispatchVLogEvent(_vLogList[i]);

            DispatchVLogEvent(new VLog()
            {
                vLogType = VLogType.Error,
                source = VLogSource.None,
                validatorName = TypeName,
                scenePath = string.Empty,
                objectPath = string.Empty,
                message = string.Format("More than one Scene of the Scene(s) validated has an [{0}] present", _typeToTrack.Name)
            });

            return false;
        }
    }
}
