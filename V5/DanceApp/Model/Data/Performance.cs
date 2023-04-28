using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class Performance
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        [ForeignKey("Tour")]
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }

        public string Number { get; set; }

        [ForeignKey("Dance")]
        public int DanceId { get; set; }
        public virtual Dance Dance { get; set; }

        public List<JudgeInPerformance> JudgeInPerformance { get; } = new();
        public List<ScoreInDance> ScoreInDances { get; } = new();
    }
}
