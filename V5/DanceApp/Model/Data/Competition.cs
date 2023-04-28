using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Data
{
    public class Competition
    {
        [Key]
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? StartDate { get; set; }
        public string? StartTime { get; set; }
        public string? Manager { get; set; }
        public string? Address { get; set; }
        public string? Club { get; set; }
        public int SettingsToJudges { get; set; }
        public int PairsToGroups { get; set; }
    }
}
