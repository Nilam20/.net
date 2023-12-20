using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplication.Models
{
    public class Standard
    {
        [Key]
        public int StandardId { get; set; }
        public int Name { get; set; }
        public int MinAge { get; set; }
        
    }
}
