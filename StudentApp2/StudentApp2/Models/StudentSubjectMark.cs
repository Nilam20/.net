using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp2.Models
{
    public class StudentSubjectMark
    {   
        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        [Range(35, 100, ErrorMessage = "Mark must be grater than 35 and less than 100")]
        public int Mark { get; set; }
       [NotMapped]
        public List<StudentData> liststddata { get; set; } = new List<StudentData>();
        [NotMapped]
        public List<StudentData> listStd { get; set; } = new List<StudentData>();


        [NotMapped]
        public List<StudentSubject> liststdsub { get; set; } = new List<StudentSubject>();
        [NotMapped]
        public List<StudentSubjectMark> listn { get; set; } = new List<StudentSubjectMark>();
        [NotMapped]
        public List<CustomModel> AllData { get; set; } = new List<CustomModel>();


    }
}
