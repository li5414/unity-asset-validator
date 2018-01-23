/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
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
