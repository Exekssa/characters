using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters
{
    internal class SortByYear : IComparer<Character>
    {
        public int Compare(Character x, Character y)
        {
            return x.DateOfBirth.Year.CompareTo(y.DateOfBirth.Year);
        }
    }
}
