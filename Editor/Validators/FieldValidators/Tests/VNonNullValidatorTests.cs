/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using NUnit.Framework;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Validators.FieldValidators.Tests
{
    [TestFixture]
    public class VNonNullValidatorTests
    {
        private NonNullValidator _validator;
        private VNonNullFieldTestObjectA _objectA;
        private VNonNullFieldTestObjectB _objectB;
        private GameObject _gameObject;

        [SetUp]
        public void Setup()
        {
            _validator = new NonNullValidator();

            _gameObject = new GameObject();
            _objectA = _gameObject.AddComponent<VNonNullFieldTestObjectA>();
            _objectB = _gameObject.AddComponent<VNonNullFieldTestObjectB>();
        }

        [TearDown]
        public void Cleanup()
        {
            Object.DestroyImmediate(_gameObject);
        }

        [Test]
        public void AssertThatFieldValidatorValidatesFalseOnNonNullRef()
        {
            Assert.False(_validator.Validate(_objectA));
        }
        
        [Test]
        public void AssertThatFieldValidatorValidatesTrueOnNonNullRef()
        {
            Assert.True(_validator.Validate(_objectB));
        }
    }

    [OnlyIncludeInTests]
    [Validate]
    public class VNonNullFieldTestObjectA : MonoBehaviour
    {
        [NonNull]
        public object obj;
    }

    [OnlyIncludeInTests]
    [Validate]
    public class VNonNullFieldTestObjectB : MonoBehaviour
    {
        public object obj;
    }
}
