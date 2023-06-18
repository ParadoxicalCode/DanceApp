using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class Judge
    {
        [Key]
        public int ID { get; set; }
        public char Character { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public string? Club { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public List<JudgesInPerformance> JudgesInPerformance { get; } = new();
        public List<IntermediateResult> IntermediateResult { get; } = new();
        public List<JudgesAssesment> JudgesAssesment { get; } = new();
        public List<NextRound> NextRound { get; } = new();
    }
}
