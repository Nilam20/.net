using Microsoft.AspNetCore.Mvc;
using StudentApp2.Data;
using StudentApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp2.Controllers
{
    public class StudentSubjectMarkController : Controller
    {
        private readonly ApplicationContext context;

        public StudentSubjectMarkController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddEdit(int id)
        {
           //StudentSubjectMark ssm = new StudentSubjectMark();
            List<CustomModel> cmm = new List<CustomModel>();
            ViewBag.SubjectDetail = context.SubjectDetails.ToList();
            if (id > 0)
            {
                List<StudentSubject> listss = context.StudentSubjects1.Where(e => e.SubjectId == id).ToList();
                List<StudentSubjectMark> listmark = context.StudentSubjectMarks.Where(e => e.SubjectId == id).ToList();
                for (int i = 0; i < listss.Count; i++)
                {

                    var liststudent = context.StudentDatas.FirstOrDefault(e => e.StudentId == listss[i].StudentId);
                    {
                        //var sd = new StudentData()
                        //{
                        //    StudentId = liststudent.StudentId,
                        //    Name = liststudent.Name,
                        //    Standard = liststudent.Standard,
                        //    RollNo = liststudent.RollNo

                        //};
                        //ssm.listStd.Add(sd);
                        var cm = new CustomModel()
                        {
                            SubjectId = id,
                            StudentId = liststudent.StudentId,
                            Name = liststudent.Name,
                            Standard = liststudent.Standard,
                            RollNo = liststudent.RollNo,
                           // Mark = listmark.FirstOrDefault(e => e.StudentId == liststudent.StudentId).Mark
                           
                        };
                        cmm.Add(cm);
                    }

                }

              

            }

           

            return View(cmm);
        }



        [HttpPost]
        public IActionResult AddEdit(List<CustomModel> cmm)
        {
            ViewBag.SubjectDetail = context.SubjectDetails.ToList();
            List<CustomModel> lst = cmm;
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst != null)
                {
                    StudentSubjectMark obj = new StudentSubjectMark()
                    {
                        // SubjectId=cmm[i].SubjectId
                        SubjectId = cmm[i].SubjectId,
                        StudentId = cmm[i].StudentId,
                        Mark = cmm[i].Mark
                    };
                    context.StudentSubjectMarks.AddRange(obj);
                    context.SaveChanges();
                }

            }
            return View();
        }

         

        }
}
