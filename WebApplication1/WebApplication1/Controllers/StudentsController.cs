using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    // [Authorize(Roles="Student,Admin")]
    public class StudentsController : Controller
    {
        private readonly DataContext _context;

        public StudentsController(DataContext context)
        {
            _context = context;
        }
       
        [Route("student/index")]
        // [Authorize(Roles = "Student,Admin")]
        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5  v11
        [Route("student/details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
                // return View("NotFound");
            }

            var student = await _context.Students.Include(x => x.Enrollment).ThenInclude(y => y.Course)
                                                 .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
                // return View("NotFound",id);
            }

            return View(student);
            // return View("NotFound");
        }

        // GET: Students/Create  v9
        public IActionResult Create()
        {
            // Name, Enrolled empty -> checkbox with Courses and select checkbox and new Student
            var courses = _context.Courses.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString(),

            }).ToList();

            CreateStudentViewModel vm = new CreateStudentViewModel();


            vm.Courses = courses; 
            return View(vm);
        }

        // POST: Students/Create  v10
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateStudentViewModel vm)
        {
            // insert data in Student
            var student = new Student
            {
                Name = vm.Name,
                Enrolled = vm.Enrolled
            };

            var selectedCourses = vm.Courses.Where(x => x.Selected).Select(y => y.Value).ToList();
            // insert values in middle table thats is StudentCourse
            foreach (var item in selectedCourses)
            {
                student.Enrollment.Add(new StudentCourse()
                {
                    // using Student, StudentCourse n automatically filled
                    CourseId = int.Parse(item)

                });
            }
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Students/Edit/5  v12
        public async Task<IActionResult> Edit(int? id)
        {

            var student = await _context.Students.Include(x => x.Enrollment).Where(y => y.Id == id).FirstOrDefaultAsync();
            if (id == null)
            {
                return NotFound();
            }
            var selectedIds = student.Enrollment.Select(x => x.CourseId).ToList();

            var items = _context.Courses.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString(),
                Selected = selectedIds.Contains(x.Id)
            }).ToList();
            CreateStudentViewModel vm = new CreateStudentViewModel();
            // pass edit from Student
            vm.Name = student.Name;
            vm.Enrolled = student.Enrolled;
            vm.Courses = items;
            if (student == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: Students/Edit/5  v13
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateStudentViewModel vm)
        {
            /*

            when you edit student, it create new one student not change existing student

            var student = new Student
            {
                Name = vm.Name,
                Enrolled = vm.Enrolled

            };
            */
            // retrieve a existing student
            var student = _context.Students.Find(vm.Id);
            student.Name = vm.Name;
            student.Enrolled = vm.Enrolled;

            var studentByIds = _context.Students.Include(x => x.Enrollment).FirstOrDefault(y => y.Id == vm.Id);
            var existingIds = studentByIds.Enrollment.Select(x => x.CourseId).ToList();
            var selectedIds = vm.Courses.Where(x => x.Selected).Select(y=>y.Value).Select(int.Parse).ToList();
            var toAdd = selectedIds.Except(existingIds);
            var toRemove = existingIds.Except(selectedIds);

            // update entry from middle table -> remove existing entry
            // toRemove entry remove from table
            student.Enrollment = student.Enrollment.Where(x => !toRemove.Contains(x.CourseId)).ToList();

            // add new entry in middle table
            foreach (var item in toAdd)
            {
                student.Enrollment.Add(new StudentCourse()
                { 
                    CourseId = item               

                });
            }

            // update student(data) through checkbox and save changes
            _context.Students.Update(student);
            _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
