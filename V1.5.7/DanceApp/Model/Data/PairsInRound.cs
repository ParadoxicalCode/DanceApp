using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class PairsInRound
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Round")]
        public int RoundID { get; set; }
        public virtual Round Round { get; set; }

        [ForeignKey("Pair")]
        public int PairID { get; set; }
        public virtual Pair Pair { get; set; }

        public bool Select { get; set; }
    }
}
