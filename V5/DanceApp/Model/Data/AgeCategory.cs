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
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public List<AgeCategoryInGroup> AgeCategoryInGroup { get; } = new();
    }
}
