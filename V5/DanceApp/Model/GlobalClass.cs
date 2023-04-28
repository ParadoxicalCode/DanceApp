using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model
{
    public class GlobalClass
    {
        public static int NumberOfPairs = 10;
        public static int NumberOfJudges = 7;
        public static int CompetitionId;
        public static bool SettingsToJudges;

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
