/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace JCMG.AssetValidator.Editor.Utility.Tests
{
    [TestFixture]
    public class ReflectionUtilityTests
    {
        private readonly List<A> allAssignableFromA = new List<A>
        {
            new A(), new B(), new C(), new D()
        };
        private readonly List<A> allSubclassesOfA = new List<A>
        {
            new B(), new C(), new D()
        };

        [Test]
        public void AssertReflectionUtilityCanDeriveAllNestedSubclasses()
        {
            var allReflectionDerivedSubclassesOfA = new List<A>();
            allReflectionDerivedSubclassesOfA.AddRange(ReflectionUtility.GetAllDerivedInstancesOfType<A>());

            Assert.AreEqual(3, allReflectionDerivedSubclassesOfA.Count);
            for (var i = 0; i < allSubclassesOfA.Count; i++)
                Assert.IsTrue(allReflectionDerivedSubclassesOfA.Exists(x => x.GetType() == allSubclassesOfA[i].GetType()));
        }

        [Test]
        public void AssertThatReflectionUtilityCanDistinguishDerivedTypesThatImplementAnInterface()
        {
            // Test root interface
            var allTypesOfAThatImplementILetter = new List<A>();
            allTypesOfAThatImplementILetter.AddRange(ReflectionUtility.GetAllInstancesOfTypeWithInterface<A, ILetter>());

            Assert.AreEqual(4, allTypesOfAThatImplementILetter.Count);
            for (var i = 0; i < allAssignableFromA.Count; i++)
                Assert.IsTrue(allAssignableFromA.Exists(x => x.GetType() == allAssignableFromA[i].GetType()));

            // Test nested interface
            var allSubclassesOfAWithInterfaceIFavoriteLetter = new List<A>()
            {
                new C(), new D()
            };

            var reflectionDerivedTypesOfAWithInterfaceIFavoriteLetter = new List<A>();
            reflectionDerivedTypesOfAWithInterfaceIFavoriteLetter.AddRange(ReflectionUtility.GetAllInstancesOfTypeWithInterface<A, IFavoriteLetter>());

            Assert.AreEqual(2, reflectionDerivedTypesOfAWithInterfaceIFavoriteLetter.Count);
            for (var i = 0; i < allSubclassesOfAWithInterfaceIFavoriteLetter.Count; i++)
                Assert.IsTrue(reflectionDerivedTypesOfAWithInterfaceIFavoriteLetter.Exists(x => x.GetType() == allSubclassesOfAWithInterfaceIFavoriteLetter[i].GetType()));
        }

        [Test]
        public void AssertThatReflectionUtilityCanFindTypeInstancesWithSpecificAttribute()
        {
            // All classes with Letter attribute
            var allTypesWithAttribute = new List<A>()
            {
                new B(), new D()
            };

            var reflectionTypesOfAWithAttributeLetter = new List<A>();
            reflectionTypesOfAWithAttributeLetter.AddRange(ReflectionUtility.GetAllInstancesOfTypeWithAttribute<A, LetterAttribute>());

            Assert.AreEqual(2, reflectionTypesOfAWithAttributeLetter.Count);
            for (var i = 0; i < allTypesWithAttribute.Count; i++)
                Assert.IsTrue(reflectionTypesOfAWithAttributeLetter.Exists(x => x.GetType() == allTypesWithAttribute[i].GetType()));
        }

        [Test]
        public void AssertThatReflectionUtilityCanFindTypesWithSpecificAttribute()
        {
            // All classes with Letter attribute
            var allTypesWithAttribute = new List<Type>()
            {
                typeof(B), typeof(D)
            };

            var reflectionTypesOfAWithAttributeLetter = new List<Type>();
            reflectionTypesOfAWithAttributeLetter.AddRange(ReflectionUtility.GetAllDerivedTypesOfTypeWithAttribute<A, LetterAttribute>());

            Assert.AreEqual(2, reflectionTypesOfAWithAttributeLetter.Count);
            for (var i = 0; i < allTypesWithAttribute.Count; i++)
                Assert.IsTrue(reflectionTypesOfAWithAttributeLetter.Exists(x => x == allTypesWithAttribute[i]));
        }

        #region TestClasses

        public interface ILetter { }
        public interface IFavoriteLetter { }

        public class LetterAttribute : Attribute { }


        public class A : ILetter { }
        [Letter] public class B : A { }
        public class C : A, IFavoriteLetter { }
        public class D : B, IFavoriteLetter { }

        #endregion
    }
}
