using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SQLite.Data
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<City>
        Cities { get; private set; } = new ObservableCollection<City>();
    }
}