using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class PairInPerformance
    {
        public int ID { get; set; }
        public int PerformanceId { get; set; }
        public Performance Performance { get; set; }
        public int PairId { get; set; }
        public Pair Pair { get; set; }
        public string? Disqualification { get; set; }
    }
}
