using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class Pair
    {
        [Key]
        public int PairId { get; set; }
        public string Number { get; set; }

        [ForeignKey("Competition")]
        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }
        
        public List<PairInPerformance> PairInPerformance { get; } = new();
        public List<ScoreInDance> ScoreInDances { get; } = new();
    }
}
