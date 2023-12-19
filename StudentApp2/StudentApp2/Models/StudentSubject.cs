using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp2.Models
{
    public class StudentSubject
    {   
        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
    }
}
