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
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JCMG.AssetValidator.Editor.Validators.ObjectValidators.Tests
{
    [TestFixture]
    public class VZeroChildrenValidatorTests
    {
        private ZeroChildrenValidator _vzValidator;
        private GameObject _gameObject;
        private VTestObjectA _vTestObjectA;

        [SetUp]
        public void Setup()
        {
            _gameObject = new GameObject();
            _vTestObjectA = _gameObject.AddComponent<VTestObjectA>();

            _vzValidator = new ZeroChildrenValidator();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_gameObject);
        }

        [Test]
        public void AssertThatVZeroChildrenValidatesAsTrueForZeroChildren()
        {
            Assert.True(_vzValidator.Validate(_vTestObjectA));
        }

        [Test]
        public void AssertThatVZeroChildrenValidatesAsFalseForOneOrMoreChildren()
        {
            var childObject = new GameObject();
            childObject.transform.SetParent(_gameObject.transform);

            Assert.False(_vzValidator.Validate(_vTestObjectA));
        }

        [OnlyIncludeInTests]
        [ZeroChilden]
        public class VTestObjectA : MonoBehaviour
        {
            
        }
    }
}
