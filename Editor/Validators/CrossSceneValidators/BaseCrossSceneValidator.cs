/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Utility;
using JCMG.AssetValidator.Editor.Validators.Output;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JCMG.AssetValidator.Editor.Validators.CrossSceneValidators
{
    public class BaseCrossSceneValidator
    {
        public Action<VLog> OnLogEvent;

        private string _typeName;
        public string TypeName
        {
            get
            {
                return string.IsNullOrEmpty(_typeName) 
                    ? (_typeName = GetType().Name) 
                    : _typeName;
            }
        }

        /// <summary>
        /// Search the current Scene and add the information gathered to a cache so that
        /// we can validate it in aggregate after the target Scene(s) have been searched.
        /// </summary>
        public virtual void Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// After all scenes have been searched, validate using the aggregated results.
        /// </summary>
        /// <returns></returns>
        public virtual bool Validate()
        {
            throw new NotImplementedException();
        }

        protected VLog CreateVLog(Object obj, 
                                  VLogType type, 
                                  string message, 
                                  string scenePath = "", 
                                  VLogSource source = VLogSource.Scene)
        {
            return new VLog()
            {
                vLogType = type,
                source = source,
                validatorName = TypeName,
                message = message,
                objectPath = ObjectUtility.GetObjectPath(obj),
                scenePath = scenePath
            };
        }

        protected void DispatchVLogEvent(VLog vlog)
        {
            if (OnLogEvent != null)
                OnLogEvent(vlog);
        }

        protected void DispatchVLogEvent(Object obj,
            VLogType type,
            string message,
            string scenePath = "",
            VLogSource source = VLogSource.Scene)
        {
            if (OnLogEvent != null)
                OnLogEvent(new VLog()
                {
                    vLogType = type,
                    source = source,
                    validatorName = TypeName,
                    message = message,
                    objectPath = ObjectUtility.GetObjectPath(obj),
                    scenePath = scenePath
                });
        }
    }
}
