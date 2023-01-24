using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace DanceApp.Model.Data
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerUnitials { get; set; }
        public string CityId { get; set; }
        public virtual City? City { get; set; }
    }
}
