using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Tour
    {
        [Key]
        public int ID { get; set; }
        public string TourName { get; set; }

        public List<Performance> Performance { get; } = new();
    }
}
