using Microsoft.EntityFrameworkCore;
using StudentApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplication.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options) { }
        public DbSet<StudentDetail> StudentDetails { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Standard> Standards { get; set; }
        public DbSet<Division> Divisions { get; set; }

    }
}
