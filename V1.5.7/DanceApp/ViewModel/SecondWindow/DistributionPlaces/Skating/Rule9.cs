using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace DanceApp.Model.Skating
{
    public class Rule9
    {
        private DataBaseContext db = GlobalClass.db;
        public void Calculate(int PerformanceID)
        {
            var performance = db.Performance.Find(PerformanceID);

            var pairQuery = db.PairsInPerformance.Where(x => x.PerformanceID == PerformanceID).Select(x => x.PairID).ToList();
            List<Pair> pairsInPerformance = new List<Pair>();
            foreach (var p in pairQuery)
            {
                var pair = db.Pair.Find(p);
                pairsInPerformance.Add(pair);
            }

            var danceQuery = db.DancesInGroup.Where(x => x.GroupID == performance.GroupID).Select(x => x.DanceID).ToList();
            List<Dance> dancesInGroup = new List<Dance>();
            foreach (var d in danceQuery)
            {
                var dance = db.Dance.Find(d);
                dancesInGroup.Add(dance);
            }

            // Извлечение оценок судей из базы данных
            float[,] places = new float[pairsInPerformance.Count, dancesInGroup.Count];
            for (int i = 0; i < pairsInPerformance.Count; i++)
            {
                for (int j = 0; j < dancesInGroup.Count; j++)
                {
                    var value = db.IntermediateResult.Where(x => x.PerformanceID == PerformanceID && x.PairID == pairsInPerformance[i].ID && x.DanceID == dancesInGroup[j].ID).FirstOrDefault();
                    string number;
                    if (value != null)
                    {
                        number = value.Value;
                    }
                    else
                    {
                        number = "0";
                    }
                    places[i, j] = float.Parse(number);
                }
            }

            // Подсчёт сумм мест
            var placesSum = new float[2, pairsInPerformance.Count];
            for (int i = 0; i < pairsInPerformance.Count; i++)
            {
                placesSum[0, i] = pairsInPerformance[i].ID;
            }

            for (int i = 0; i < pairsInPerformance.Count; i++)
            {
                for (int j = 0; j < dancesInGroup.Count; j++)
                {
                    placesSum[1, i] += places[i, j];
                }
            }

            // Сортировка сумм мест по возрастанию
            for (int i = 0; i < pairsInPerformance.Count; i++)
            {
                for (int j = 0; j < pairsInPerformance.Count - 1; j++)
                {
                    if (placesSum[1, j] > placesSum[1, j + 1] && placesSum[1, j + 1] != 0)
                    {
                        float temp1 = placesSum[0, j];
                        placesSum[0, j] = placesSum[0, j + 1];
                        placesSum[0, j + 1] = temp1;

                        float temp2 = placesSum[1, j];
                        placesSum[1, j] = placesSum[1, j + 1];
                        placesSum[1, j + 1] = temp2;
                    }
                }
            }

            // Присвоение мест
            for (int i = 0; i < pairsInPerformance.Count; i++)
            {
                FinalResult result = new FinalResult();
                result.PerformanceID = PerformanceID;
                result.PairID = Int32.Parse(placesSum[0, i].ToString());
                result.Value = (i + 1).ToString();

                db.FinalResult.Add(result);
            }
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
        }
    }
}
