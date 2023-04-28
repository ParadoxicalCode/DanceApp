using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class Group
    {
        [Key]
        public int ID { get; set; }

        public string? Number { get; set; }
        public string Title { get; set; }
        public string Program { get; set; }
        public string NumberOfOutputs { get; set; }
        public string DancersCount { get; set; }

        [ForeignKey("TypesOfPerformance")]
        public int TypeOfPerformanceId { get; set; }
        public virtual TypeOfPerformance TypesOfPerformance { get; set; }

        public List<Pair> Pair { get; } = new();
        public List<AgeCategoryInGroup> AgeCategoryInGroup { get; } = new();
        public List<JudgeInGroup> JudgeInGroup { get; } = new();
        public List<Performance> Performance { get; } = new();
    }
}
