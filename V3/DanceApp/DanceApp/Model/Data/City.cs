using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Title { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public List<Club> Club { get; } = new();
    }
}
