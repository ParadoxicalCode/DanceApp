using DanceApp.Model.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace DanceApp.Model.Groups
{
    public class SaveData
    {
        public DataBaseContext db = GlobalClass.db;
        private int TourID;
        private string SportsDiscipline;
        private string PerformanceType;
        private string AgeCategory1;
        private string AgeCategory2;
        List<ClassDances> SelectedDances;
        List<ClassPairs> SelectedPairs;
        public int Save(int tourID, int groupID, string number, string sportsDiscipline, string performanceType, string ageCategory1, string ageCategory2, List<ClassDances> selectedDances, List<ClassPairs> selectedPairs)
        {
            TourID = tourID;
            SportsDiscipline = sportsDiscipline;
            PerformanceType = performanceType;
            AgeCategory1 = ageCategory1;
            AgeCategory2 = ageCategory2;
            SelectedDances = selectedDances;
            SelectedPairs = selectedPairs;

            if (groupID == 0)
            {
                groupID = AddGroup(number);
                if (groupID == 0)
                {
                    return 0;
                }
                AddDances(groupID);
                Pairs(groupID);
            }
            else
            {
                var flag = EditGroup(groupID, number);
                if (flag == false)
                {
                    return 0;
                }
                AddDances(groupID);
                Pairs(groupID);
            }
            return groupID;
        }

        public int AddGroup(string number)
        {
            var title = AgeCategory1 + AgeCategory2;
            if (PerformanceType == "Соло")
                title += " (" + PerformanceType + ")";

            if (Validation(number, title) == true)
            {
                string program;
                if (SelectedDances.Count == 1)
                    program = "1 танец";
                else if (SelectedDances.Count < 5)
                    program = SelectedDances.Count.ToString() + " танца";
                else
                    program = "5 танцев";

                var group = new Data.Group();
                group.TourID = TourID;
                group.Number = number;
                group.Title = title;
                group.AgeCategory1 = AgeCategory1;
                group.AgeCategory2 = AgeCategory2;
                group.PerformanceType = PerformanceType;
                group.Program = program;
                group.SportsDiscipline = SportsDiscipline;
                group.PairsCount = SelectedPairs.Count;
                group.Status = "Не завершено";

                db.Group.Add(group);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Запись добавлена!");
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            }
            else
            {
                return 0;
            }

            int groupID;
            if (AgeCategory2 == null || AgeCategory2 == "")
            {
                groupID = db.Group.Where(x => x.TourID == TourID && x.PerformanceType == PerformanceType && x.AgeCategory1 == AgeCategory1).FirstOrDefault().ID;
            }
            else
            {
                groupID = db.Group.Where(x => x.TourID == TourID && x.PerformanceType == PerformanceType && x.AgeCategory1 == AgeCategory1 && x.AgeCategory2 == AgeCategory2).FirstOrDefault().ID;
            }
            return groupID;
        }

        public bool EditGroup(int groupID, string number)
        {
            // Если изменяем данные группы, то надо проверить, не изменился ли номер или параметры группы (тип выступления, возрастные категории)
            // При изменении параметров группы или номера надо проверить 

            var competition = db.Competition.Find(1);
            var group = db.Group.Find(groupID);

            var title = AgeCategory1 + AgeCategory2;
            if (PerformanceType == "Соло")
                title += " (" + PerformanceType + ")";

            if (group.Title != title)
            {
                MessageBox.Show("Группа с выбранными параметрами уже есть!");
                return false;
            }

            var data = db.Competition.Find(1);
            bool checkIsExist = db.Group.Any(x => x.TourID == (int)data.TourID && x.Number == number);
            if (group.Number != number && checkIsExist == true)
            {
                MessageBox.Show("Группа с таким номером уже есть!");
                return false;
            }

            string program;
            if (SelectedDances.Count == 1)
                program = "1 танец";
            else if (SelectedDances.Count < 5)
                program = SelectedDances.Count.ToString() + " танца";
            else
                program = "5 танцев";

            group.Number = number;
            group.Title = title;
            group.AgeCategory1 = AgeCategory1;
            group.AgeCategory2 = AgeCategory2;
            group.PerformanceType = PerformanceType;
            group.Program = program;
            group.SportsDiscipline = SportsDiscipline;
            group.PairsCount = SelectedPairs.Count;

            try
            {
                db.SaveChanges();
                MessageBox.Show("Запись изменена!");
                return true;
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.InnerException.Message);
                return false;
            }
        }

        public void AddDances(int groupID)
        {
            var data = db.Group.Find(groupID);

            // Удаление данных о выбранных танцах в группе
            var deleteDances = db.DancesInGroup.Where(x => x.GroupID == groupID).ToList();
            db.DancesInGroup.RemoveRange(deleteDances);

            // Сохранение выбранных танцев в базу данных
            var selectDance = SelectedDances.Select(x => x.ID).ToList();

            var allDances = db.Dance.Where(u => u.SportsDiscipline == SportsDiscipline).ToList();
            foreach (var d in allDances)
            {
                bool select = false;
                for (int i = 0; i < selectDance.Count; i++)
                {
                    if (d.ID == selectDance[i])
                        select = true;
                }

                var dancesInGroup = new DancesInGroup();
                dancesInGroup.GroupID = groupID;
                dancesInGroup.DanceID = d.ID;
                dancesInGroup.Select = select;

                db.DancesInGroup.Add(dancesInGroup);
                UpdateDataBase();
            }
        }

        public void Pairs(int groupID)
        {
            // Обнуление всех пар из таблицы PairsInTour, которые присутствуют в таблице PairsInGroup
            var pairsInTour = db.PairsInTour.Where(x => x.TourID == TourID).ToList();
            var deletePairsInGroup = db.PairsInGroup.Where(x => x.GroupID == groupID).ToList();

            foreach (var p in pairsInTour)
            {
                for (int k = 0; deletePairsInGroup.Count > 0 && k < deletePairsInGroup.Count; k++)
                {
                    if (p.PairID == deletePairsInGroup[k].PairID)
                    {
                        p.Select = false;
                        UpdateDataBase();
                    }
                }
            }

            // Удаление данных о выбранных парах в группе
            if (deletePairsInGroup.Count > 0)
            {
                db.PairsInGroup.RemoveRange(deletePairsInGroup);
                UpdateDataBase();
            }

            // Сохранение выбранных пар в таблицу PairsInGroup
            foreach (var p in SelectedPairs)
            {
                var pairsInGroup = new PairsInGroup();

                pairsInGroup.GroupID = groupID;
                pairsInGroup.PairID = p.ID;

                db.PairsInGroup.Add(pairsInGroup);
                UpdateDataBase();
            }

            // Отмечаем выбранные пары в таблице PairsInTour
            int i = 0;
            foreach (var p in SelectedPairs)
            {
                var pair = db.PairsInTour.Where(u => u.TourID == TourID && u.PairID == SelectedPairs[i].ID).FirstOrDefault();
                pair.Select = true;
                UpdateDataBase();
                i++;
            }
        }

        public bool Validation(string number, string title)
        {
            // Валидация номера
            var data = db.Competition.Find(1);

            bool checkIsExist = db.Group.Any(x => x.TourID == data.TourID && x.Number == number);
            if (checkIsExist == true && number != "")
            {
                MessageBox.Show("Группа с таким номером уже есть!");
                return false;
            }

            // Проверка на существование группы
            if (db.Group.Where(u => u.TourID == data.TourID && u.Title == title).FirstOrDefault() != null)
            {
                MessageBox.Show("Группа с выбранными параметрами уже есть!");
                return false;
            }
            return true;
        }

        private void UpdateDataBase()
        {
            try { db.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
        }
    }
}
