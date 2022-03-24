using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CoursesController : Controller
    {

        // retirve courses fron db and show it
        private readonly DataContext _context;

        // make constructor for initialize context class
        public CoursesController(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get section of CoursesController
        /// </summary>
        /// <returns></returns>
        [HttpGet]
      
        public IActionResult Index(string sortOrder)
        {

            ViewData["TitleSort"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            
            var courses = _context.Courses.ToList();
            switch (sortOrder)
            {
                case "title_desc":
                    courses = courses.OrderByDescending(s => s.Title).ToList();
                    break;

                default:
                    courses = courses.OrderBy(s => s.Title).ToList();
                    break;
            }
            return View(courses);
        }

        // not require to get -> empty textbox
        [HttpGet]
        public IActionResult Create()
        {
            // not pass data               
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var course = _context.Courses.Where(x=>x.Id == id).FirstOrDefault();
            return View(course);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Where(x => x.Id == id).FirstOrDefault();
            return View(course);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Where(x => x.Id == id).FirstOrDefault();
            return View(course);
        }

        /// <summary>
        /// Post section of CoursesController
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Create(Course model)
        {
            _context.Courses.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Edit(Course model)
        {
            _context.Courses.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Delete(Course model)
        {
            _context.Courses.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
