using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Tour
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        public List<Performance> Performance { get; } = new();
    }
}
