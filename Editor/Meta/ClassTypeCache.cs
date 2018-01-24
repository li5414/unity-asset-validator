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
using JCMG.AssetValidator.Editor.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JCMG.AssetValidator.Editor.Meta
{
    public class ClassTypeCache
    {
        /// <summary>
        /// All cached types
        /// </summary>
        public readonly List<Type> Types = new List<Type>();

        /// <summary>
        /// Anything that is or derives from these types is not included
        /// </summary>
        private readonly List<Type> _ignoreClassTypes = new List<Type>();

        /// <summary>
        /// Anything that is or derives from these types is not included
        /// </summary>
        private readonly List<Type> _ignoreAttributeTypes = new List<Type>();

        public void AddType<T>() where T : class
        {
            var types = ReflectionUtility.GetAllDerivedTypesOfType<T>();

            foreach (var type in types)
            {
                if(_ignoreClassTypes.Any(x => type.IsSubclassOf(x) || type == x)) continue;
                if (_ignoreAttributeTypes.Any(x => type.GetCustomAttributes(x, true).Length > 0)) continue;

                Types.Add(type);
            }
        }

        public void AddTypeWithAttribute<T, TV>() where T : class where TV : Attribute
        {
            var types = ReflectionUtility.GetAllDerivedTypesOfTypeWithAttribute<T, TV>(false);

            foreach (var type in types)
            {
                if (_ignoreClassTypes.Any(x => type.IsSubclassOf(x) || type == x)) continue;
                if (_ignoreAttributeTypes.Any(x => type.GetCustomAttributes(x, false).Length > 0)) continue;

                Types.Add(type);
            }
        }

        /// <summary>
        /// Add a type to be excluded from caching even if it meets other AddType qualifications
        /// This type will be ignored if a type that is attempted to be added is a subclass of this type or 
        /// is this type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void IgnoreType<T>() where T : class
        {
            if(!_ignoreClassTypes.Contains(typeof(T)))
                _ignoreClassTypes.Add(typeof(T));
        }

        /// <summary>
        /// Add a type to be excluded from caching even if it meets other AddType qualifications
        /// This type will be ignored if a type that is attempted to be added is a subclass of this type or 
        /// is this type.
        /// </summary>
        /// <param name="type"></param>
        public void IgnoreType(Type type)
        {
            if(!type.IsClass)
                throw new ArgumentException(string.Format("Type {0} cannot be ignored as it is not a class", type));

            _ignoreClassTypes.Add(type);
        }

        /// <summary>
        /// Adds an attribute on a class to be excluded from caching even if it meets other 
        /// AddType qualifications. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void IgnoreAttribute<T>() where T : Attribute
        {
            if (!_ignoreAttributeTypes.Contains(typeof(T)))
                _ignoreAttributeTypes.Add(typeof(T));
        }

        public void Clear()
        {
            Types.Clear();
        }

        public int Count
        {
            get { return Types.Count; }
        }

        public Type this[int index]
        {
            get { return Types[index]; }
        }
    }
}