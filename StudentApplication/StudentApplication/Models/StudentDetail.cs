using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplication.Models
{
    public class StudentDetail
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string FatherMobileNo { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int StandardId { get; set; }
        [Required]
        public int DivisionId { get; set; }
        [Required]
        public int RollNo { get; set; }
        [Required]
        public string GRNo { get; set; }
        [Required]
        public DateTime DOB { get; set; }

        [NotMapped]
        public List<Subject> lstsub { get; set; } = new List<Subject>();
        
        [NotMapped]
        public List<Division> lstdiv { get; set; } = new List<Division>();
        [NotMapped]
        public List<Standard> lststd { get; set; } = new List<Standard>();
    }
}
