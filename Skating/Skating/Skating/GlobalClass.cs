using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skating
{
    public class GlobalClass
    {
        public static string NumberOfPairs;
        public static string NumberOfJudges;
        public bool StringIsDigits(string s)
        {
            foreach (var item in s)
            {
                if (!char.IsDigit(item)) { return false; }       
            }return true;
        }
    }
}
