using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Dancer
    {
        [Key]
        public int DancerId { get; set; }

        [ForeignKey("Competition")]
        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }

        public string? Number { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public string Birthday { get; set; }

        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }

        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        [ForeignKey("Pair")]
        public int? PairId { get; set; }
        public virtual Pair Pair { get; set; }

        public string Trainer { get; set; }
    }
}
