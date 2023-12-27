using Microsoft.EntityFrameworkCore;
using StudentApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp2.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options){ }
        public DbSet<StudentData> StudentDatas { get; set; }
        public DbSet<StudentSubject> StudentSubjects1 { get; set; }
        public DbSet<StudentSubjectMark> StudentSubjectMarks { get; set; }
        public DbSet<SubjectDetail> SubjectDetails { get; set; }
        public DbSet<StudentApp2.Models.CustomModel> CustomModel { get; set; }
    }
}
