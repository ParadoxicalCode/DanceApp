using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace DanceApp.Model.Groups
{
    public class UpdateStatus
    {
        private DataBaseContext db = GlobalClass.db;
        public string Round(int roundID)
        {
            var checkIsExistRound = db.Group.Where(x => x.RoundID == roundID && x.Status == "Не завершено").FirstOrDefault();

            if (checkIsExistRound != null)
            {
                return "Не завершено";
            }
            else
            {
                db.Round.Find(roundID).Status = "Завершено";
                UpdateDataBase();
                return "Завершено";
            }
        }

        public string Group(int groupID)
        {
            var performancesInGroup = db.Performance.Where(x => x.GroupID == groupID);

            foreach (var p in performancesInGroup)
            {
                var performance = db.Performance.Find(p.ID);
                if (performance.Status == "Не завершено")
                {
                    return "Не завершено";
                }
            }

            db.Group.Find(groupID).Status = "Завершено";
            UpdateDataBase();
            return "Завершено";
        }

        public string Performance(int groupID, int performanceNumber, int DanceID)
        {
            var PerformanceID = db.Performance.Where(x => x.GroupID == groupID && x.Number == performanceNumber).FirstOrDefault().ID;
            var dancesInGroup = db.DancesInGroup.Where(x => x.GroupID == groupID);

            foreach (var d in dancesInGroup)
            {
                var checkIsExistDance = db.IntermediateResult.Where(x => (x.PerformanceID == PerformanceID && x.DanceID == d.DanceID) && (x.Value != null || x.Value != "")).FirstOrDefault();

                // Нам надо удостовериться, что все танцы станцованы. Значит надо подставить такой DanceID, результата для которого ещё нет
                if (checkIsExistDance == null)
                {
                    return "Не завершено";
                }
            }

            db.Performance.Find(PerformanceID).Status = "Завершено";
            UpdateDataBase();
            return "Завершено";
        }

        public string Dance(int groupID, int performanceNumber, int danceID)
        {
            var PerformanceID = db.Performance.Where(x => x.GroupID == groupID && x.Number == performanceNumber).FirstOrDefault().ID;

            var check = db.IntermediateResult.Where(x => (x.PerformanceID == PerformanceID && x.DanceID == danceID) && (x.Value != null || x.Value != "")).FirstOrDefault();
            if (check != null)
            {
                return "Завершено";
            }
            else
            {
                return "Не завершено";
            }
        }

        private void UpdateDataBase()
        {
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
        }
    }
}
