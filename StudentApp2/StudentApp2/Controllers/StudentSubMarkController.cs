using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentApp2.Data;
using StudentApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp2.Controllers
{
    public class StudentSubMarkController : Controller
    {
        private readonly ApplicationContext context;

        public StudentSubMarkController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var data = context.StudentSubjectMarks.ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            ViewBag.SubjectDetail = context.SubjectDetails.ToList();
            StudentSubjectMark ssm = new StudentSubjectMark();
            //{
            //    var data = context.StudentSubjectMarks.FirstOrDefault(e => e.StudentId == id);
            //    if (id > 0)
            //    {
            //        ssm = new StudentSubjectMark()
            //        {
            //            StudentId = data.StudentId,
            //            SubjectId = data.SubjectId,
            //            Mark = data.Mark

            //        };

            //    }
            //}

          //  ViewBag.SubjectDetail = context.SubjectDetails.ToList();

            //List<StudentData> list = context.StudentDatas.ToList();
            //if (list.Count > 0)
            //{
            //    foreach (var d in list)
            //    {
            //        StudentData sd = new StudentData()
            //        {
            //            Name = d.Name,
            //            Standard = d.Standard,
            //            RollNo = d.RollNo,
            //            StudentId = d.StudentId
            //        };
            //        ssm.liststddata.Add(sd);

            //    }
            //}


            return View(ssm);
        }


        [HttpPost]
        public IActionResult AddEdit(StudentSubjectMark ssm)
        {
            ViewBag.SubjectDetail = context.SubjectDetails.ToList();
            List<StudentData> list1 = context.StudentDatas.ToList();
            List<StudentSubject> list2 = context.StudentSubjects1.ToList();
            if (list1.Count > 0)
            {
                foreach (var d in list1)
                {
                    StudentData sd = new StudentData()
                    {
                        StudentId = d.StudentId,
                        Name = d.Name,
                        Standard = d.Standard,
                        RollNo = d.RollNo,
                        GRNO = d.GRNO,
                        Percentage = d.Percentage

                    };
                    ssm.liststddata.Add(sd);

                }
            }
            if (list2.Count > 0)
            {
                foreach (var d in list2)
                {
                    StudentSubject sd = new StudentSubject()
                    {
                        SubjectId = d.SubjectId,
                        StudentId = d.StudentId
                    };
                    ssm.liststdsub.Add(sd);
                }
            }

            for (int i = 0; i < list1.Count; i++)
            {
                var data = ssm.liststdsub[i].StudentId;
                var data1 = ssm.liststddata[i].StudentId;
                if (data == data1)
                {
                    
                    StudentSubjectMark sdn = new StudentSubjectMark()
                    {
                        SubjectId = ssm.liststdsub[i].SubjectId,
                        StudentId = ssm.liststdsub[i].StudentId,

                      //  Mark = ssm.liststdsub[i].StudentId

                    };
                    ssm.listn.Add(sdn);

                }
            }
            context.StudentSubjectMarks.AddRange(ssm.listn);
            context.SaveChanges();
            if (ssm.listn.Count > 0)
            {
               

                for (int i = 0; i < ssm.listn.Count; i++)
                {
                    var sum = 0;
                    var stidlistn = ssm.listn[i].StudentId;
                    var stdata = ssm.liststddata[i].StudentId;
                    if (stidlistn == stdata)
                    {
                       
                        sum = sum + ssm.listn[i].Mark;
                    
                        var per = sum * 100 / 300;

                       var ff= context.StudentDatas.FirstOrDefault(x => x.StudentId == ssm.liststddata[i].StudentId);
                        {
                               context.StudentDatas.Remove(ff);
                                var obj = new StudentData()
                                {
                                    StudentId = ff.StudentId,
                                    Name = ff.Name,
                                    FatherName = ff.FatherName,
                                    Standard=ff.Standard,
                                    RollNo = ff.RollNo,
                                    GRNO = ff.GRNO,
                                    Percentage = per

                                };
                                context.StudentDatas.Update(obj);
                            
                           
                            context.SaveChanges();
                        } 
                    }
                  
                   
                    

                }
            }

           

            return RedirectToAction("Index");
            // return RedirectToAction("Index");

            // return View(ssm);


        }
        
           
    }
}