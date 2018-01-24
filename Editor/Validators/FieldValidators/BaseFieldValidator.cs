﻿/*
AssetValidator 
Copyright (c) 2018 Jeff Campbell

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
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
