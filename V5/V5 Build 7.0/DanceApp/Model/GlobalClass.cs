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
        public static int NumberOfPairs = 10;
        public static int NumberOfJudges = 7;
        public static int CompetitionId;
        public static string dataBaseName;
        public static DateTime competitionDate = new DateTime(2023, 05, 06);

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

        public static DataBaseContext db;

    }
}
