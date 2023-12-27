using Microsoft.AspNetCore.Mvc;
using StudentApp2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp2.Controllers
{
    public class CustomController : Controller
    {
        private readonly ApplicationContext context;

        public CustomController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddEdit(int id)
        {
            return View();
        }
    }
}
