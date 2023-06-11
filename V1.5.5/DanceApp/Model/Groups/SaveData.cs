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
        public void Save(int groupID, string number, string sportsDiscipline, string performanceType, string ageCategory1, string ageCategory2, List<ClassDances> selectedDances, List<ClassPairs> selectedPairs)
        {
            if (groupID == 0)
            {
                int newGroupID = AddGroup(number, sportsDiscipline, performanceType, ageCategory1, ageCategory2, selectedDances, selectedPairs);
                AddDances(newGroupID, sportsDiscipline, selectedDances);
                AddPairs(newGroupID, selectedPairs);
            }
            else
            {
                EditGroup(groupID, number, sportsDiscipline, performanceType, ageCategory1, ageCategory2, selectedDances, selectedPairs);
                AddDances(groupID, sportsDiscipline, selectedDances);
                AddPairs(groupID, selectedPairs);
            } 
        }

        public int AddGroup(string number, string sportsDiscipline, string performanceType, string ageCategory1, string ageCategory2, List<ClassDances> selectedDances, List<ClassPairs> selectedPairs)
        {
            var data = db.Competitions.Find(1);

            var title = ageCategory1 + ageCategory2;
            if (performanceType == "Соло")
                title += " (" + performanceType + ")";

            if (Validation(number, title) == true)
            {
                string program;
                if (selectedDances.Count == 1)
                    program = "1 танец";
                else if (selectedDances.Count < 5)
                    program = selectedDances.Count.ToString() + " танца";
                else
                    program = "5 танцев";

                var group = new Data.Group();
                group.TourID = (int)data.TourID;
                group.Number = number;
                group.Title = title;
                group.AgeCategory1 = ageCategory1;
                group.AgeCategory2 = ageCategory2;
                group.PerformanceType = performanceType;
                group.Program = program;
                group.SportsDiscipline = sportsDiscipline;
                group.PairsCount = selectedPairs.Count;

                db.Groups.Add(group);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Запись добавлена!");
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            }
            var groupID = db.Groups.Where(x => x.TourID == data.TourID && x.Number == number).FirstOrDefault().ID;
            return groupID;
        }

        public void EditGroup(int groupID, string number, string sportsDiscipline, string performanceType, string ageCategory1, string ageCategory2, List<ClassDances> selectedDances, List<ClassPairs> selectedPairs)
        {
            // Если изменяем данные группы, то надо проверить, не изменился ли номер или параметры группы (тип выступления, возрастные категории)
            // При изменении параметров группы или номера надо проверить 

            var competition = db.Competitions.Find(1);
            var group = db.Groups.Find(groupID);

            var title = ageCategory1 + ageCategory2;
            if (performanceType == "Соло")
                title += " (" + performanceType + ")";

            if (group.Title != title)
            {
                MessageBox.Show("Группа с выбранными параметрами уже есть!");
                return;
            }

            if (group.Number != number)
            {
                MessageBox.Show("Группа с таким номером уже есть!");
                return;
            }

            string program;
            if (selectedDances.Count == 1)
                program = "1 танец";
            else if (selectedDances.Count < 5)
                program = selectedDances.Count.ToString() + " танца";
            else
                program = "5 танцев";

            group.Number = number;
            group.Title = title;
            group.AgeCategory1 = ageCategory1;
            group.AgeCategory2 = ageCategory2;
            group.PerformanceType = performanceType;
            group.Program = program;
            group.SportsDiscipline = sportsDiscipline;
            group.PairsCount = selectedPairs.Count;

            try
            {
                db.SaveChanges();
                MessageBox.Show("Запись изменена!");
            }
            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
        }

        public void AddDances(int groupID, string sportsDiscipline, List<ClassDances> selectedDances)
        {
            var data = db.Groups.Find(groupID);

            // Удаление данных о выбранных танцах в группе
            var deleteDances = db.DancesInGroup.Where(x => x.GroupID == groupID).ToList();
            db.DancesInGroup.RemoveRange(deleteDances);

            // Сохранение выбранных танцев в базу данных
            var selectDance = selectedDances.Select(x => x.ID).ToList();

            var allDances = db.Dances.Where(u => u.SportsDiscipline == sportsDiscipline).ToList();
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

        public void AddPairs(int groupID, List<ClassPairs> selectedPairs)
        {
            var data = db.Competitions.Find(1);

            // Обнуление всех пар из таблицы PairsInTour, которые присутствуют в таблице PairsInGroup
            var pastSelectPairs = db.PairsInTour.Where(x => x.TourID == data.TourID).ToList();
            foreach (var p in pastSelectPairs)
            {
                p.Select = false;
                UpdateDataBase();
            }

            // Удаление данных о выбранных парах в группе
            var deletePairsInGroup = db.PairsInGroup.Where(x => x.GroupID == groupID).ToList();
            db.PairsInGroup.RemoveRange(deletePairsInGroup);
            UpdateDataBase();

            // Сохранение выбранных пар в таблицу PairsInGroup
            foreach (var p in selectedPairs)
            {
                var pairsInGroup = new PairsInGroup();

                pairsInGroup.GroupID = groupID;
                pairsInGroup.PairID = p.ID;

                db.PairsInGroup.Add(pairsInGroup);
                UpdateDataBase();
            }

            // Отмечаем выбранные пары в таблице PairsInTour
            int i = 0;
            foreach (var p in selectedPairs)
            {
                var pair = db.PairsInTour.Where(u => u.TourID == (int)data.TourID && u.PairID == selectedPairs[i].ID).FirstOrDefault();
                pair.Select = true;
                UpdateDataBase();
                i++;
            }
        }

        public bool Validation(string number, string title)
        {
            // Валидация номера
            var data = db.Competitions.Find(1);

            bool checkIsExist = db.Groups.Any(x => x.TourID == data.TourID && x.Number == number);
            if (checkIsExist == true && number != "")
            {
                MessageBox.Show("Пара с таким номером уже есть!");
                return false;
            }

            // Проверка на существование группы
            if (db.Groups.Where(u => u.TourID == data.TourID && u.Title == title).FirstOrDefault() != null)
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
