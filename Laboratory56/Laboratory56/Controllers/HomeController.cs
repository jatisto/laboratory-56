using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Laboratory56.Data;
using Microsoft.AspNetCore.Mvc;
using Laboratory56.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Laboratory56.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_context.Publications.OrderByDescending(p => p.Id).ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        #region Search

        public ActionResult Searching()
        {
            return View();
        }

        public ActionResult SearchingResult(string key)
        {
            List<UserListElements> users = _userManager.Users.Where(u =>
                    u.Name.Contains(key) ||
                    u.UserName.Contains(key))
                .Select(u => new UserListElements
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    AvatarImages = u.AvatarImage
                }).ToList();

            return PartialView("UserSearchResult", users);
        }

        public class UserListElements
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string AvatarImages { get; set; }
        }

        #endregion
    }
}