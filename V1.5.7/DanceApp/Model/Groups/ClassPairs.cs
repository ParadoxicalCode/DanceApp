using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceApp.Model.Groups
{
    public class ClassPairs
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string MaleSurname { get; set; }
        public string MaleName { get; set; }
        public string MalePatronymic { get; set; }
        public string MaleBirthday { get; set; }
        public string FemaleSurname { get; set; }
        public string FemaleName { get; set; }
        public string FemalePatronymic { get; set; }
        public string FemaleBirthday { get; set; }
        public bool Select { get; set; }
    }
}
