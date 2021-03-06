﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
using Microsoft.Extensions.Localization;

namespace Laboratory56.Controllers
{
    public class PublicationsController : Controller
    {
        #region Conections and Constructor

        public PublicationsController(ApplicationDbContext context, IHostingEnvironment environment,
            FileUploadService fileUploadService, UserManager<ApplicationUser> userManager,
            IStringLocalizer<HomeController> localizer)
        {
            _context = context;
            _environment = environment;
            _fileUploadService = fileUploadService;
            _userManager = userManager;
            _localizer = localizer;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly FileUploadService _fileUploadService;
        private readonly IStringLocalizer<HomeController> _localizer;

        #endregion

        #region Index

        // GET: Publications
        public async Task<IActionResult> Index()
        {
            ViewData["Publication"] = _localizer["Публикации"];
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var sort = await _context.Publications
                    .Include(s => s.User)
                    .Where(p => p.UserId == user.Id)
                    .OrderByDescending(s => s.Id)
                    .ToListAsync();

                return View(sort);
            }
            else
            {
                var sort = await _context.Publications
                    .Include(s => s.User)
                    .OrderByDescending(s => s.Id)
                    .ToListAsync();

                return View(sort);
            }
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
                var user = await _userManager.GetUserAsync(User);
                var pub = Publication(publication, model);
                pub.UserId = user.Id;
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
                    var path = Path.Combine(_environment.WebRootPath,
                        $"images\\{_userManager.GetUserName(User)}\\Publication");

                    _fileUploadService.Upload(path, model.ImageUrl.FileName, model.ImageUrl);
                    var imageUrlContent =
                        $"images/{_userManager.GetUserName(User)}/Publication/{model.ImageUrl.FileName}";

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

        public ActionResult LikeMethod(string userId, int postId)
        {
            var userLike = _context.Publications
                .Where(l => l.Like == 0)
                .FirstOrDefault(u => u.Id == postId);

            if (ModelState.IsValid)
            {
                if (userLike != null)
                {
                    userLike.Like = userLike.Like + 1;

                    _context.Update(userLike);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        #endregion

        #region DisLikeMethod

        public ActionResult DisLikeMethod(string userId, int postId)
        {
            var userLike = _context.Publications
                .Where(l => l.Like != 0)
                .FirstOrDefault(u => u.Id == postId);
            if (ModelState.IsValid)
            {
                if (userLike != null)
                {
                    userLike.Like = userLike.Like - 1;


                    _context.Update(userLike);
                    _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        #endregion

        #region SubscriptionMethod

        #region AsyncMethod

        /*public async Task<ActionResult> SubscriptionMethod(string userId, int postId)
        {
            var countSub = _context.Publications.FirstOrDefault(c => c.Id == postId);

            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserId(User);
                var searchUser = _context.Subscriptions
                    .FirstOrDefault(s => s.SubscribersId == userId && s.SubscribedId == user);

                if (searchUser == null)
                {
                    var sub = new Subscription
                    {
                        SubscribersId = userId, //На кого подписываються
                        SubscribedId = user, // Кто подписалься
                        SubImageUrl = countSub?.ImageUrl
                    };
                    if (countSub != null)
                    {
                        countSub.SubCount = countSub.SubCount + 1;

                       await _context.Add(sub);
                       await _context.UpdateAsync(countSub);
                    }

                    _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            return View();
        }*/

        #endregion

        #region No Async

        public ActionResult SubscriptionMethod(string userId, int postId)
        {
            var countSub = _context.Publications.FirstOrDefault(c => c.Id == postId);

            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserId(User);
                var searchUser = _context.Subscriptions
                    .FirstOrDefault(s => s.SubscribersId == userId && s.SubscribedId == user);

                if (searchUser == null)
                {
                    var sub = new Subscription
                    {
                        SubscribersId = userId, //На кого подписываються
                        SubscribedId = user, // Кто подписалься
                        SubImageUrl = countSub?.ImageUrl
                    };
                    if (countSub != null)
                    {
                        countSub.SubCount = countSub.SubCount + 1;

                        _context.Add(sub);
                        _context.Update(countSub);
                    }

                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        #endregion

        #endregion

        #region UnSubscriptionMethod

        /*
                public ActionResult UnSubscriptionMethod(string userId, int postId)
                {
                    var userLike = _context.Publications.FirstOrDefault(u => u.Id == postId);
                    if (ModelState.IsValid)
                    {
                        if (userLike != null)
                        {
                            userLike.Subscription = userLike.Subscription - 1;

                            _context.Update(userLike);
                            _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }

                    return View();
                }
                */

        #endregion
    }
}