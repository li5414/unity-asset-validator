/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System.Collections.Generic;
using System.Diagnostics;

namespace JCMG.AssetValidator.Editor.Utility
{
    public static class CommandLineUtility
    {
        public static Dictionary<string, string> GetNamedCommandlineArguments(char delimiter)
        {
            var dict = new Dictionary<string, string>();
            var args = System.Environment.GetCommandLineArgs();
            for (var i = 0; i < args.Length; i++)
            {
                var splitArg = args[i].Split(delimiter);

                if(splitArg.Length <= 1) continue;

                dict.Add(splitArg[0], splitArg[1]);
            }

            return dict;
        }
    }
}