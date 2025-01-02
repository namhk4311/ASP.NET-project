﻿using System;
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
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(string searchString)
        {
            var course = await _context.Courses.ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                course = _context.Courses.Where(x => x.codeCourse == searchString).ToList();
                TempData["btn-fullList"] = "on";
            }
            return View(course);
        }

        // GET: Courses/Details/5
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

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            var sessionUser = HttpContext.Session.GetObject<String>("ssUser_username");
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nameCourse,codeCourse,duration")] Course course)
        {
            //if (ModelState.IsValid)
            //{
            //}
            _context.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Courses");
        }

        // GET: Courses/Edit/5
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

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nameCourse,codeCourse,duration")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            return RedirectToAction("Index", "Courses");
        }

        // GET: Courses/Delete/5
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

        //    var course = await _context.Courses
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(course);
        //}

        //// POST: Courses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var course = await _context.Courses.FindAsync(id);
        //    if (course != null)
        //    {
        //        _context.Courses.Remove(course);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (id == null)
            {
                return RedirectToAction("Index", "Courses");
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Courses");
        }
        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
