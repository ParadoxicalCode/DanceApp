using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class City
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string CityId { get; set; }
        public ICollection<Competition> Compititions { get; set; }
    }
}
