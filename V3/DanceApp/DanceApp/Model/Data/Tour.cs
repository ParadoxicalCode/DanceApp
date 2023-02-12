using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Tour
    {
        public int TourId { get; set; }
        public string? Name { get; set; }

        public List<Group> Group { get; } = new();
        public List<Performance> Performance { get; } = new();
    }
}
