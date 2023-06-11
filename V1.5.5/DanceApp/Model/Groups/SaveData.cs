using DanceApp.Model.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace DanceApp.Model.Groups
{
    public class SaveData
    {
        public DataBaseContext db = GlobalClass.db;
        public void Save(int id, string number, string sportsDiscipline, string performanceType, string ageCategory1, string ageCategory2, List<ClassDances> selectedDances, List<ClassPairs> selectedPairs)
        {
            if (id == 0)
            {
                int groupID = AddGroup(number, sportsDiscipline, performanceType, ageCategory1, ageCategory2, selectedDances, selectedPairs);
                AddDances(groupID, sportsDiscipline, selectedDances);
                AddPairs();
            }
            else
                EditGroup();
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

        public void AddPairs()
        {
            /*
            
            // Обнуление всех пар из таблицы PairsInTour, которые присутствуют в таблице PairsInGroup
            var pastSelectPairs = db.PairsInGroup.Where(x => x.GroupID == groupID.ID).ToList();
            foreach (var p in pastSelectPairs)
            {
                var pairsInTourSelectDelete = db.PairsInTour.Where(u => u.PairID == p.PairID && u.Select == true).FirstOrDefault();
                pairsInTourSelectDelete.Select = false;
                UpdateDataBase();
            }

            // Удаление данных о выбранных парах в группе
            var deletePairsInGroup = db.PairsInGroup.Where(x => x.GroupID == groupID.ID).ToList();
            db.PairsInGroup.RemoveRange(deletePairsInGroup);

            UpdateDataBase();

            string f = (PerformanceTypeCB.SelectedItem as CBItems).Element;


            
            // Сохранение выбранных пар в таблицу PairsInGroup
                var selectPairs = selectedPairs.Select(x => x.ID).ToList();
                foreach (var p in selectPairs)
                {
                    var pair = db.Pairs.Where(u => u.ID == p).FirstOrDefault();
                    var pairsInGroup = new PairsInGroup();

                    pairsInGroup.GroupID = groupID.ID;
                    pairsInGroup.PairID = pair.ID;

                    db.PairsInGroup.Add(pairsInGroup);
                    UpdateDataBase();
                }

                // Отмечаем выбранные пары в таблице PairsInTour
                int k = 0;
                foreach (var p in selectPairs)
                {
                    var pair = db.PairsInTour.Where(u => u.TourID == (int)data.TourID && u.PairID == selectPairs[k]).FirstOrDefault();
                    pair.Select = true;
                    UpdateDataBase();
                    k++;
                }
                AddData();
                GetPairs();

            */
        }

        public void EditGroup()
        {
            // Если же изменяем данные группы, то надо проверить, не изменился ли номер или параметры группы (тип выступления, возрастные категории)
            // При изменении параметров группы или номера надо проверить 



            /*
            
            var group = db.Groups.Where(u => u.ID == ID).FirstOrDefault();

                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Запись изменена!");
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }

             */
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
