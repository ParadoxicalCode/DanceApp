using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows;

namespace DanceApp.Model.Data
{
    public class DBInteraction
    {
        public bool CreateCompetition(string name, string managerUnitials, string city)
        {
            bool result = false;
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Competitions.Any(x => x.Name == name);
                if (!checkIsExist)
                {
                    Competition newCompetition = new Competition();
                    newCompetition.Name = name;
                    newCompetition.ManagerUnitials = managerUnitials;
                    newCompetition.CityId = city;

                    //try
                    //{
                        db.Competitions.Add(newCompetition);
                        db.SaveChanges();
                        result = true;
                    //}
                    //catch (Exception ex)
                    //{
                        //MessageBox.Show(ex.Message.ToString());
                    //}

                }
                return result;
            }
        }

        public string CreateCity(string name)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Cities.Any(el => el.CityId == name);
                if (!checkIsExist)
                {
                    try
                    {
                        City newCity = new City { CityId = name };
                        db.Cities.Add(newCity);
                        db.SaveChanges();
                        result = "Запись успешно добавлена!";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                return result;
            }
        }
    }
}
