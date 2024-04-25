using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters
{
    internal class SortProfession : IComparer<Character>
    {
        public int Compare(Character x, Character y)
        {
            return string.CompareOrdinal(x.Profession, y.Profession);
            throw new NotImplementedException();
        }
    }
}
