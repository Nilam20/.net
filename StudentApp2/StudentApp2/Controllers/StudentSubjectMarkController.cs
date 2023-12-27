﻿using Microsoft.AspNetCore.Mvc;
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
            var data = context.StudentSubjectMarks.ToList();
            return View(data);
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

                        var cm = new CustomModel()
                        {
                            SubjectId = id,
                            StudentId = liststudent.StudentId,
                            Name = liststudent.Name,
                            Standard = liststudent.Standard,
                            RollNo = liststudent.RollNo,
                            //if(listmark.Count!=0)
                            //      {
                            // Mark = listmark.FirstOrDefault(e => e.StudentId == liststudent.StudentId).Mark

                            // }


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

                    context.StudentSubjectMarks.UpdateRange(obj);
                    context.SaveChanges();
                    int TotalMark = context.StudentSubjectMarks.Where(e => e.StudentId == cmm[i].StudentId).Sum(e => e.Mark);
                    List<int> StudSubjectID = new List<int>();
                    List<StudentSubject> li = context.StudentSubjects1.Where(e => e.StudentId == cmm[i].StudentId).ToList();
                    if (li.Count != 0)
                    {
                        for (int j = 0; j < li.Count; j++)
                        {
                            StudSubjectID.Add(li[j].SubjectId);
                        }

                       
                    }
                    int TotalMaxMark = context.SubjectDetails.Where(e => StudSubjectID.Contains(e.SubjectId)).Sum(e => e.MaxMark);
                    double per = TotalMark * 100 / TotalMaxMark;
                    var ff = context.StudentDatas.FirstOrDefault(x => x.StudentId == cmm[i].StudentId);
                    {
                         context.StudentDatas.Remove(ff);
                        var nn = new StudentData()
                        {
                            StudentId = ff.StudentId,
                            Name = ff.Name,
                            FatherName = ff.FatherName,
                            Standard = ff.Standard,
                            RollNo = ff.RollNo,
                            GRNO = ff.GRNO,
                            Percentage = per

                        };
                        context.StudentDatas.Update(ff);


                        context.SaveChanges();
                    //}



                }


            }





            // return RedirectToAction("Index");
            return View();


        }



        //int sum = 0;
        //sum = cmm[i].Mark * 100 / 300;
        //double per = sum;
        //var sdata = context.StudentDatas.FirstOrDefault(e => e.StudentId == cmm[i].StudentId);
        //{
        //    StudentData std = new StudentData()
        //    {

        //        Percentage = per
        //    };
        //    context.StudentDatas.Update(std);

    }



}

