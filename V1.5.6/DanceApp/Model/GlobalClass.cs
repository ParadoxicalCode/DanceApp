using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model
{
    public class GlobalClass
    {
        public static int NumberOfPairs = 8;
        public static int NumberOfJudges = 5;
        public static string dataBaseName;
        public static DataBaseContext db;

        public bool TrimAndCheckNumber(ref string str)
        {
            str = str.Trim();
            int number;
            if (int.TryParse(str, out number))
                return true;
            else
                return false;
        }
    }
}
