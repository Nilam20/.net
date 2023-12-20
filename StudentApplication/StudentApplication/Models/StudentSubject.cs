using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplication.Models
{
    public class StudentSubject
    {   [Key]
        public int StudentSubjectId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
    }
}
