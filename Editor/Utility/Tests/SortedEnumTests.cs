/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Meta;
using NUnit.Framework;

namespace JCMG.AssetValidator.Editor.Utility.Tests
{
    [TestFixture]
    public class SortedEnumTests
    {
        private enum SortedEnumTestType : byte
        {
            A = 0, B = 1, C = 2
        }

        private SortedEnum<SortedEnumTestType> _se;

        [SetUp]
        public void SetUp()
        {
            _se = new SortedEnum<SortedEnumTestType>();
        }

        [Test]
        public void AssertCanGetAllValuesGreaterThanA()
        {
            var items = _se.GetAllGreaterThan(SortedEnumTestType.A);

            Assert.IsTrue(items.Contains(SortedEnumTestType.B));
            Assert.IsTrue(items.Contains(SortedEnumTestType.C));
        }

        [Test]
        public void AssertCanGetAllValuesGreaterThanOrEqualToA()
        {
            var items = _se.GetAllGreaterThanOrEqualTo(SortedEnumTestType.A);

            Assert.IsTrue(items.Contains(SortedEnumTestType.A));
            Assert.IsTrue(items.Contains(SortedEnumTestType.B));
            Assert.IsTrue(items.Contains(SortedEnumTestType.C));
        }

        [Test]
        public void AssertCanGetAllValuesLessThanC()
        {
            var items = _se.GetAllLesserThan(SortedEnumTestType.C);

            Assert.IsTrue(items.Contains(SortedEnumTestType.A));
            Assert.IsTrue(items.Contains(SortedEnumTestType.B));
        }

        [Test]
        public void AssertCanGetAllValuesLessThanOrEqualToC()
        {
            var items = _se.GetAllLesserThanOrEqualTo(SortedEnumTestType.C);

            Assert.IsTrue(items.Contains(SortedEnumTestType.A));
            Assert.IsTrue(items.Contains(SortedEnumTestType.B));
            Assert.IsTrue(items.Contains(SortedEnumTestType.C));
        }
    }
}
