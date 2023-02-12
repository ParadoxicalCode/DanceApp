using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model
{
    public class GlobalClass
    {
        public static string NumberOfPairs;
        public static string NumberOfJudges;
        public static int CompetitionId;

        public bool StringIsDigits(string s)
        {
            foreach (var item in s)
            {
                if (!char.IsDigit(item)) { return false; }
            }
            return true;
        }

        public static event EventHandler DataChanged;
        public static void NotifyDataChanged()
        {
            EventHandler temp = DataChanged;
            if (temp != null)
            {
                temp(null, EventArgs.Empty);
            }
        }
    }
}
