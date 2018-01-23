/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Meta;
using NUnit.Framework;
using UnityEngine;

namespace JCMG.AssetValidator.Editor.Utility.Tests
{
    [TestFixture]
    public class ClassTypeCacheTests
    {
        private ClassTypeCache classTypeCache;

        [SetUp]
        public void Setup()
        {
            classTypeCache = new ClassTypeCache();
        }

        [Test]
        public void AssertThatTypeCacheCanProperlyFindAndCacheTypes()
        {
            classTypeCache.AddType<CTCValidatedEntity>();

            Assert.AreEqual(2, classTypeCache.Count);
            Assert.AreEqual(typeof(CTCTestValidatedEntity), classTypeCache[0]);
        }

        [Test]
        public void AssertThatTypeCacheCanProperlyFindAndCacheTypesWithAttributes()
        {
            classTypeCache.AddTypeWithAttribute<CTCValidatedEntity, ValidateAttribute>();

            Assert.AreEqual(1, classTypeCache.Count);
            Assert.AreEqual(typeof(CTCTestValidatedEntity), classTypeCache[0]);
        }

        [Test]
        public void AssertThatTypeCacheCanIgnoreClassTypes()
        {
            classTypeCache.IgnoreType<IgnoredCTCTestValidatedEntity>();
            classTypeCache.AddTypeWithAttribute<CTCValidatedEntity, ValidateAttribute>();

            Assert.AreEqual(1, classTypeCache.Count);
            Assert.AreEqual(typeof(CTCTestValidatedEntity), classTypeCache[0]);
        }

        [Test]
        public void AssertThatTypeCacheCanIgnoreAttributeTypes()
        {
            classTypeCache.IgnoreType<IgnoredCTCTestValidatedEntity>();
            classTypeCache.IgnoreAttribute<ValidateAttribute>();
            classTypeCache.AddTypeWithAttribute<CTCValidatedEntity, ValidateAttribute>();

            // Because we've added VValidateAttribute as an ignored attribute, we shouldn't find any types
            Assert.AreEqual(0, classTypeCache.Count);
        }

        public abstract class CTCValidatedEntity : MonoBehaviour { }

        [Validate]
        public class CTCTestValidatedEntity : CTCValidatedEntity
        {
            [NonNull]
            public GameObject PublicRef;
        }

        public class IgnoredCTCTestValidatedEntity : CTCTestValidatedEntity
        {
            
        }
    }
}
