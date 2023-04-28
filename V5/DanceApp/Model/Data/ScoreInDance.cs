using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class ScoreInDance
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Pair")]
        public int PairId { get; set; }
        public virtual Pair Pair { get; set; }

        [ForeignKey("Performance")]
        public int PerformanceId { get; set; }
        public virtual Performance Performance { get; set; }

        [ForeignKey("Dance")]
        public int DanceId { get; set; }
        public virtual Dance Dance { get; set; }

        [ForeignKey("Judge")]
        public int JudgeId { get; set; }
        public virtual Judge Judge { get; set; }

        public string Score { get; set; }
    }
}
