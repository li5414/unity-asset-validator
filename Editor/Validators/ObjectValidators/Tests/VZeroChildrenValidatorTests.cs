/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
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
