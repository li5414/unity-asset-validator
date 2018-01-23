/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.UnitTestObjects;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Validators.FieldValidators.Tests
{
    [TestFixture]
    public class VFieldValidatorTests
    {
        private FieldTestValidator _validator;
        private VFieldTestObjectA _objectA;
        private VFieldTestObjectB _objectB;

        private GameObject _gameObject;

        [SetUp]
        public void Setup()
        {
            _gameObject = new GameObject();

            _validator = new FieldTestValidator();
            _objectA = _gameObject.AddComponent<VFieldTestObjectA>();
            _objectB = _gameObject.AddComponent<VFieldTestObjectB>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_gameObject);
        }

        [Test]
        public void AssertThatFieldValidatorAppliesToInstance()
        {
            var fields = _validator.GetFieldInfosApplyTo(_objectA);

            Assert.AreEqual(1, fields.Count());
            Assert.True(_validator.AppliesTo(_objectA));
        }

        [Test]
        public void AssertThatFieldValidatorDoesNotApplyToInstance()
        {
            var fields = _validator.GetFieldInfosApplyTo(_objectB);

            Assert.AreEqual(0, fields.Count());
            Assert.False(_validator.AppliesTo(_objectB));
        }
    }

    public class FieldTestAttribute : FieldAttribute { }

    [OnlyIncludeInTests]
    [FieldTarget("field_test_validator", typeof(FieldTestAttribute))]
    public class FieldTestValidator : BaseFieldValidator
    {
        public override bool Validate(Object obj)
        {
            return false;
        }
    }
    
    [Validate]
    public class VFieldTestObjectA : Monobehavior2
    {
        [FieldTest]
        public object obj;
    }
    
    [Validate]
    public class VFieldTestObjectB : Monobehavior2
    {
        public object obj;
    }
}
