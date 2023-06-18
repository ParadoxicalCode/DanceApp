using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Dance
    {
        [Key]
        public int ID { get; set; }

        public string SportsDiscipline { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }

        public List<DancesInGroup> DancesInGroup { get; } = new();
        public List<IntermediateResult> IntermediateResult { get; } = new();
        public List<JudgesAssesment> JudgesAssesment { get; } = new();
        public List<PairsInPerformance> PairsInPerformance { get; } = new();
        public List<JudgesInDance> JudgesInDance { get; } = new();
    }
}
