using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace DanceApp.Model.Groups
{
    public class GetCategories
    {
        public DataBaseContext db = GlobalClass.db;

        // Поиск возрастных категорий на основании выбранного типа выступления и нераспределённых пар
        public List<AgeCategory> Get1(string performanceType, List<Pair> freePairs)
        {
            List<AgeCategory> CategoryCBItems = new List<AgeCategory>();
            var query = freePairs.Where(u => u.PerformanceType == performanceType).ToList();

            // Поиск всех уникальных возрастных категорий
            List<int> ageCategories = query.Select(x => x.AgeCategoryID).Distinct().ToList();

            // Сортировка по возрастанию
            ageCategories.Sort();

            // Замена ID на название возрастной категории
            var pairs = db.Pairs.ToList();

            foreach (var a in ageCategories)
            {
                var data = db.AgeCategories.Where(u => u.ID == a).FirstOrDefault();
                CategoryCBItems.Add(data);
            }
            return CategoryCBItems.ToList();
        }

        // Поиск возрастных категорий на основе выбранной возрастной категории
        public List<AgeCategory> Get2(string performanceType, string selectAgeCategory)
        {
            List<AgeCategory> CategoryCBItems = new List<AgeCategory>();

            switch (selectAgeCategory) // Проверить, есть ли такие пары на самом деле
            {
                case "Дети 0":
                    if (db.Pairs.Where(x => x.AgeCategoryID == 2 && x.PerformanceType == performanceType).FirstOrDefault() != null)
                        CategoryCBItems.Add(db.AgeCategories.Where(x => x.Title == "Дети 1").FirstOrDefault());
                    break;
                case "Дети 1":
                    if (db.Pairs.Where(x => x.AgeCategoryID == 3 && x.PerformanceType == performanceType).FirstOrDefault() != null)
                        CategoryCBItems.Add(db.AgeCategories.Where(x => x.Title == "Дети 2").FirstOrDefault());
                    break;
                case "Юниоры 1":
                    if (db.Pairs.Where(x => x.AgeCategoryID == 5 && x.PerformanceType == performanceType).FirstOrDefault() != null)
                        CategoryCBItems.Add(db.AgeCategories.Where(x => x.Title == "Юниоры 2").FirstOrDefault());
                    break;
                case "Юниоры 2":
                    if (db.Pairs.Where(x => x.AgeCategoryID == 7 && x.PerformanceType == performanceType).FirstOrDefault() != null)
                        CategoryCBItems.Add(db.AgeCategories.Where(x => x.Title == "Взрослые").FirstOrDefault());
                    break;
            }
            return CategoryCBItems.ToList();
        }
    }
}
