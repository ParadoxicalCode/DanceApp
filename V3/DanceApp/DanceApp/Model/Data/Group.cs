using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        [ForeignKey("Competition")]
        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }

        public string Name { get; set; }
        public string? Number { get; set; }

        [ForeignKey("Tour")]
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }

        public List<Dancer> Dancer { get; } = new();
        public List<AgeCategoryInGroup> AgeCategoryInGroup { get; } = new();
        public List<JudgeInGroup> JudgeInGroup { get; } = new();
        public List<Performance> Performance { get; } = new();
    }
}
