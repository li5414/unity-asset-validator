/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;
using JCMG.AssetValidator.Editor.Validators.Output;

namespace JCMG.AssetValidator.Editor.Utility
{
    public static class OutputFormatUtility
    {
        public static string GetOutputFormatExtension(OutputFormat format)
        {
            switch (format)
            {
                case OutputFormat.None:
                    return string.Empty;
                case OutputFormat.Html:
                    return ".html";
                case OutputFormat.Csv:
                    return ".csv";
                case OutputFormat.Text:
                    return ".txt";
                default:
                    return string.Empty;
            }
        }
    }
}