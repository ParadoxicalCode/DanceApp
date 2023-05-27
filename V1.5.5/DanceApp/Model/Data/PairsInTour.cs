using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class PairsInTour
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Tour")]
        public int TourID { get; set; }
        public virtual Tour Tour { get; set; }

        [ForeignKey("Pair")]
        public int PairID { get; set; }
        public virtual Pair Pair { get; set; }
    }
}
