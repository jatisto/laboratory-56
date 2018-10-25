using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laboratory56.Data;
using Laboratory56.Models;
using Laboratory56.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Laboratory56.Controllers
{
    public class PublicationsController : Controller
    {
        #region Conections and Constructor

        public PublicationsController(ApplicationDbContext context, IHostingEnvironment environment,
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

        #endregion

        #region Index

        // GET: Publications
        public async Task<IActionResult> Index()
        {
            var sort = await _context.Publications.ToListAsync();

            return View(sort.OrderByDescending(s => s.Id));
        }
        

        #endregion

        #region Details

        // GET: Publications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publications
                .SingleOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        #endregion

        #region Create

        // GET: Publications/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImadeUrl,Description,Like,RePost")]
            Publication publication, PublicationVM model)
        {
            if (ModelState.IsValid)
            {
                // Метод Publication
                var pub = Publication(publication, model);
                //---------------------------------------------
                _context.Add(pub);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(publication);
        }

        #endregion

        #region Edit

        // GET: Publications/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publications.SingleOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // POST: Publications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImadeUrl,Description,Like,RePost")]
            Publication publication, PublicationVM model)
        {

            if (id != publication.Id)
            {
                return NotFound();
            }

            var searching = await _context.Publications.SingleOrDefaultAsync(s => s.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    var path = Path.Combine(_environment.WebRootPath, $"images\\{_userManager.GetUserName(User)}\\Publication");

                    _fileUploadService.Upload(path, model.ImageUrl.FileName, model.ImageUrl);
                    var imageUrlContent = $"images/{_userManager.GetUserName(User)}/Publication/{model.ImageUrl.FileName}";

                    searching.Description = publication.Description;
                    searching.ImageUrl = imageUrlContent;

                    _context.Update(searching);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicationExists(publication.Id))
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

            return View(publication);
        }

        #endregion

        #region PublicationUpload

        private Publication Publication(Publication publication, PublicationVM model)
        {
            var path = Path.Combine(_environment.WebRootPath, $"images\\{_userManager.GetUserName(User)}\\Publication");

            _fileUploadService.Upload(path, model.ImageUrl.FileName, model.ImageUrl);
            var imageUrlContent = $"images/{_userManager.GetUserName(User)}/Publication/{model.ImageUrl.FileName}";

            var pub = new Publication
            {
                ImageUrl = imageUrlContent,
                Description = publication.Description
            };
            return pub;
        }

//        // для тестов
//        private Publication PublicationEdit(Publication publication, PublicationVM model)
//        {
//            var path = Path.Combine(_environment.WebRootPath, $"images\\{_userManager.GetUserName(User)}\\Publication");
//
//            _fileUploadService.Upload(path, model.ImageUrl.FileName, model.ImageUrl);
//            var imageUrlContent = $"images/{_userManager.GetUserName(User)}/Publication/{model.ImageUrl.FileName}";
//
//            var pub = new Publication
//            {
//                ImageUrl = imageUrlContent,
//                Description = publication.Description
//            };
//            return pub;
//        }

        #endregion

        #region Delete

        // GET: Publications/Delete/5
        // [AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publications
                .SingleOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publication = await _context.Publications.SingleOrDefaultAsync(m => m.Id == id);
            _context.Publications.Remove(publication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicationExists(int id)
        {
            return _context.Publications.Any(e => e.Id == id);
        }

        #endregion

        #region LikeMethod

        public ActionResult LikeMethod(int like, int userId, string postId)
        {
            var userLike = _userManager.Users.Where(u => u.Id == postId);
            if (ModelState.IsValid)
            {
                var post = new Publication
            {
                Like = like + 1,
                Id = userId
            };
                
                _context.Add(post);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(userLike);
        }

        #endregion
 
    }
}