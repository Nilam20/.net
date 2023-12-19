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
            StudentSubjectMark ssm = new StudentSubjectMark();
            ViewBag.SubjectDetail = context.SubjectDetails.ToList();
           
            return View(ssm);
        }
        

        [HttpPost]
        public IActionResult AddEdit(StudentSubjectMark ssm)
        {
            if (ModelState.IsValid)
            {
                // code to save record  and redirect to listing page
            }
            
           ViewBag.SubjectDetails = context.SubjectDetails.ToList();
            
            return View(ssm);
        }
    }
}
