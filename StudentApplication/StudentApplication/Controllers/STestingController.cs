using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Data;
using StudentApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplication.Controllers
{
    public class STestingController : Controller
    {
        private readonly ApplicationContext context;

        public STestingController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        { 
           var data= context.StudentDetails.ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            ViewBag.Standard = context.Standards.ToList();
            ViewBag.Division = context.Divisions.ToList();
            ViewBag.Subject = context.Subjects.ToList();


            StudentDetail obj = new StudentDetail();

           

            if (ModelState.IsValid)
            {
                var data = context.StudentDetails.FirstOrDefault(e => e.StudentId == id);
              //  ViewBag.StudentSubject = context.StudentSubjects.Where(x => x.StudentId == obj.StudentId).ToList();
                if (id > 0)
                {
                    obj = new StudentDetail()
                    {
                        StudentId = data.StudentId,
                        StudentName = data.StudentName,
                        FatherName = data.FatherName,
                        FatherMobileNo = data.FatherMobileNo,
                        IsActive = true,
                        StandardId = data.StandardId,
                        DivisionId = data.DivisionId,
                        RollNo = data.RollNo,
                        GRNo = data.GRNo,
                        DOB = data.DOB
                    };
                   ViewBag.StudentSubject = context.StudentSubjects.Where(x => x.StudentId == obj.StudentId).ToList();

                 }
            }

         
            //  return View();
            return View(obj);
        }

        [HttpPost]
        public IActionResult AddEdit(StudentDetail sd ,IFormCollection fg)
        {
            ViewBag.Standard = context.Standards.ToList();
            ViewBag.Division = context.Divisions.ToList();
            ViewBag.Subject = context.Subjects.ToList();

            var list3 = context.Standards.ToList();
            if (list3 != null)
            {
                foreach (var item in list3)
                {
                    var std = new Standard()
                    {
                        StandardId = item.StandardId,
                        Name = item.Name,
                        MinAge=item.MinAge

                    };
                    sd.lststd.Add(std);

                }
            }
            ViewBag.StudentSubject = context.StudentSubjects.Where(x => x.StudentId == sd.StudentId).ToList();
            int age = (int)((DateTime.Now - sd.DOB).TotalDays / 365.242199);
            for (int i = 0; i < sd.lststd.Count; i++)
            {
                var sd1 = sd.lststd[i].MinAge;
                var sd2 = sd.lststd[i].StandardId;
                if (sd2 == sd.StandardId)
                {
                    if (sd1 >= age)
                    {
                        TempData["msg"] = "age is not valid for standard";
                        return View(sd);
                    }
                }

             }
            //List<Subject> list = context.Subjects.Where(x => x.SubjectId == sd.StudentId).ToList();
           
           
            var data = context.StudentDetails.Where(e => e.RollNo == sd.RollNo && e.StandardId == sd.StandardId && e.DivisionId == sd.DivisionId && e.StudentId != sd.StudentId).ToList();
            var data1 = data.GroupBy(x => new { x.StandardId, x.DivisionId, x.RollNo }).ToList();
            foreach (var group in data1)
            {
                if (group.ToList().Count() >= 1)
                {
                    TempData["msg"] = "please check RollNo";
                    
                    return View(sd);
                }
            }

            if (context.StudentDetails.Any(e => e.GRNo == sd.GRNo && e.StudentId != sd.StudentId))
            {
                ModelState.AddModelError("GRNo", "GRNo already exist");
            }



            if (ModelState.IsValid)
            {
                if (sd.StudentId == 0)
                {
                    var obj = new StudentDetail()
                    {
                        StudentId = sd.StudentId,
                        StudentName = sd.StudentName,
                        FatherName = sd.FatherName,
                        FatherMobileNo = sd.FatherMobileNo,
                        IsActive = true,
                        StandardId = sd.StandardId,
                        DivisionId = sd.DivisionId,
                        RollNo = sd.RollNo,
                        GRNo = sd.GRNo,
                        DOB = sd.DOB
                    };
                    
                    context.StudentDetails.Add(obj);
                    context.SaveChanges();
                    var id = obj.StudentId;
                   
                    if (fg["SubjectSelection"].ToString() != "")
                    {

                        foreach (var item in fg["SubjectSelection"].ToString().Split(','))
                        {
                            var sdata = new StudentSubject()
                            {
                                StudentId = id,
                                SubjectId = Convert.ToInt32(item)
                            };
                            context.StudentSubjects.AddRange(sdata);
                            context.SaveChanges();
                        }

                    }
                    TempData["Msg"] = "Data Saved";
                    return RedirectToAction("Index");
                }
                else
                {
                    var obj = new StudentDetail()
                    {
                        StudentId = sd.StudentId,
                        StudentName = sd.StudentName,
                        FatherName = sd.FatherName,
                        FatherMobileNo = sd.FatherMobileNo,
                        IsActive = true,
                        StandardId = sd.StandardId,
                        DivisionId = sd.DivisionId,
                        RollNo = sd.RollNo,
                        GRNo = sd.GRNo,
                        DOB = sd.DOB
                    };
                   

                    context.StudentDetails.Update(obj);
                    context.SaveChanges();
                    List<StudentSubject> list1 = context.StudentSubjects.Where(y => y.StudentId == sd.StudentId).ToList();
                    context.StudentSubjects.RemoveRange(list1);
                    // for getting checked checkbox
                    var id = obj.StudentId;
                    if (fg["SubjectSelection"].ToString() != "")
                    {

                        foreach (var item in fg["SubjectSelection"].ToString().Split(','))
                        {
                            var sdata = new StudentSubject()
                            {
                                StudentId = id,
                                SubjectId = Convert.ToInt32(item)
                            };
                            context.StudentSubjects.AddRange(sdata);
                            context.SaveChanges();
                        }

                    }
                    TempData["Msg"] = "Data Updated";
                    return RedirectToAction("Index");
                }
             
            }
           

            return View(sd);

        }

    }
}
