using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Club
    {
        [Key]
        public int ClubId { get; set; }
        public string Title { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public List<Competition>? Competition { get; } = new();
        public List<Dancer> Dancer { get; } = new();
    }
}
