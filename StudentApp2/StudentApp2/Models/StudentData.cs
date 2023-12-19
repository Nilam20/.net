using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp2.Models
{
    public class StudentData
    {   [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Standard { get; set; }
        public string FatherName { get; set; }
        public int RollNo { get; set; }
        public string GRNO { get; set; }
        public double Percentage { get; set; }
    }
}
