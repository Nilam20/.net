using Microsoft.AspNetCore.Mvc;
using StudentApplication.Data;
using StudentApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApplication.Controllers
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
            var list = context.StudentDetails.ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            StudentDetail obj = new StudentDetail();
            if (ModelState.IsValid)
            {
                var data = context.StudentDetails.FirstOrDefault(e => e.StudentId == id);
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
                   
                }
            }

          
            //  return View();
            return View(obj);
            
           
        }
    [HttpPost]
        public IActionResult AddEdit(StudentDetail sd)
        {
            var list = context.Subjects.ToList();
            if (list != null)
            {
                foreach (var item in list)
                {
                    var stusub = new Subject()
                    {

                        SubjectId = item.SubjectId,
                        SubjectName = item.SubjectName

                    };
                    sd.lstsub.Add(stusub);

                }


            }
            var list1 = context.Divisions.ToList();
            if (list1 != null)
            {
                foreach (var item in list1)
                {
                    var div = new Division()
                    {
                        DivisionId = item.DivisionId,
                        Name = item.Name

                    };
                    sd.lstdiv.Add(div);
                }
            }
            var list3 = context.Standards.ToList();
            if (list3 != null)
            {
                foreach (var item in list3)
                {
                    var std = new Standard()
                    {
                        StandardId = item.StandardId,
                        Name = item.Name

                    };
                    sd.lststd.Add(std);

                }
            }
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
            var listss = context.StudentSubjects.ToList();
            for (int i = 0; i < sd.lstsub.Count; i++)
            {
                if (sd.lstsub[i].IsChecked == true)
                {

                }

            }
           var data = context.StudentDetails.Where(e=>e.RollNo == sd.RollNo && e.StandardId == sd.StandardId && e.DivisionId==sd.DivisionId&&e.StudentId!=sd.StudentId).ToList();
            var data1 = data.GroupBy(x => new { x.StandardId,x.DivisionId,x.RollNo }).ToList();
            foreach (var group in data1)
            {
                if (group.ToList().Count() >=1)
                {
                    TempData["msg"] = "please check RollNo";
                    return View(sd);
                }
            }

            if (context.StudentDetails.Any(e => e.GRNo == sd.GRNo && e.StudentId !=sd.StudentId))
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
                  ;
                    context.StudentDetails.Add(obj);
                    context.SaveChanges();
                    
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
                    //if (obj.lstdiv.Count > 0)
                    //{
                    //    int id1 = obj.DivisionId;
                    //    for (var i = 0; i < obj.lstdiv.Count; i++)
                    //    {
                    //        sd.lstdiv[i].DivisionId = id1;
                    //    }
                    //}
                    //if (obj.lststd.Count > 0)
                    //{
                    //    int id1 = obj.StandardId;
                    //    for (var i = 0; i < obj.lststd.Count; i++)
                    //    {
                    //        sd.lststd[i].StandardId = id1;
                    //    }
                    //}
                   

                    context.StudentDetails.Update(obj);
                    context.SaveChanges();
                    TempData["Msg"] = "Data Updated";
                    return RedirectToAction("Index");
                }

            }

            return View(sd);
           
        }
    }
}
