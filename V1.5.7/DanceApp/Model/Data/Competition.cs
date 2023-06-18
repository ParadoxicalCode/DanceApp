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
        public string? Date { get; set; }
        public string? Rank { get; set; }
        public string? Manager { get; set; }
        public string? City { get; set; }
        public string? MainJudge { get; set; }
        public string? CountingCommission { get; set; }
        public int? SiteCapacity { get; set; }
        public string? Fraction { get; set; }

        public bool RegistrationStatus { get; set; }

        [ForeignKey("Round")]
        public int? RoundID { get; set; }
        public virtual Round? Round { get; set; }
    }
}
