using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class JudgesInPerformance
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Group")]
        public int GroupID { get; set; }
        public virtual Group Group { get; set; }

        [ForeignKey("Dance")]
        public int DanceID { get; set; }
        public virtual Dance Dance { get; set; }

        public int PerformanceNumber { get; set; }

        [ForeignKey("Judge")]
        public int JudgeID { get; set; }
        public virtual Judge Judge { get; set; }
    }
}
