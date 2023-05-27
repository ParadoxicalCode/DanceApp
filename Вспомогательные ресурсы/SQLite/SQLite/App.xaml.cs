using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Windows;

namespace SQLite
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            DatabaseFacade facade = new DatabaseFacade(new Data.AppContext());
            facade.EnsureCreated();
        }
    }
}
