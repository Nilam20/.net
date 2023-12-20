using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplication.Models
{
    public class Division
    {
        [Key]
        public int DivisionId { get; set; }
        public string Name { get; set; }
    }
}
