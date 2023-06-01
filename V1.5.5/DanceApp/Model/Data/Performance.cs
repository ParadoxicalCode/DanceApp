 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Performance
    {
        [Key]
        public int ID { get; set; }
        public int Number { get; set; }

        public List<PerformancesInDance> PerformancesInDance { get; } = new();
    }
}
