using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class PerformancesInDance
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Dance")]
        public int DanceID { get; set; }
        public virtual DancesInGroup DanceInGroup { get; set; }

        [ForeignKey("Performance")]
        public int PerformanceID { get; set; }
        public virtual Performance Performance { get; set; }
    }
}
