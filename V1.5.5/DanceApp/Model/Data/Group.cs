using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class Group
    {
        [Key]
        public int ID { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string PerformanceType { get; set; }
        public string Program { get; set; }
        public int PairsCount { get; set; }

        public List<Performance> Performance { get; } = new();
        public List<PairsInGroup> PairsInGroup { get; } = new();
    }
}
