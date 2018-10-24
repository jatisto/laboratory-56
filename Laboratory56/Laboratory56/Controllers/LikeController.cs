using System.Collections.Generic;
using System.Linq;
using Laboratory56.Data;
using Laboratory56.Models;
using Laboratory56.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Laboratory56.Controllers
{
    public class LikeController : Controller
    {
        public LikeController(ApplicationDbContext context, IHostingEnvironment environment,
            FileUploadService fileUploadService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _fileUploadService = fileUploadService;
            _userManager = userManager;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly FileUploadService _fileUploadService;

        public IActionResult Like(string id)
        {
            return View();
        }
    }
}