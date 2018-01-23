/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System.Collections.Generic;
using System.Linq;

namespace JCMG.AssetValidator.Editor.Validators.Output
{
    public class AssetValidatorLogger
    {
        private readonly IList<VLog> _logHistory;

        public AssetValidatorLogger()
        {
            _logHistory = new List<VLog>();
        }

        public void OnLogEvent(VLog log)
        {
            _logHistory.Add(log);
        }

        public void ClearLogs()
        {
            _logHistory.Clear();
        }

        public bool HasLogs()
        {
            return _logHistory.Any();
        }

        public IList<VLog> GetLogs()
        {
            return _logHistory;
        }
    }
}