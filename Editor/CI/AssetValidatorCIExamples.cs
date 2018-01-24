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