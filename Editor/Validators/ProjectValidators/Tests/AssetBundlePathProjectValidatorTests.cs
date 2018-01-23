/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JCMG.AssetValidator.Editor.Validators.ProjectValidators.Tests
{
    [TestFixture]
    public class AssetBundlePathProjectValidatorTests
    {
        private AssetBundlePathProjectValidator _aValidator;

        [SetUp]
        public void SetUp()
        {
            _aValidator = new AssetBundlePathProjectValidator();
        }

        [Test]
        [Ignore("These tests require that an asset bundle be created with the name 'unit_test_bundle' and " +
                "a scene be created in it with the name of 'bundle_item.unity'. I would prefer to not include" +
                "any AssetBundles in the project by default and only create these when needing to run these unit tests.")]
        public void AssetThatValidatorCanFindAndValidateUnitTestBundle()
        {
            _aValidator.Search();

            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var prop = typeof(AssetBundlePathProjectValidator).GetProperty("AssetBundleValidationCache", flags);

            var dict = (Dictionary<string, List<string>>)prop.GetValue(_aValidator, null);
            dict = dict.Where(x => x.Key == UnitTestAssetBundleContract.UNIT_TEST_ASSET_VALID_BUNDLE).ToDictionary(x => x.Key, x => x.Value);
            prop.SetValue(_aValidator, dict, null);

            Assert.True(dict.Count == 1);
            Assert.True(_aValidator.Validate());
        }

        [Test]
        [Ignore("These tests require that an asset bundle be created with the name 'unit_test_bundle' and " +
               "a scene be created in it with the name of 'bundle_item.unity'. I would prefer to not include" +
               "any AssetBundles in the project by default and only create these when needing to run these unit tests.")]
        public void AssetThatValidatorCannotFindAndValidateInvalidUnitTestBundle()
        {
            _aValidator.Search();

            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var prop = typeof(AssetBundlePathProjectValidator).GetProperty("AssetBundleValidationCache", flags);

            var dict = (Dictionary<string, List<string>>)prop.GetValue(_aValidator, null);
            dict = dict.Where(x => x.Key == UnitTestAssetBundleContract.UNIT_TEST_ASSET_INVALID_BUNDLE).ToDictionary(x => x.Key, x => x.Value);
            prop.SetValue(_aValidator, dict, null);

            Assert.True(dict.Count == 1);
            Assert.False(_aValidator.Validate());
        }

        public class UnitTestAssetBundleContract : AssetBundlePathContract
        {
            public const string UNIT_TEST_ASSET_VALID_BUNDLE = "unit_test_bundle";
            public const string UNIT_TEST_ASSET_BUNDLE_VALID_ITEM = "bundle_item.unity";

            public const string UNIT_TEST_ASSET_INVALID_BUNDLE = "invalid_unit_test_bundle";

            public const string UNIT_TEST_ASSET_BUNDLE_INVALID_ITEM = "invalid_bundle_item.unity";

            public override Dictionary<string, List<string>> GetPaths()
            {
                var dict = new Dictionary<string, List<string>>
                {
                    {UNIT_TEST_ASSET_VALID_BUNDLE, new List<string>() {UNIT_TEST_ASSET_BUNDLE_VALID_ITEM}},
                    {UNIT_TEST_ASSET_INVALID_BUNDLE, new List<string>() {UNIT_TEST_ASSET_BUNDLE_INVALID_ITEM}}
                };

                return dict;
            }
        }
    }
}
