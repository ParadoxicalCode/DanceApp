using DanceApp.Model.Data;
using DanceApp.Model.Groups;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace DanceApp.Model.Performances
{
    public class PairsToPerformances
    {
        public DataBaseContext db = GlobalClass.db;
        public void Distribution(int GroupID, List<ClassDances> selectedDances, List<ClassPairs> selectedPairs)
        {
            int performanceCount = Add(GroupID, selectedDances, selectedPairs);

            // Перемешивание пар
            var random = new Random();
            for (int i = selectedPairs.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                var temp = selectedPairs[j];
                selectedPairs[j] = selectedPairs[i];
                selectedPairs[i] = temp;
            }

            for (int i = 0; i < selectedDances.Count; i++)
            {
                for (int j = 1; j <= selectedPairs.Count;)
                {
                    for (int h = 1; h <= performanceCount; h++)
                    {
                        var performance = db.Performance.Where(x => x.GroupID == GroupID && x.DanceID == selectedDances[i].ID && 
                            x.Number == h).FirstOrDefault();

                        // Если не получается равномерно разделить пары по заходам
                        if (j > selectedPairs.Count)
                        {
                            break;
                        }

                        var pair = new PairsInPerformance();
                        pair.PerformanceID = performance.ID;
                        pair.PairID = selectedPairs[j - 1].ID;
                        pair.Select = false;

                        db.PairsInPerformance.Add(pair);
                        UpdateDataBase();
                        j++;
                    }
                }
            }
        }

        public int Add(int GroupID, List<ClassDances> selectedDances, List<ClassPairs> selectedPairs)
        {
            // Удаление заходов
            var performancesInGroup = db.Performance.Where(x => x.GroupID == GroupID).ToList();
            db.Performance.RemoveRange(performancesInGroup);

            // Создание заходов
            var siteCapacity = (db.Competition.Find(1)).SiteCapacity;
            int performanceCount = (int)Math.Ceiling((float)selectedPairs.Count / (float)siteCapacity);

            for (int i = 0; i < selectedDances.Count; i++)
            {
                for (int j = 1; j <= performanceCount; j++)
                {
                    var performance = new Performance();

                    performance.GroupID = GroupID;
                    performance.DanceID = selectedDances[i].ID;
                    performance.Number = j;
                    performance.Status = "Не завершено";

                    db.Performance.Add(performance);
                    UpdateDataBase();
                }
            }
            return performanceCount;
        }

        public void UpdateDataBase()
        {
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
        }
    }
}
