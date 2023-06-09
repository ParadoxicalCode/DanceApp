using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class PairsInGroup
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Group")]
        public int GroupID { get; set; }
        public virtual Group Group { get; set; }

        [ForeignKey("Pair")]
        public int PairID { get; set; }
        public virtual Pair Pair { get; set; }
    }
}
