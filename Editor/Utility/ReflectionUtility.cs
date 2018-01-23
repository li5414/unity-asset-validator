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
using System.Runtime.Serialization;

namespace JCMG.AssetValidator.Editor.Utility
{
    public static class ReflectionUtility
    {
        /// <summary>
        /// Returns an IEnumerable of class instances of Types derived from T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAllDerivedInstancesOfType<T>() where T : class, new()
        {
            var objects = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes()
                .Where(myType => myType.IsClass &&
                                 !myType.IsAbstract &&
                                 !myType.IsGenericType &&
                                 myType.IsSubclassOf(typeof(T))))
                {
                    objects.Add((T)Activator.CreateInstance(type));
                }
            }
            
            return objects;
        }

        /// <summary>
        /// Returns an IEnumerable of class instances of Types derived from T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAllDerivedInstancesOfTypeWithAttribute<T, TV>() 
            where T : class, new()
        {
            var objects = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes()
                        .Where(myType => myType.IsClass &&
                                         !myType.IsAbstract &&
                                         !myType.IsGenericType &&
                                         myType.IsSubclassOf(typeof(T)) &&
                                         myType.IsDefined(typeof(TV), true)))
                {
                    objects.Add((T) Activator.CreateInstance(type));
                }
            }
            return objects;
        }

        /// <summary>
        /// Returns an IEnumerable of class instances of Type T that implement interface V
        /// </summary>
        /// <typeparam name="T">The Type a class must derive from</typeparam>
        /// <typeparam name="TV">The Type a class must implement</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAllInstancesOfTypeWithInterface<T, TV>() where T : class, new()
        {
            var objects = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes()
                        .Where(myType => myType.IsClass &&
                                         !myType.IsAbstract &&
                                         !myType.IsGenericType &&
                                         (myType.IsAssignableFrom(typeof(T)) || myType.IsSubclassOf(typeof(T))) &&
                                         myType.GetInterfaces().Contains(typeof(TV))))
                {
                    objects.Add((T) Activator.CreateInstance(type));
                }
            }
            return objects;
        }

        /// <summary>
        /// Returns an IEnumerable of class instances of Type T that implement interface V
        /// </summary>
        /// <typeparam name="T">The Type a class must derive from</typeparam>
        /// <typeparam name="TV">The Attribute a class must have</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAllInstancesOfTypeWithAttribute<T, TV>() where T : class, new()
        {
            var objects = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes()
                        .Where(myType => myType.IsClass &&
                                         !myType.IsAbstract &&
                                         !myType.IsGenericType &&
                                         (myType.IsAssignableFrom(typeof(T)) || myType.IsSubclassOf(typeof(T))) &&
                                         myType.IsDefined(typeof(TV), true)))
                {
                    objects.Add((T) Activator.CreateInstance(type));
                }
            }
            return objects;
        }

        /// <summary>
        /// Returns an IEnumerable of class instances of Types derived from T that implement V.
        /// </summary>
        /// <typeparam name="T">The Type a class must derive from</typeparam>
        /// <typeparam name="TV">the Type a class must implement</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAllDerivedInstancesOfTypeWithInterface<T, TV>() where T : class, new()
        {
            var objects = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes()
                        .Where(myType => myType.IsClass &&
                                         !myType.IsAbstract &&
                                         !myType.IsGenericType &&
                                         myType.IsSubclassOf(typeof(T)) &&
                                         myType.GetInterfaces().Contains(typeof(TV))))
                {
                    objects.Add((T) Activator.CreateInstance(type));
                }
            }
            return objects;
        }

        /// <summary>
        /// Returns an IEnumerable of class instances of Types derived from T that take arguments constructorArgs
        /// </summary>
        /// <typeparam name="T">The Type a class must derive from</typeparam>
        /// <param name="constructorArgs"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllDerivedInstancesOfType<T>(params object[] constructorArgs) where T : class, new()
        {
            var objects = new List<T>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes()
                        .Where(myType => myType.IsClass &&
                                         !myType.IsAbstract &&
                                         !myType.IsGenericType &&
                                         myType.IsSubclassOf(typeof(T))))
                {
                    objects.Add((T) Activator.CreateInstance(type, constructorArgs));
                }
            }
            return objects;
        }

        /// <summary>
        /// Returns an IEnumerable of class instances of Types derived from T
        /// </summary>
        /// <typeparam name="T">The Type a class must derive from</typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllDerivedTypesOfType<T>() where T : class
        {
            var objects = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes()
                        .Where(myType => myType.IsClass &&
                                         myType.IsSubclassOf(typeof(T))))
                {
                    objects.Add(type);
                }
            }
            return objects;
        }

        /// <summary>
        /// Returns an IEnumerable of class instances of Types derived from T
        /// </summary>
        /// <typeparam name="T">The Type a class must derive from</typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllDerivedTypesOfTypeWithAttribute<T, TV>(bool inherit = true) 
            where T : class where TV : Attribute
        {
            var objects = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (var i = 0; i < assemblies.Length; i++)
            {
                foreach (var type in assemblies[i].GetTypes()
                                                  .Where(myType => (myType.IsClass &&
                                                                    myType.IsSubclassOf(typeof(T))) &&
                                                                    myType.IsDefined(typeof(TV), inherit)))
                {
                    objects.Add(type);
                }

            }
            
            return objects;
        }

        public static string ToEnumString<T>(T type)
        {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttribute.Value;
        }
    }
}