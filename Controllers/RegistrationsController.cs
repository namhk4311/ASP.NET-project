using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Registrations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Registrations.Include(r => r.Classes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Registrations/Details/5
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

            var registration = await _context.Registrations
                .Include(r => r.Classes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Create
        public IActionResult Create()
        {
            var sessionUser = HttpContext.Session.GetObject<String>("ssUser_username");
            if (sessionUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,StudentId,ClassId")] Registration registration)
        {
            Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);

            //if (!emailRegex.IsMatch(registration.Email))
            //{
            //    TempData["validEmail"] = "false";
            //    return View(registration);
            //}
            //else TempData["validEmail"] = "true";

            //if (ModelState.IsValid)
            //{
            _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Registrations");
            //}
            //ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", registration.ClassId);
            //return View(registration);
        }

        // GET: Registrations/Edit/5
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

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", registration.ClassId);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,StudentId,ClassId")] Registration registration)
        {
            if (id != registration.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Registrations");
            //}
            //ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", registration.ClassId);
            //return View(registration);
        }

        // GET: Registrations/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var registration = await _context.Registrations
        //        .Include(r => r.Classes)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (registration == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(registration);
        //}

        //// POST: Registrations/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var registration = await _context.Registrations.FindAsync(id);
        //    if (registration != null)
        //    {
        //        _context.Registrations.Remove(registration);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (id == null)
            {
                return RedirectToAction("Index", "Registrations");
            }
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Registrations");
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.Id == id);
        }
    }
}
