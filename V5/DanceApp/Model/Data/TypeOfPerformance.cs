using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceApp.Model.Data
{
    public class TypeOfPerformance
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }

        public List<Group> Group { get; } = new();
    }
}
