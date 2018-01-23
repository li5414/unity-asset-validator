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
using Object = UnityEngine.Object;

namespace JCMG.AssetValidator.Editor.Validators
{
    public abstract class AbstractInstanceValidator
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

        protected Type _typeToTrack;

        public abstract bool Validate(Object o);

        public abstract bool AppliesTo(Object obj);

        public abstract bool AppliesTo(Type type);

        protected void DispatchVLogEvent(Object obj, VLogType type, string message)
        {
            if (OnLogEvent != null)
            {
                OnLogEvent(new VLog()
                {
                    vLogType = type,
                    validatorName = TypeName,
                    message = message,
                    objectPath = ObjectUtility.GetObjectPath(obj)
                });
            }
        }
    }
}