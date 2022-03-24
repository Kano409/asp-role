using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        private readonly DataContext _context;

        
        public TestController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.courses = new SelectList(_context.Courses, "Id", "Title");
            return View();
        }
    }
}
