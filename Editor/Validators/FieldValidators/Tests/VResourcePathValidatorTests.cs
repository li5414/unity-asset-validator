/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using NUnit.Framework;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JCMG.AssetValidator.Editor.Validators.FieldValidators.Tests
{
    [TestFixture]
    public class VResourcePathValidatorTests
    {
        private ResourcePathValidator _vrpValidator;
        private VResourcePathTestObjectA _vResourcePathTestObjectA;
        private VResourcePathTestObjectB _vResourcePathTestObjectB;
        private GameObject _gameObject;

        [SetUp]
        public void Setup()
        {
            _vrpValidator = new ResourcePathValidator();
            _gameObject = new GameObject();
            _vResourcePathTestObjectA = _gameObject.AddComponent<VResourcePathTestObjectA>();
            _vResourcePathTestObjectB = ScriptableObject.CreateInstance<VResourcePathTestObjectB>();
        }

        [TearDown]
        public void Cleanup()
        {
            Object.DestroyImmediate(_gameObject);
            Object.DestroyImmediate(_vResourcePathTestObjectB);
        }

        private void ToggleResources(bool isValidResources)
        {
            var resourcePath = isValidResources ? "ProjectPrefabSearchTest" : "Invalid path";

            _vResourcePathTestObjectA.resourcePath = resourcePath;
            _vResourcePathTestObjectA.helper = new ResourcePathHelper(resourcePath);

            _vResourcePathTestObjectB.resourcePath = resourcePath;
            _vResourcePathTestObjectB.helper = new ResourcePathHelper(resourcePath);
        }

        [Test]
        [Ignore("This test is skipped due to not wanting to include unessary Resources folder in projects using this library. " +
                "See the readme.md file in the resource_unit_test folder for details on running these tests")]
        public void AssertThatValidatorValidatesTrueOnResourcePath()
        {
            ToggleResources(true);
            
            // Monobehavior
            var aFields = _vrpValidator.GetFieldInfosApplyTo(_vResourcePathTestObjectA);

            Assert.AreEqual(2, aFields.Count());

            Assert.True(_vrpValidator.AppliesTo(_vResourcePathTestObjectA));
            Assert.True(_vrpValidator.Validate(_vResourcePathTestObjectA));

            // ScriptableObject
            var bFields = _vrpValidator.GetFieldInfosApplyTo(_vResourcePathTestObjectB);

            Assert.AreEqual(2, bFields.Count());

            Assert.True(_vrpValidator.AppliesTo(_vResourcePathTestObjectB));
            Assert.True(_vrpValidator.Validate(_vResourcePathTestObjectB));
        }

        [Test]
        [Ignore("This test is skipped due to not wanting to include unessary Resources folder in projects using this library. " +
                "See the readme.md file in the resource_unit_test folder for details on running these tests")]
        public void AssertThatValidatorValidatesFalseOnResourcePathString()
        {
            ToggleResources(false);

            // Monobehavior
            var aFields = _vrpValidator.GetFieldInfosApplyTo(_vResourcePathTestObjectA);

            Assert.AreEqual(2, aFields.Count());

            Assert.True(_vrpValidator.AppliesTo(_vResourcePathTestObjectA));
            Assert.False(_vrpValidator.Validate(_vResourcePathTestObjectA));

            // ScriptableObject
            var bFields = _vrpValidator.GetFieldInfosApplyTo(_vResourcePathTestObjectB);

            Assert.AreEqual(2, bFields.Count());

            Assert.True(_vrpValidator.AppliesTo(_vResourcePathTestObjectB));
            Assert.False(_vrpValidator.Validate(_vResourcePathTestObjectB));
        }

        public class VResourcePathTestObjectA : MonoBehaviour
        {
            [ResourcePath]
            public string resourcePath;

            [ResourcePath]
            public ResourcePathHelper helper;
        }

        public class VResourcePathTestObjectB : ScriptableObject
        {
            [ResourcePath]
            public string resourcePath;

            [ResourcePath]
            public ResourcePathHelper helper;
        }

        public class ResourcePathHelper
        {
            private readonly string _path;

            public ResourcePathHelper(string path)
            {
                _path = path;
            }

            public override string ToString()
            {
                return _path;
            }
        }
    }
}
