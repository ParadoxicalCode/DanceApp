using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Performance
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Group")]
        public int GroupID { get; set; }
        public virtual Group Group { get; set; }

        public int Number { get; set; }
        public string Status { get; set; }

        public List<PairsInPerformance> PairsInPerformance { get; } = new();
        public List<FinalResult> FinalResult { get; } = new();
        public List<IntermediateResult> IntermediateResult { get; } = new();
        public List<JudgesInPerformance> JudgesInPerformance { get; } = new();
        public List<JudgesAssesment> JudgesAssesment { get; } = new();
    }
}
