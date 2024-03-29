﻿using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace DanceApp.Model.Groups
{
    public class GetPairs
    {
        public DataBaseContext db = GlobalClass.db;
        public List<Pair> Free(int RoundID)
        {
            List<Pair> freePairs = new List<Pair>();

            // Получаем ID нераспределённых пар
            var query = db.PairsInRound
                .Where(x => x.RoundID == RoundID && x.Select == false)
                .Select(x => x.PairID)
                .ToList();

            // Поиск полученных ID в таблице Pairs
            int i = 0;
            foreach (var p in query)
            {
                var pair = db.Pair.Where(u => u.ID == query[i]).FirstOrDefault();

                if (pair == null)
                    break;

                freePairs.Add(pair);
                i++;
            }
            return freePairs;
        }

        // Поиск пар, которые подходят выбранному типу выступления и возрастным категориям
        public List<ClassPairs> Get(int RoundID, string performanceType, int ageCategory1, int ageCategory2)
        {
            // Поиск всех пар в туре по ID
            List<Pair> pairsList1 = new List<Pair>();
            var pairsInRound = db.PairsInRound.Where(x => x.RoundID == RoundID).ToList();

            foreach (var p in pairsInRound)
            {
                var pair = db.Pair.Find(p.PairID);
                pairsList1.Add(pair);
            }

            // Поиск всех пар с выбранным типом выступления
            List<Pair> pairsList2 = new List<Pair>();
            pairsList2 = pairsList1.Where(x => x.PerformanceType == performanceType).ToList();
            pairsList1.Clear();

            // Поиск всех пар с выбранной возрастной категорией
            if (ageCategory2 == 0)
            {
                pairsList1 = pairsList2.Where(x => x.AgeCategoryID == ageCategory1).ToList();
            }
            else
            {
                pairsList1 = pairsList2.Where(x => x.AgeCategoryID == ageCategory1 || x.AgeCategoryID == ageCategory2).ToList();
            }

            // Поиск нераспределённых пар
            List<ClassPairs> pairsList3 = new List<ClassPairs>();
            foreach (var p in pairsList1)
            {
                var selectPair = db.PairsInRound.Where(x => x.RoundID == RoundID && x.PairID == p.ID).FirstOrDefault();
                if (selectPair.Select == false)
                {
                    var pair = new ClassPairs();

                    pair.ID = p.ID;
                    pair.Number = p.Number;
                    pair.MaleSurname = p.MaleSurname;
                    pair.MaleName = p.MaleName;
                    pair.MalePatronymic = p.MalePatronymic;
                    pair.MaleBirthday = p.MaleBirthday;
                    pair.FemaleSurname = p.FemaleSurname;
                    pair.FemaleName = p.FemaleName;
                    pair.FemalePatronymic = p.FemalePatronymic;
                    pair.FemaleBirthday = p.FemaleBirthday;
                    pair.Select = false;

                    pairsList3.Add(pair);
                }
            }
            return pairsList3.ToList();
        }

        public List<ClassPairs> Load(int groupID, string performanceType, int ageCategory1, int ageCategory2)
        {
            // У нас есть таблица PairsInGroup. Нужно найти все пары и объединить их со списком нераспределённых пар согласно условиям

            List<ClassPairs> pairsDGItems = new List<ClassPairs>();

            // Поиск выбранных пар
            var pairsInGroup = db.PairsInGroup.Where(x => x.GroupID == groupID).ToList();
            foreach (var p in pairsInGroup)
            {
                var pair = db.Pair.Find(p.PairID);

                pairsDGItems.Add(new ClassPairs
                {
                    ID = pair.ID,
                    Number = pair.Number,
                    MaleSurname = pair.MaleSurname,
                    MaleName = pair.MaleName,
                    MalePatronymic = pair.MalePatronymic,
                    MaleBirthday = pair.MaleBirthday,
                    FemaleSurname = pair.FemaleSurname,
                    FemaleName = pair.FemaleName,
                    FemalePatronymic = pair.FemalePatronymic,
                    FemaleBirthday = pair.FemaleBirthday,
                    Select = true
                });
            }

            // Поиск нераспределённых пар
            int RoundID = db.Group.Find(groupID).RoundID;
            var freePairs = Get(RoundID, performanceType, ageCategory1, ageCategory2);
            foreach (var p in freePairs)
            {
                var pair = db.Pair.Find(p.ID);

                pairsDGItems.Add(new ClassPairs
                {
                    ID = pair.ID,
                    Number = pair.Number,
                    MaleSurname = pair.MaleSurname,
                    MaleName = pair.MaleName,
                    MalePatronymic = pair.MalePatronymic,
                    MaleBirthday = pair.MaleBirthday,
                    FemaleSurname = pair.FemaleSurname,
                    FemaleName = pair.FemaleName,
                    FemalePatronymic = pair.FemalePatronymic,
                    FemaleBirthday = pair.FemaleBirthday,
                    Select = false
                });
            }
            return pairsDGItems.ToList();
        }
    }
}
