﻿/*
AssetValidator 
Copyright (c) 2018 Jeff Campbell

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using JCMG.AssetValidator.Editor.Meta;
using JCMG.AssetValidator.Editor.Validators.Output;
using JCMG.AssetValidator.UnitTestObjects;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;

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
