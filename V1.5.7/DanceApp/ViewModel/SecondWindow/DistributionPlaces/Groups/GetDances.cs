using DanceApp.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
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
                dances = db.Dance.Where(u => u.SportsDiscipline == "Европейская программа").ToList();
            }
            else
            {
                dances = db.Dance.Where(u => u.SportsDiscipline == "Латиноамериканская программа").ToList();
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

        public List<ClassDances> Load(int groupID)
        {
            // Здесь надо извлечь выбранные танцы из базы данных и объединить их с танцами из выбранной спортивной дисциплины
            // Надо узнать спортивную дисциплину и 


            List<ClassDances> dancesDGItems = new List<ClassDances>();

            var dancesInGroup = db.DancesInGroup.Where(u => u.GroupID == groupID).Select(x => x.DanceID).ToList();
            var sportsDiscipline = (db.Dance.Find(dancesInGroup[0])).SportsDiscipline;
            var allDances = db.Dance.Where(x => x.SportsDiscipline == sportsDiscipline).Select(x => x.ID).ToList();

            foreach (var danceID in allDances)
            {
                var dances = new ClassDances();

                var dance = db.Dance.Find(danceID);
                dances.Select = false;

                for (int i = 0; i < dancesInGroup.Count; i++)
                {
                    if (danceID == dancesInGroup[i])
                        dances.Select = true;
                }
                dances.ID = dance.ID;
                dances.Title = dance.Title;
                dances.ShortName = dance.ShortName;

                dancesDGItems.Add(dances);
            }
            return dancesDGItems.ToList();
        }
    }
}
