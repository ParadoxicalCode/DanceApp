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
                    data.CurrentTour = null;
                    try 
                    {
                        // Удаление всех туров, групп и заходов
                        var performances = db.Performances.ToList();
                        db.Performances.RemoveRange(performances);

                        var groups = db.Groups.ToList();
                        db.Groups.RemoveRange(groups);

                        var tours = db.Tours.ToList();
                        db.Tours.RemoveRange(tours);

                        try { db.SaveChanges(); }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                        
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
