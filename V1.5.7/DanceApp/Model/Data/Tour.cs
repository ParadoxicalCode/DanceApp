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
        public string Title { get; set; }
        public string Status { get; set; }

        public List<Competition> Competition { get; } = new();
        public List<PairsInTour> PairsInTour { get; } = new();
        public List<Group> Group { get; } = new();
    }
}
