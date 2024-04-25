using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters
{
    internal class SortSurname : IComparer<Character>
    {
        public int Compare(Character x, Character y)
        {
            return string.Compare(x.Surname, y.Surname, StringComparison.Ordinal);
            throw new NotImplementedException();
        }
    }
}
