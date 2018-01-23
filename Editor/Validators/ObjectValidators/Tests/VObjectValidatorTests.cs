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
    public class VObjectValidatorTests
    {
        private MonobehaviorObjectTestValidator _mbValidator;
        private ScriptableObjectTestValidator _soValidator;

        // Monobehaviors
        private VObjectTestA _vObjectTestA;
        private VObjectTestC _vObjectTestC;

        // ScriptableObjects
        private VObjectTestB _vObjectTestB;
        private VObjectTestD _vObjectTestD;

        private GameObject _gameObject;

        [SetUp]
        public void Setup()
        {
            _gameObject = new GameObject();
            _vObjectTestA = _gameObject.AddComponent<VObjectTestA>();
            _vObjectTestC = _gameObject.AddComponent<VObjectTestC>();

            _vObjectTestB = ScriptableObject.CreateInstance<VObjectTestB>();
            _vObjectTestD = ScriptableObject.CreateInstance<VObjectTestD>();

            _mbValidator = new MonobehaviorObjectTestValidator();
            _soValidator = new ScriptableObjectTestValidator();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_gameObject);
            Object.DestroyImmediate(_vObjectTestB);
            Object.DestroyImmediate(_vObjectTestD);
        }

        [Test]
        public void AssertThatObjectValidatorCanApplyOnlyInstanceOfMonobehavior()
        {
            Assert.True(_mbValidator.AppliesTo(_vObjectTestA)); // Can apply to decorated Monobehavior derived class
            Assert.False(_mbValidator.AppliesTo(_vObjectTestB)); // Cannot apply to decorated ScriptableObject
            Assert.False(_mbValidator.AppliesTo(typeof(VObjectTestE))); // Cannot apply to decorated POCO class
        }


        [Test]
        public void AssertThatObjectValidatorCanApplyOnlyToInstanceOfScriptableObject()
        {
            Assert.True(_soValidator.AppliesTo(_vObjectTestD)); // Can apply to decorated ScriptableObject
            Assert.False(_soValidator.AppliesTo(_vObjectTestC)); // Cannot apply to decorated Monobehavior
            Assert.False(_soValidator.AppliesTo(typeof(VObjectTestF))); // Cannot apply to decorated POCO class
        }

        public class ObjectTestMonobehaviorAttribute : ValidateAttribute { }

        public class ObjectTestScriptableObjectAttribute : ValidateAttribute
        {
            public ObjectTestScriptableObjectAttribute() : base(UnityTarget.ScriptableObject) { }
        }

        [OnlyIncludeInTests]
        [ObjectTarget("monobehavior_object_test_validator", typeof(ObjectTestMonobehaviorAttribute))]
        public class MonobehaviorObjectTestValidator : BaseObjectValidator
        {
            public override bool Validate(Object o)
            {
                return false;
            }
        }

        [OnlyIncludeInTests]
        [ObjectTarget("scriptable_object_test_validator", typeof(ObjectTestScriptableObjectAttribute))]
        public class ScriptableObjectTestValidator : BaseObjectValidator
        {
            public override bool Validate(Object o)
            {
                return false;
            }
        }

        // Test objects for monobehavior only attribute
        /// <summary>
        /// Valid for this attribute
        /// </summary>
        [ObjectTestMonobehavior]
        public class VObjectTestA : MonoBehaviour
        {
            public object obj;
        }

        /// <summary>
        /// Invalid for this attribute
        /// </summary>
        [ObjectTestMonobehavior]
        public class VObjectTestB : ScriptableObject
        {
            public object obj;
        }

        // Test objects for scriptable object only attribute
        /// <summary>
        /// Invalid for this attribute
        /// </summary>
        [ObjectTestScriptableObject]
        public class VObjectTestC : MonoBehaviour
        {
            public object obj;
        }

        /// <summary>
        /// Valid for this attribute
        /// </summary>
        [ObjectTestScriptableObject]
        public class VObjectTestD : ScriptableObject
        {
            public object obj;
        }

        // Test POCO objects for both attributes
        /// <summary>
        /// Invalid for this attribute
        /// </summary>
        [ObjectTestMonobehavior]
        public class VObjectTestE
        {
            public object obj;
        }

        /// <summary>
        /// Valid for this attribute
        /// </summary>
        [ObjectTestScriptableObject]
        public class VObjectTestF
        {
            public object obj;
        }
    }
}
