using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class JudgesAssesment
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Performance")]
        public int PerformanceID { get; set; }
        public virtual Performance Performance { get; set; }

        [ForeignKey("Dance")]
        public int DanceID { get; set; }
        public virtual Dance Dance { get; set; }

        [ForeignKey("Judge")]
        public int JudgeID { get; set; }
        public virtual Judge Judge { get; set; }

        [ForeignKey("Pair")]
        public int PairID { get; set; }
        public virtual Pair Pair { get; set; }

        public string Value { get; set; }
    }
}
