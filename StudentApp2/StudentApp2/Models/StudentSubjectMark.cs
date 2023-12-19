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
        public int Mark { get; set; }
        [NotMapped]
        public List<StudentData> listsubject { get; set; } = new List<StudentData>();

        [NotMapped]
        public List<StudentSubject> liststdsub { get; set; } = new List<StudentSubject>();
    }
}
