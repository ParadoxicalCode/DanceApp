using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class NextRound
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Round")]
        public int RoundID { get; set; }
        public virtual Round Round { get; set; }

        [ForeignKey("Performance")]
        public int PerformanceID { get; set; }
        public virtual Performance Performance { get; set; }

        [ForeignKey("Judge")]
        public int JudgeID { get; set; }
        public virtual Judge Judge { get; set; }

        public bool Select { get; set; }
    }
}
