/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using JCMG.AssetValidator.Editor.Validators;
using JCMG.AssetValidator.Editor.Validators.Output;
using NUnit.Framework;
using System;

namespace JCMG.AssetValidator.Editor.CI
{
    /// <summary>
    /// These are examples of how to run the AssetValidatorCI either as a part of a Continuous Integration process
    /// or as part of Unit tests in the project.
    /// </summary>
    [TestFixture]
    public class AssetValidatorCIExamples
    {
        [Test]
        [Ignore("This is an example of how to run the AssetValidator only on Project Assets" +
                "as a Unit Test or CI process.")]
        public void RunAssetValidatorOnProjectAssetsOnly()
        {
            var result = AssetValidatorCI.RunValidation(SceneValidationMode.None, 
                                                        OutputFormat.None, 
                                                        doValidateProjectAssets:true);

            Assert.True(result.isSuccessful, result.message);
        }

        [Test]
        [Ignore("This is an example of how to run the AssetValidator on Project Assets and on Build and AssetBundle Scenes" +
                "and write out the results to a datetime formatted html log as a Unit Test or CI process.")]
        public void RunAssetValidatorOnProjectAssets_BuildAndAssetBundleScenes()
        {
            var loggerFileName = string.Format("asset_validator_results_{0:h_mm_ss_MM_dd_yyyy}", DateTime.Now);
            var result = AssetValidatorCI.RunValidation(SceneValidationMode.AllBuildAndAssetBundleScenes, 
                                                        OutputFormat.Html,
                                                        doValidateProjectAssets:true,
                                                        fileName: loggerFileName);

            Assert.True(result.isSuccessful, result.message);
        }
    }
}