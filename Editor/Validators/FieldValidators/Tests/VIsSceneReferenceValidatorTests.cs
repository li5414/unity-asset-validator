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
    public class VIsSceneReferenceValidatorTests
    {
        private IsSceneReferenceValidator _sValidator;
        private GameObject _gameObject;
        private SceneRefObjectA _projectRefObjectA;
        private const string UNIT_TEST_RESOURCE_PATH = "ProjectPrefabSearchTest";

        [SetUp]
        public void Setup()
        {
            _sValidator = new IsSceneReferenceValidator();
            _gameObject = new GameObject();
            _projectRefObjectA = _gameObject.AddComponent<SceneRefObjectA>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_gameObject);
        }

        [Test]
        public void AssertThatValidatorReturnsFalseForNullReference()
        {
            Assert.False(_sValidator.Validate(_projectRefObjectA));
        }

        [Test]
        public void AssertThatValidatorReturnsTrueForSceneReference()
        {
            var gameObjectSceneRef = new GameObject();
            _projectRefObjectA.projectRefField = gameObjectSceneRef;

            Assert.True(_sValidator.Validate(_projectRefObjectA));
        }

        [Test]
        public void AssertThatValidatorReturnsFalseForProjectReference()
        {
            var projectRef = Resources.Load(UNIT_TEST_RESOURCE_PATH);
            _projectRefObjectA.projectRefField = projectRef;

            Assert.False(_sValidator.Validate(_projectRefObjectA));
        }

        public class SceneRefObjectA : MonoBehaviour
        {
            [IsSceneReference]
            public Object projectRefField;
        }
    }
}

