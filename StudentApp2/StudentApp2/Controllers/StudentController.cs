using Microsoft.AspNetCore.Http;
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
           // double per;
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
                    //added 
                    List<StudentSubject> list = context.StudentSubjects1.Where(e => e.StudentId == Stdd.StudentId).ToList();
                    context.StudentSubjects1.RemoveRange(list);

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
                       // Percentage = per

                    };
                  
                    //context.StudentDatas.Update(obj);
                    //context.SaveChanges();

                    List<StudentSubject> list = context.StudentSubjects1.Where(e => e.StudentId == Stdd.StudentId).ToList();
                    context.StudentSubjects1.RemoveRange(list);
                    context.SaveChanges();





                    if (fc["SubjectSelection"].ToString() != "")
                    {
                        List<int> StudSubjectID = new List<int>();
                        List<int> StudID = new List<int>();
                    
            
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



                            List<StudentSubject> li = context.StudentSubjects1.Where(e => e.StudentId == sdata.StudentId && e.SubjectId==sdata.SubjectId).ToList();
                            if (li.Count != 0)
                            {
                                for (int j = 0; j < li.Count; j++)
                                {
                                    StudSubjectID.Add(li[j].SubjectId);
                                    StudID.Add(li[j].StudentId);
                                }


                            }
                           

                        }
                        // var dd = context.StudentSubjectMarks.FirstOrDefault(e => !StudSubjectID.Contains(e.SubjectId));
                        List<StudentSubjectMark> dd = context.StudentSubjectMarks.Where(e => !StudSubjectID.Contains(e.SubjectId) && StudID.Contains(e.StudentId)).ToList();
                        if (dd != null)
                        {
                            context.StudentSubjectMarks.RemoveRange(dd);
                            context.SaveChanges();
                        }

                        int TotalMark = context.StudentSubjectMarks.Where(e => e.StudentId == Stdd.StudentId).Sum(e => e.Mark);
                        List<int> StudSubjectID1 = new List<int>();
                        List<StudentSubject> lin = context.StudentSubjects1.Where(e => e.StudentId == Stdd.StudentId).ToList();
                        if (lin.Count != 0)
                        {
                            for (int j = 0; j < lin.Count; j++)
                            {
                                StudSubjectID1.Add(lin[j].SubjectId);
                            }


                        }
                         int TotalMaxMark = context.SubjectDetails.Where(e => StudSubjectID1.Contains(e.SubjectId)).Sum(e => e.MaxMark);
                         double per = TotalMark * 100 / TotalMaxMark;
                         obj.Percentage = per;
                    }
                    context.StudentDatas.Update(obj);
                    context.SaveChanges();
                    TempData["Msg"] = "Data Updated";
                    return RedirectToAction("Index");
                }
            }

            return View(Stdd);

        }
    }
}
