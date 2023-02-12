using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class Judge
    {
        [Key]
        public int JudgeId { get; set; }

        //[ForeignKey("Competition")]
        //public int CompetitionId { get; set; }
        //public virtual Competition Competition { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }

        public List<JudgeInGroup> JudgeInGroup { get; } = new();
        public List<JudgeInPerformance> JudgeInPerformance { get; } = new();
        public List<ScoreInDance> ScoreInDances { get; } = new();
    }
}
