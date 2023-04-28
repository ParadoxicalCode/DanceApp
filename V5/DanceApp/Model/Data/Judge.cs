using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class Judge
    {
        [Key]
        public int ID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }

        public string? Club { get; set; }

        public List<JudgeInGroup> JudgeInGroup { get; } = new();
        public List<JudgeInPerformance> JudgeInPerformance { get; } = new();
        public List<ScoreInDance> ScoreInDances { get; } = new();
    }
}
