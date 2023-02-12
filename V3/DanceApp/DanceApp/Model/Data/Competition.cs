using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Competition
    {
        [Key]
        public int CompetitionId { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string Manager { get; set; }
        public string Address { get; set; }

        [ForeignKey("Club")]
        public int? ClubId { get; set; }
        public virtual Club? Club { get; set; }

        public string? StartTime { get; set; }
        public string? Selection { get; set; }

        public List<Dancer> Dancer { get; } = new();
        public List<Group> Group { get; } = new();
        public List<Judge> Judge { get; } = new();
        public List<Pair> Pair { get; } = new();
        public List<Performance> Performance { get; } = new();
        public List<ScoreInDance> ScoreInDances { get; } = new();
    }
}
