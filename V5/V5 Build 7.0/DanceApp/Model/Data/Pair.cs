using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class Pair
    {
        [Key]
        public int ID { get; set; }
        public string? Number { get; set; }

        public string MaleSurname { get; set; }
        public string MaleName { get; set; }
        public string? MalePatronymic { get; set; }
        public string MaleBirthday { get; set; }

        public string FemaleSurname { get; set; }
        public string FemaleName { get; set; }
        public string? FemalePatronymic { get; set; }
        public string FemaleBirthday { get; set; }

        public string PerformanceType { get; set; }
        public string Club { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Trainer1 { get; set; }
        public string Trainer2 { get; set; }
        public string AgeCategory { get; set; }

        [ForeignKey("Group")]
        public int? GroupID { get; set; }
        public virtual Group? Group { get; set; }

        public List<PairInPerformance> PairInPerformance { get; } = new();
        public List<ScoreInDance> ScoreInDances { get; } = new();
    }
}
