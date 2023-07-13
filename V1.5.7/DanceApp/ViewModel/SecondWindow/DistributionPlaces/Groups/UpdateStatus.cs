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
        public bool Round(int roundID)
        {
            var group = db.Group.Where(x => x.RoundID == roundID && x.Status == false).FirstOrDefault();
            int freePairs = new GetPairs().Free(roundID).Count;

            if (group == null && freePairs == 0)
            {
                return true;
            }
            db.Round.Find(roundID).Status = true;
            UpdateDataBase();
            return false;
        }

        public bool Group(int groupID)
        {
            var performancesInGroup = db.Performance.Where(x => x.GroupID == groupID);

            foreach (var p in performancesInGroup)
            {
                var performance = db.Performance.Find(p.ID);
                if (performance.Status == false)
                    return false;
            }
            db.Group.Find(groupID).Status = true;
            UpdateDataBase();
            return true;
        }

        public bool Performance(int groupID, int performanceNumber, int DanceID)
        {
            var PerformanceID = db.Performance.Where(x => x.GroupID == groupID && x.Number == performanceNumber).FirstOrDefault().ID;
            var dancesInGroup = db.DancesInGroup.Where(x => x.GroupID == groupID);

            foreach (var d in dancesInGroup)
            {
                var checkIsExistDance = db.IntermediateResult.Where(x => (x.PerformanceID == PerformanceID && x.DanceID == d.DanceID) && (x.Value != null || x.Value != "")).FirstOrDefault();

                // Нам надо удостовериться, что все танцы станцованы. Значит надо подставить такой DanceID, результата для которого ещё нет
                if (checkIsExistDance == null)
                {
                    return false;
                }
            }
            db.Performance.Find(PerformanceID).Status = true;
            UpdateDataBase();
            return true;
        }

        public bool Dance(int groupID, int performanceNumber, int danceID)
        {
            var PerformanceID = db.Performance.Where(x => x.GroupID == groupID && x.Number == performanceNumber).FirstOrDefault().ID;

            var check = db.IntermediateResult.Where(x => (x.PerformanceID == PerformanceID && x.DanceID == danceID) && (x.Value != null || x.Value != "")).FirstOrDefault();
            if (check != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateDataBase()
        {
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
        }
    }
}
