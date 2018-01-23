/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
#if UNITY_5_6_OR_NEWER
using System.Collections;
using JCMG.AssetValidator.Editor.Meta;
using JCMG.AssetValidator.Editor.Validators.FieldValidators;
using JCMG.AssetValidator.Editor.Validators.Output;
using JCMG.AssetValidator.UnitTestObjects;
using NUnit.Framework;
using UnityEngine;

using UnityEngine.TestTools;

namespace JCMG.AssetValidator.Editor.Validators.Tests
{
    [TestFixture]
    public class SceneValidatorTests
    {
        private ActiveSceneValidatorManager _sValidatorManager;
        private ClassTypeCache _coreCache;
        private AssetValidatorLogger _logger;

        [SetUp]
        public void Setup()
        {
            _coreCache = new ClassTypeCache();
            _coreCache.AddTypeWithAttribute<Monobehavior2, ValidateAttribute>();
            _logger = new AssetValidatorLogger();

            _sValidatorManager = new ActiveSceneValidatorManager(_coreCache, _logger);
        }

        [TearDown]
        public void Teardown()
        {
            if (_sValidatorManager != null) _sValidatorManager.Dispose();
        }

        [UnityTest]
        public IEnumerator AssertThatSceneSearcherCanFindObjectsToValidate()
        {
            _sValidatorManager.Search();

            Assert.AreEqual(0, _sValidatorManager.GetObjectsToValidate().Count);

            yield return null;

            var validObjectOne = new GameObject();
            var validObjectTwo = new GameObject();
            var validObjectThree = new GameObject();

            var sceneParserTestObjectA = validObjectOne.AddComponent<SceneParserTestObjectA>();
            validObjectTwo.AddComponent<SceneParserTestObjectB>();
            validObjectThree.AddComponent<SceneParserTestObjectB>();

            yield return null;

            _sValidatorManager.Search();

            var objectsToValidate = _sValidatorManager.GetObjectsToValidate();
            Assert.Contains(sceneParserTestObjectA, objectsToValidate, 
                "ObjectsToValidate should contain a SceneParserTestObjectA...");
            Assert.AreEqual(3, objectsToValidate.Count);

            Object.DestroyImmediate(validObjectOne);
            Object.DestroyImmediate(validObjectTwo);
            Object.DestroyImmediate(validObjectThree);
        }
        
        [UnityTest]
        public IEnumerator AssertThatSceneSearcherCanValidateObjects()
        {
            var validObjectOne = new GameObject("TargetToEval");
            var validObjectTwo = new GameObject("TargetToIgnore");
            var validObjectThree = new GameObject("TargetTo");

            validObjectOne.AddComponent<SceneParserTestObjectA>();

            yield return null;

            _sValidatorManager.Search();
            _sValidatorManager.ValidateAll();

            validObjectTwo.AddComponent<SceneParserTestObjectB>();
            validObjectThree.AddComponent<SceneParserTestObjectC>();

            yield return null;

            _sValidatorManager.Search();
            _sValidatorManager.ValidateAll();

            Object.DestroyImmediate(validObjectOne);
            Object.DestroyImmediate(validObjectTwo);
            Object.DestroyImmediate(validObjectThree);
        }

        public class SceneParserTestAttribute : FieldAttribute { }

        [OnlyIncludeInTests]
        [FieldTarget("scene_parser_test_validator", typeof(SceneParserTestAttribute))]
        public class SceneParserTestValidator : BaseFieldValidator
        {
            public override bool Validate(Object obj)
            {
                return obj.GetType() != typeof(SceneParserTestObjectA);
            }
        }
        
        [Validate]
        public class SceneParserTestObjectA : Monobehavior2
        {
            [SceneParserTest]
            public object obj;
        }
        
        [Validate]
        public class SceneParserTestObjectB : Monobehavior2
        {
            public object obj;
        }
        
        [Validate]
        public class SceneParserTestObjectC : Monobehavior2
        {
            [SceneParserTest]
            public object obj;
        }
    }
}
#endif