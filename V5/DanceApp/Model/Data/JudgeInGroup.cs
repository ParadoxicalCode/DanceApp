using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class JudgeInGroup
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int JudgeId { get; set; }
        public Judge Judge { get; set; }
    }
}
