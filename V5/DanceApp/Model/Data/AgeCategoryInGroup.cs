using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class AgeCategoryInGroup
    {
        [Key]
        public int ID { get; set; }
        public Group Group { get; set; }

        [ForeignKey("AgeCategory")]
        public int AgeCategoryId { get; set; }
        public virtual AgeCategory AgeCategory { get; set; }
    }
}
