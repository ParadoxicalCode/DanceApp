using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        public string Name { get; set; }

        public List<Dancer> Dancer { get; } = new();
    }
}
