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
    public class VIsProjectReferenceValidatorTests
    {
        private IsProjectReferenceValidator _pValidator;
        private GameObject _gameObject;
        private ProjectRefObjectA _projectRefObjectA;
        private const string UNIT_TEST_RESOURCE_PATH = "ProjectPrefabSearchTest";

        [SetUp]
        public void Setup()
        {
            _pValidator = new IsProjectReferenceValidator();
            _gameObject = new GameObject();
            _projectRefObjectA = _gameObject.AddComponent<ProjectRefObjectA>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_gameObject);
        }

        [Test]
        [Ignore("This test is skipped due to not wanting to include unessary Resources folder in projects using this library. " +
                "See the readme.md file in the resource_unit_test folder for details on running these tests")]
        public void AssertThatValidatorReturnsFalseForNullReference()
        {
            Assert.False(_pValidator.Validate(_projectRefObjectA));
        }

        [Test]
        [Ignore("This test is skipped due to not wanting to include unessary Resources folder in projects using this library. " +
                "See the readme.md file in the resource_unit_test folder for details on running these tests")]
        public void AssertThatValidatorReturnsFalseForSceneReference()
        {
            var gameObjectSceneRef = new GameObject();
            _projectRefObjectA.projectRefField = gameObjectSceneRef;

            Assert.False(_pValidator.Validate(_projectRefObjectA));
        }

        [Test]
        [Ignore("This test is skipped due to not wanting to include unessary Resources folder in projects using this library. " +
                "See the readme.md file in the resource_unit_test folder for details on running these tests")]
        public void AssertThatValidatorReturnsTrueForProjectReference()
        {
            var projectRef = Resources.Load(UNIT_TEST_RESOURCE_PATH);
            _projectRefObjectA.projectRefField = projectRef;

            Assert.True(_pValidator.Validate(_projectRefObjectA));
        }

        public class ProjectRefObjectA : MonoBehaviour
        {
            [IsProjectReference]
            public Object projectRefField;
        }
    }
}

