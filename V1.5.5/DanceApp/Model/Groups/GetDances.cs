using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Groups
{
    public class GetDances
    {
        public DataBaseContext db = GlobalClass.db;
        public List<ClassDances> Add(int selectedIndex)
        {
            List<Dance> dances = new List<Dance>();
            if (selectedIndex == 0)
            {
                dances = db.Dances.Where(u => u.SportsDiscipline == "Европейская программа").ToList();
            }
            else
            {
                dances = db.Dances.Where(u => u.SportsDiscipline == "Латиноамериканская программа").ToList();
            }

            // Привязка данных к DancesDG
            List<ClassDances> DancesDGItems = new List<ClassDances>();

            foreach (var d in dances)
            {
                var dance = new ClassDances();
                dance.ID = d.ID;
                dance.Title = d.Title;
                dance.ShortName = d.ShortName;
                dance.Select = false;

                DancesDGItems.Add(dance);
            }
            return DancesDGItems.ToList();
        }

        public List<ClassDances> Load()
        {
            List<ClassDances> dancesDGItems = new List<ClassDances>();

            foreach (var d in db.DancesInGroup)
            {
                var dance = db.Dances.Where(u => u.ID == d.ID).FirstOrDefault();
                var dances = new ClassDances();
                dances.ID = dance.ID;
                dances.Title = dance.Title;
                dances.ShortName = dance.ShortName;
                dances.Select = d.Select;

                dancesDGItems.Add(dances);
            }
            return dancesDGItems.ToList();
        }
    }
}
