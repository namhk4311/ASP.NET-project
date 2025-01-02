using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Classes.Include(x => x.Course).Include(x => x.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var sessionUser = HttpContext.Session.GetObject<String>("ssUser_username");
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .Include(x => x.Course)
                .Include(x => x.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            var sessionUser = HttpContext.Session.GetObject<String>("ssUser_username");
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassInput classes)
        {
            var getCourseId = -1;
            var getUserId = -1;
            foreach(var eachCourse in _context.Courses)
            {
                if (classes.CourseCode == eachCourse.codeCourse)
                {
                    getCourseId = eachCourse.Id;
                    break;
                }
            }

            foreach(var eachUser in _context.Users)
            {
                if (classes.LecturerID == eachUser.Id && eachUser.role == "lecturer")
                {
                    getUserId = eachUser.Id;
                    break;
                }
            }


            if (getCourseId == -1 && getUserId == -1)
            {
                TempData["msg"] = "Cannot find the course and lecturer";
                return View();
            }
            else if (getCourseId == -1)
            {
                TempData["msg"] = "Cannot find the course";
                return View();
            }
            else if (getUserId == -1)
            {
                TempData["msg"] = "Cannot find the lecturer";
                return View();
            }
 
            Class addClass = new Class()
            {
                startTime = classes.startTime,
                Room = classes.Room,
                DayInWeek = classes.DayInWeek,
                CourseId = getCourseId,
                UserId = classes.LecturerID
            };
                _context.Add(addClass);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Classes");
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var sessionUser = HttpContext.Session.GetObject<String>("ssUser_username");
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", @class.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", @class.UserId);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,startTime,Room,DayInWeek,CourseId,UserId")] Class @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", @class.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", @class.UserId);
            return RedirectToAction("Index", "Classes");
        }

        // GET: Classes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{

        //    var sessionUser = HttpContext.Session.GetObject<String>("ssUser_username");
        //    if (sessionUser == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var @class = await _context.Classes
        //        .Include(x => x.Course)
        //        .Include(x => x.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (@class == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(@class);
        //}

        //// POST: Classes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var @class = await _context.Classes.FindAsync(id);
        //    if (@class != null)
        //    {
        //        _context.Classes.Remove(@class);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            var classes = await _context.Classes.FindAsync(id);
            if (id == null)
            {
                return RedirectToAction("Index", "Courses");
            }
            _context.Classes.Remove(classes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "classes");
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
