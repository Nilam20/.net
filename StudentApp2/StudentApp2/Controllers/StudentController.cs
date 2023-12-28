﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApp2.Data;
using StudentApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp2.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationContext context;

        public StudentController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var data = context.StudentDatas.ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            StudentData obj = new StudentData();
            var result = context.StudentDatas.FirstOrDefault(e => e.StudentId == id);
            ViewBag.SubjectDetails = context.SubjectDetails.ToList();
            if (id>0)
            {
                obj = new StudentData()
                { 
                    StudentId=result.StudentId,
                    Name=result.Name,
                    Standard=result.Standard,
                    FatherName=result.FatherName,
                    RollNo=result.RollNo,
                    GRNO=result.GRNO,
                    Percentage=result.Percentage

                };
                ViewBag.StudentSubjectlist = context.StudentSubjects1.Where(e => e.StudentId == obj.StudentId).ToList();

            }

            return View(obj);
           
        }
        [HttpPost]
        public IActionResult AddEdit(StudentData Stdd,IFormCollection fc)
        {
            ViewBag.SubjectDetails = context.SubjectDetails.ToList();

            // ViewBag.StudentSubjectlist = context.SubjectDetails.Where(e => e.StudentId == Stdd.StudentId).ToList();
            ViewBag.StudentSubjectlist = context.StudentSubjects1.Where(e => e.StudentId == Stdd.StudentId).ToList();
            if (ModelState.IsValid)
            {
                if (Stdd.StudentId == 0)
                {
                   var obj = new StudentData()
                    {
                        StudentId = Stdd.StudentId,
                        Name = Stdd.Name,
                        Standard = Stdd.Standard,
                        FatherName = Stdd.FatherName,
                        RollNo = Stdd.RollNo,
                        GRNO = Stdd.GRNO,
                        Percentage = Stdd.Percentage

                    };
                    context.StudentDatas.Add(obj);
                    context.SaveChanges();

                    if (fc["SubjectSelection"].ToString() != "")
                    {
                         var id = obj.StudentId;
                        foreach (var item in fc["SubjectSelection"].ToString().Split(','))
                        {
                            var sdata = new StudentSubject()
                            {
                                StudentId = id,
                                SubjectId = Convert.ToInt32(item)
                            };
                            context.StudentSubjects1.AddRange(sdata);
                            context.SaveChanges();
                        }

                    }
                  

                    TempData["Msg"] = "Data Saved";
                    return RedirectToAction("Index");
                }
                else
                {
                 var obj = new StudentData()
                    {
                        StudentId = Stdd.StudentId,
                        Name = Stdd.Name,
                        Standard = Stdd.Standard,
                        FatherName = Stdd.FatherName,
                        RollNo = Stdd.RollNo,
                        GRNO = Stdd.GRNO,
                        Percentage = Stdd.Percentage

                    };
                    context.StudentDatas.Update(obj);
                    context.SaveChanges();

                    List<StudentSubject> list = context.StudentSubjects1.Where(e => e.StudentId == Stdd.StudentId).ToList();
                    context.StudentSubjects1.RemoveRange(list);

                    if (fc["SubjectSelection"].ToString() != "")
                    {
                        var id = Stdd.StudentId;
                        foreach (var item in fc["SubjectSelection"].ToString().Split(','))
                        {
                            var sdata = new StudentSubject()
                            {
                                StudentId = id,
                                SubjectId = Convert.ToInt32(item)
                            };
                            context.StudentSubjects1.UpdateRange(sdata);
                            context.SaveChanges();
                        }

                    }

                    TempData["Msg"] = "Data Updated";
                    return RedirectToAction("Index");
                }
            }

            return View(Stdd);

        }
    }
}
