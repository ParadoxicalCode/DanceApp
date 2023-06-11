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
        public List<Pair> Free()
        {
            List<Pair> freePairs = new List<Pair>();

            // Получаем ID нераспределённых пар
            var query = db.PairsInTour
                .Where(x => x.Select == false)
                .Select(x => x.PairID)
                .ToList();

            // Поиск полученных ID в таблице Pairs
            int i = 0;
            foreach (var p in query)
            {
                var pair = db.Pairs.Where(u => u.ID == query[i]).FirstOrDefault();

                if (pair == null)
                    break;

                freePairs.Add(pair);
                i++;
            }
            return freePairs;
        }

        // Поиск пар, которые подходят выбранному типу выступления и возрастным категориям
        public List<ClassPairs> Get(string performanceType, int ageCategory1, int ageCategory2)
        {
            var competition = db.Competitions.Find(1);

            // Поиск всех пар в туре по ID
            List<Pair> pairsList1 = new List<Pair>();
            var pairsInTour = db.PairsInTour.Where(x => x.TourID == competition.TourID).ToList();

            foreach (var p in pairsInTour)
            {
                var pair = db.Pairs.Find(p.PairID);
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
                var selectPair = db.PairsInTour.Find(p.ID);
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

        public List<ClassPairs> Load(string selectAgeCategory, string performanceType)
        {
            // Привязка данных к PairsDG
            //var data = db.AgeCategories.Where(u => u.Title == selectAgeCategory).FirstOrDefault();
            //var pairs = db.Pairs.Where(x => x.PerformanceType == performanceType && x.AgeCategoryID == data.ID).ToList();



            List<ClassPairs> pairsDGItems = new List<ClassPairs>();

            int i = 0;
            foreach (var p in freePairs)
            {
                var query = freePairs.Select(x => x.ID).ToList();
                var data = db.PairsInGroup.Where(u => u.PairID == query[i]).FirstOrDefault();

                pairsDGItems.Add(new ClassPairs
                {
                    ID = p.ID,
                    Number = p.Number,
                    MaleSurname = p.MaleSurname,
                    MaleName = p.MaleName,
                    MalePatronymic = p.MalePatronymic,
                    MaleBirthday = p.MaleBirthday,
                    FemaleSurname = p.FemaleSurname,
                    FemaleName = p.FemaleName,
                    FemalePatronymic = p.FemalePatronymic,
                    FemaleBirthday = p.FemaleBirthday, // Нужно узнать, какие пары у нас выбраны
                    //Select = 
                });
                i++;
            }
            return pairsDGItems.ToList();
        }
    }
}
