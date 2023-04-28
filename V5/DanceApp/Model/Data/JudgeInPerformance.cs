using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class JudgeInPerformance
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Performance")]
        public int PerformanceId { get; set; }
        public Performance Performance { get; set; }
        public int JudgeId { get; set; }
        public Judge Judge { get; set; }
    }
}
