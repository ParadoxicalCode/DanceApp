using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class PairsInPerformance
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Performance")]
        public int PerformanceID { get; set; }
        public virtual Performance Performance { get; set; }

        [ForeignKey("Pair")]
        public int PairID { get; set; }
        public virtual Pair Pair { get; set; }
    }
}
