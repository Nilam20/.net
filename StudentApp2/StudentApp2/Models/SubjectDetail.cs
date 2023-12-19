using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp2.Models
{
    public class SubjectDetail
    {
        [Key]
        public int SubjectId { get; set; }
        public string Name { get; set; }
        [Range(100, Int32.MaxValue)]
        public int MaxMark { get; set; }
        [Range(35, Int32.MaxValue)]
        public int MinMark { get; set; }
    }
}
