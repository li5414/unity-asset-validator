/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Object = UnityEngine.Object;

namespace JCMG.AssetValidator.Editor.Validators.FieldValidators
{
    public class BaseFieldValidator : AbstractInstanceValidator
    {
        protected BindingFlags _fieldTypes = BindingFlags.NonPublic |
                                            BindingFlags.Instance |
                                            BindingFlags.Public;

        public BaseFieldValidator()
        {
            var vFieldTargets = (FieldTargetAttribute[]) GetType().GetCustomAttributes(typeof(FieldTargetAttribute), false);

            if(vFieldTargets.Length == 0)
                throw new Exception("Subclasses of BaseVFieldValidator are required to be " +
                                    "decorated with a VFieldTargetAttribute to determine what " +
                                    "VFieldAttribute they are validating");

            _typeToTrack = vFieldTargets[0].TargetType;
        }

        public override bool Validate(Object o)
        {
            throw new NotImplementedException();
        }

        public override bool AppliesTo(Object obj)
        {
            var fields = GetFieldInfosApplyTo(obj);
            return fields.Any();
        }

        public override bool AppliesTo(Type type)
        {
            var fields = GetFieldInfosApplyTo(type);
            return fields.Any();
        }

        public IEnumerable<FieldInfo> GetFieldInfosApplyTo(object obj)
        {
            return GetFieldInfosApplyTo(obj.GetType());
        }

        public IEnumerable<FieldInfo> GetFieldInfosApplyTo(Type type)
        {
            return type.GetFields().Where(x => x.GetCustomAttributes(_typeToTrack, false).Length > 0);
        }
    }
}
