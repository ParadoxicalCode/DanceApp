using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Round
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }

        public List<Competition> Competition { get; } = new();
        public List<PairsInRound> PairsInRound { get; } = new();
        public List<Group> Group { get; } = new();
        public List<NextRound> NextRound { get; } = new();
    }
}
