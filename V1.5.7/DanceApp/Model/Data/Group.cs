using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class Group
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Tour")]
        public int TourID { get; set; }
        public virtual Tour Tour { get; set; }

        public string? Number { get; set; }
        public string Title { get; set; }
        public string AgeCategory1 { get; set; }
        public string? AgeCategory2 { get; set; }
        public string PerformanceType { get; set; }
        public string Program { get; set; }
        public string SportsDiscipline { get; set; }
        public int PairsCount { get; set; }
        public string Status { get; set; }

        public List<PairsInGroup> PairsInGroup { get; } = new();
        public List<PairsInPerformance> PairsInPerformance { get; } = new();
        public List<Performance> Performance { get; } = new();
        public List<DancesInGroup> DancesInGroup { get; } = new();
        public List<JudgesInPerformance> JudgesInPerformance { get; } = new();
    }
}
