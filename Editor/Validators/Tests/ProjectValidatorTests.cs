/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Meta;
using JCMG.AssetValidator.Editor.Validators.Output;
using JCMG.AssetValidator.UnitTestObjects;
using NUnit.Framework;

namespace JCMG.AssetValidator.Editor.Validators.Tests
{
    [TestFixture]
    public class ProjectValidatorTests
    {
        private ProjectAssetValidatorManager _pValidatorManager;
        private ClassTypeCache _coreCache;
        private AssetValidatorLogger _logger;

        [SetUp]
        public void Setup()
        {
            _coreCache = new ClassTypeCache();
            _coreCache.AddTypeWithAttribute<Monobehavior2, ValidateAttribute>();
            _logger = new AssetValidatorLogger();

            _pValidatorManager = new ProjectAssetValidatorManager(_coreCache, _logger);
        }

        [TearDown]
        public void Teardown()
        {
            if(_pValidatorManager != null) _pValidatorManager.Dispose();
        }

        [Test]
        public void AssertThatProjectValidatorSearchCanFindProjectAsset()
        {
            Assert.AreEqual(0, _pValidatorManager.GetObjectsToValidate().Count);

            _pValidatorManager.Search();

            Assert.AreEqual(1, _pValidatorManager.GetObjectsToValidate().Count);
        }

        [Test]
        public void AssertThatProjectValidatorCanFindAndIdentifyIssuesWithProjectAssetsAllAtOnce()
        {
            _pValidatorManager.Search();
            _pValidatorManager.ValidateAll();

            var logs = _logger.GetLogs();

            Assert.AreEqual(1, _pValidatorManager.GetObjectsToValidate().Count);
            Assert.AreEqual(VLogType.Error, logs[0].vLogType);
        }

        [Test]
        public void AssertThatProjectValidatorCanFindAndIdentifyIssuesWithProjectAssetsContinuously()
        {
            _pValidatorManager.Search();

            while (_pValidatorManager.ContinueValidation())
            {
                // Do Nothing
            }

            var logs = _logger.GetLogs();

            Assert.AreEqual(1, _pValidatorManager.GetObjectsToValidate().Count);
            Assert.AreEqual(VLogType.Error, logs[0].vLogType);
        }
    }
}
