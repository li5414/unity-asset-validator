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
using JCMG.AssetValidator.UnitTestObjects;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JCMG.AssetValidator.Editor.Validators.ObjectValidators.Tests
{
    [TestFixture]
    public class VRequireComponentValidatorTests
    {
        private HasComponentValidator _reqValidator;
        private GameObject _gameObject;
        private GameObject _gameObjectTwo;

        [SetUp]
        public void SetUp()
        {
            _reqValidator = new HasComponentValidator();

            _gameObject = new GameObject();
            _gameObjectTwo = new GameObject();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_gameObject);
            Object.DestroyImmediate(_gameObjectTwo);
        }

        [Test]
        public void AssertThatValidatorCanInvalidateObjectWithoutRequiredComponent()
        {
            var reqObjectA = _gameObject.AddComponent<RequiredTestObjectA>();

            Assert.False(_reqValidator.Validate(reqObjectA));
        }

        [Test]
        public void AssetThatValidatorCanValidateObjectWithSingleRequirement()
        {
            var reqObjectA = _gameObject.AddComponent<RequiredTestObjectA>();
            _gameObject.AddComponent<RequirementA>();

            Assert.True(_reqValidator.Validate(reqObjectA));
        }

        [Test]
        public void AssetThatValidatorCanValidateObjectWithMultipleRequirements()
        {
            var reqObjectA = _gameObject.AddComponent<RequiredTestObjectB>();
            
            // Add all requirements
            _gameObject.AddComponent<RequirementA>();
            _gameObject.AddComponent<RequirementB>();
            _gameObject.AddComponent<RequirementC>();

            Assert.True(_reqValidator.Validate(reqObjectA));
        }

        [Test]
        public void AssetThatValidatorCanValidateObjectWithMultipleRequirementsWithChildren()
        {
            var reqObjectA = _gameObject.AddComponent<RequiredTestObjectB>();

            _gameObjectTwo.transform.SetParent(_gameObject.transform);

            // Add all requirements with some on child gameobjects
            _gameObject.AddComponent<RequirementA>();
            _gameObjectTwo.AddComponent<RequirementB>();
            _gameObjectTwo.AddComponent<RequirementC>();

            Assert.True(_reqValidator.Validate(reqObjectA));
        }

        [Test]
        public void AssetThatValidatorCanValidateObjectWithMultipleRequirementsWithParent()
        {
            var reqObjectA = _gameObject.AddComponent<RequiredTestObjectB>();

            _gameObject.transform.SetParent(_gameObjectTwo.transform);

            // Add all requirements with some on child gameobjects
            _gameObject.AddComponent<RequirementA>();
            _gameObjectTwo.AddComponent<RequirementB>();
            _gameObjectTwo.AddComponent<RequirementC>();

            Assert.True(_reqValidator.Validate(reqObjectA));
        }

        [Test]
        public void AssetThatValidatorCanInvalidateObjectWithMultipleRequirements()
        {
            var reqObjectA = _gameObject.AddComponent<RequiredTestObjectB>();

            // Add all but one requirement
            _gameObject.AddComponent<RequirementA>();
            _gameObject.AddComponent<RequirementB>();

            Assert.False(_reqValidator.Validate(reqObjectA));
        }

        [OnlyIncludeInTests]
        [AssetValidator.HasComponent(typeof(RequirementA))]
        public class RequiredTestObjectA : Monobehavior2 { }

        [AssetValidator.HasComponent(new [] {
            typeof(RequirementA),
            typeof(RequirementB),
            typeof(RequirementC)}, canBeOnChildObject:true, canBeOnParentObject:true)]
        public class RequiredTestObjectB : Monobehavior2 { }

        public class RequirementA : Monobehavior2 { }
        public class RequirementB : Monobehavior2 { }
        public class RequirementC : Monobehavior2 { }
    }
}
