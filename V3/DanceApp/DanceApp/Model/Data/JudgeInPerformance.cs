using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class JudgeInPerformance
    {
        public int PerformanceId { get; set; }
        public Performance Performance { get; set; }
        public int JudgeId { get; set; }
        public Judge Judge { get; set; }
    }
}
