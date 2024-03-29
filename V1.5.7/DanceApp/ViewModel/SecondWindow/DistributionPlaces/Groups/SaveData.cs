﻿using DanceApp.Model.Data;
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
        private int RoundID;
        private string SportsDiscipline;
        private string PerformanceType;
        private string AgeCategory1;
        private string AgeCategory2;
        List<ClassDances> SelectedDances;
        List<ClassPairs> SelectedPairs;
        public int Save(int roundID, int groupID, string number, string sportsDiscipline, string performanceType, string ageCategory1, string ageCategory2, List<ClassDances> selectedDances, List<ClassPairs> selectedPairs)
        {
            RoundID = roundID;
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
                group.RoundID = RoundID;
                group.Number = number;
                group.Title = title;
                group.AgeCategory1 = AgeCategory1;
                group.AgeCategory2 = AgeCategory2;
                group.PerformanceType = PerformanceType;
                group.Program = program;
                group.SportsDiscipline = SportsDiscipline;
                group.PairsCount = SelectedPairs.Count;
                group.Status = false;

                db.Group.Add(group);
                try {  db.SaveChanges(); }
                catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            }
            else
            {
                return 0;
            }

            int groupID;
            if (AgeCategory2 == null || AgeCategory2 == "")
            {
                groupID = db.Group.Where(x => x.RoundID == RoundID && x.PerformanceType == PerformanceType && x.AgeCategory1 == AgeCategory1).FirstOrDefault().ID;
            }
            else
            {
                groupID = db.Group.Where(x => x.RoundID == RoundID && x.PerformanceType == PerformanceType && x.AgeCategory1 == AgeCategory1 && x.AgeCategory2 == AgeCategory2).FirstOrDefault().ID;
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
            bool checkIsExist = db.Group.Any(x => x.RoundID == (int)data.RoundID && x.Number == number);
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
            var selectDances = SelectedDances.Select(x => x.ID).ToList();
            foreach(var danceID in selectDances)
            {
                var dancesInGroup = new DancesInGroup();
                dancesInGroup.GroupID = groupID;
                dancesInGroup.DanceID = danceID;

                db.DancesInGroup.Add(dancesInGroup);
                UpdateDataBase();
            }
        }

        public void Pairs(int groupID)
        {
            // Обнуление всех пар из таблицы PairsInRound, которые присутствуют в таблице PairsInGroup
            var pairsInRound = db.PairsInRound.Where(x => x.RoundID == RoundID).ToList();
            var deletePairsInGroup = db.PairsInGroup.Where(x => x.GroupID == groupID).ToList();

            foreach (var p in pairsInRound)
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
                var pair = new PairsInGroup();

                pair.GroupID = groupID;
                pair.PairID = p.ID;

                db.PairsInGroup.Add(pair);
                UpdateDataBase();
            }

            // Отмечаем выбранные пары в таблице PairsInRound
            int i = 0;
            foreach (var p in SelectedPairs)
            {
                var pair = db.PairsInRound.Where(u => u.RoundID == RoundID && u.PairID == SelectedPairs[i].ID).FirstOrDefault();
                pair.Select = true;
                UpdateDataBase();
                i++;
            }
        }

        public bool Validation(string number, string title)
        {
            // Валидация номера
            var data = db.Competition.Find(1);

            bool checkIsExist = db.Group.Any(x => x.RoundID == data.RoundID && x.Number == number);
            if (checkIsExist == true && number != "")
            {
                MessageBox.Show("Группа с таким номером уже есть!");
                return false;
            }

            // Проверка на существование группы
            if (db.Group.Where(u => u.RoundID == data.RoundID && u.Title == title).FirstOrDefault() != null)
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
