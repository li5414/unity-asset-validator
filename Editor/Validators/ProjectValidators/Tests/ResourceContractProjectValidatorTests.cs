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
    public class ResourceContractProjectValidatorTests
    {
        private ResourceContractProjectValidator _cValidator;

        [SetUp]
        public void SetUp()
        {
            _cValidator = new ResourceContractProjectValidator();
        }

        [Test]
        [Ignore("In order to include the unit tests, but not have unecessary Resources assets included with this project I have " +
                "set this test to be ignored and renamed the Resources folder it was using for the unit test to 'Resource_Unit_Test'." +
                "In order to run this unit test, comment out this ignore attribute and rename that folder to Resources.")]
        public void AssertThatUnitTestGuaranteeIsFoundAndCanBeValidated()
        {
            _cValidator.Search();

            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var prop = typeof(ResourceContractProjectValidator).GetProperty("ResourcePaths", flags);

            var list = (List<string>)prop.GetValue(_cValidator, null);
            list = list.Where(x => x == UnitTestResourcePathContract.UNIT_TEST_RESOURCE_PATH).ToList();
            prop.SetValue(_cValidator, list, null);

            Assert.True(_cValidator.GetNumberOfResults() == 1, "");
            Assert.True(_cValidator.Validate());
        }

        public class UnitTestResourcePathContract : ResourcePathContract
        {
            public const string UNIT_TEST_RESOURCE_PATH = "ProjectPrefabSearchTest";

            public override IEnumerable<string> GetPaths()
            {
                return new[] { UNIT_TEST_RESOURCE_PATH };
            }
        }
    }
}
