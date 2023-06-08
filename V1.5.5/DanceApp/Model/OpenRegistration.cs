using DanceApp.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DanceApp.Model
{
    #nullable disable
    public class OpenRegistration
    {
        public DataBaseContext db = GlobalClass.db;
        public bool Delete()
        {
            var data = db.Competitions.Where(u => u.ID == 1).FirstOrDefault();
            if (data.RegistrationStatus == false)
            {
                if (MessageBox.Show("При открытии регистрации удалятся все данные о группах, результатах танцев и финальный отчёт. Вы уверены?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    data.RegistrationStatus = true;
                    data.TourID = null;

                    var pairsInTour = db.PairsInTour.ToList();
                    db.PairsInTour.RemoveRange(pairsInTour);

                    var pairsInGroup = db.PairsInGroup.ToList();
                    db.PairsInGroup.RemoveRange(pairsInGroup);

                    var dancesInGroup = db.DancesInGroup.ToList();
                    db.DancesInGroup.RemoveRange(dancesInGroup);

                    var groups = db.Groups.ToList();
                    db.Groups.RemoveRange(groups);

                    var tours = db.Tours.ToList();
                    db.Tours.RemoveRange(tours);

                    try 
                    { 
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception ex) 
                    { 
                        MessageBox.Show(ex.Message.ToString());
                        return false;
                    }
                }
                else
                    return false;
            }
            else
                return true;
        }
    }
}
