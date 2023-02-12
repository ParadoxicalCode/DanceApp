using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string Title { get; set; }

        public List<City> Cities { get; } = new();
        public List<Club> Club { get; } = new();
    }
}
