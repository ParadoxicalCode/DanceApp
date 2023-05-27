using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class AgeCategory
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }

        public List<Pair> Pair { get; } = new();
        public List<Group> Group { get; } = new();
    }
}
