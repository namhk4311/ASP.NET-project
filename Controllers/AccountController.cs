using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Login()
        {
            var sessionUser = HttpContext.Session.GetObject<String>("ssUser_username");
            if (sessionUser != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserAccount user)
        {
            var result = this._context.Users.Where(x => x.username == user.Username && x.password == user.Password);
            if (result.Count() != 0)
            {
                var fname = result.Select(x => x.firstname).FirstOrDefault();
                var role = result.Select(x => x.role).FirstOrDefault();
                var lname = result.Select(x => x.lastname).FirstOrDefault();

                var email = result.Select(x => x.email).FirstOrDefault();

                var id = result.Select(x => x.Id).FirstOrDefault();
                HttpContext.Session.SetObject("ssUser_fName", fname);
                HttpContext.Session.SetObject("ssUser_lName", lname);
                HttpContext.Session.SetObject("ssUser_email", email);
                HttpContext.Session.SetObject("ssUser_username", user.Username);
                HttpContext.Session.SetObject("ssUser_userid", id);
                HttpContext.Session.SetObject("ssUser_role", role);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "Incorrect Username/Password";
            }


            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("ssUser_fName");
            HttpContext.Session.Remove("ssUser_lName");
            HttpContext.Session.Remove("ssUser_email");
            HttpContext.Session.Remove("ssUser_username");
            HttpContext.Session.Remove("ssUser_userid");
            HttpContext.Session.Remove("ssUser_role");

            return RedirectToAction("Index", "Home");

        }
    }
}
