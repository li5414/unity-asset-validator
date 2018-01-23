/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace JCMG.AssetValidator.Editor.Meta
{
    public class SortedEnum<T>
    {
        private readonly IList<T> _allTypeValues;

        public SortedEnum()
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("Generic type T must be an enum.");

            _allTypeValues = (T[])Enum.GetValues(typeof(T));
        }

        public IList<T> GetAllGreaterThan(T type)
        {
            var typeVal = Convert.ToInt32(type);
            return _allTypeValues.Where(x => typeVal < Convert.ToInt32(x)).ToList();
        }

        public IList<T> GetAllGreaterThanOrEqualTo(T type)
        {
            var typeVal = Convert.ToInt32(type);
            return _allTypeValues.Where(x => typeVal <= Convert.ToInt32(x)).ToList();
        }

        public IList<T> GetAllLesserThan(T type)
        {
            var typeVal = Convert.ToInt32(type);
            return _allTypeValues.Where(x => typeVal > Convert.ToInt32(x)).ToList();
        }

        public IList<T> GetAllLesserThanOrEqualTo(T type)
        {
            var typeVal = Convert.ToInt32(type);
            return _allTypeValues.Where(x => typeVal >= Convert.ToInt32(x)).ToList();
        }

        public IList<T> GetAllValues()
        {
            return new List<T>(_allTypeValues);
        }
    }
}
