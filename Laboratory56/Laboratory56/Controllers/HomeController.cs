using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Laboratory56.Data;
using Microsoft.AspNetCore.Mvc;
using Laboratory56.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Laboratory56.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<HomeController> localizer)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = _localizer["Header"];
            ViewData["Message"] = _localizer["Message"];

            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var publish = _context.Publications
                    .Include(p => p.User)
                    .Include(p => p.CommentsList)
                    .Where(p => p.UserId != user.Id)
                    .OrderByDescending(p => p.Id).ToList();
                return View(publish);
            }
            else
            {
                var publish = _context.Publications
                    .Include(p => p.User)
                    .Include(p => p.CommentsList)
                    .OrderByDescending(p => p.Id).ToList();
                return View(publish);
            }


            
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
                    Name = u.UserName,
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

        #region AllUser

        public ActionResult UsersList()
        {
            return View(_context.Users.ToList());
        }

        #endregion

        #region UsersInfo

        public ActionResult UsersInfo(string id)
        {
            List<ApplicationUser> userInfo = _context.ApplicationUser
                .Where(u => u.Id == id).ToList();

            return View(userInfo);
        }

        #endregion

        #region Culture

        public string GetCulture(string code = "")
        {
            if (!String.IsNullOrEmpty(code))
            {
                CultureInfo.CurrentCulture = new CultureInfo(code);
                CultureInfo.CurrentUICulture = new CultureInfo(code);
            }
            return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
        }

        #endregion
    }
}