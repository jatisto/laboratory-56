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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Laboratory56.Controllers
{
    public class CommentsController : Controller
    {
        #region Conection and ctor

        public CommentsController(ApplicationDbContext context, IHostingEnvironment environment,
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

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var comments = await _context.Comments
                    .Include(c => c.User)
                    .Include(c => c.Post)
                    .Where(p => p.UserId == user.Id)
                    .OrderByDescending(c => c.PostId).ToListAsync();
                return View(comments);
            }
            else
            {
                var comments = await _context.Comments
                    .Include(c => c.User)
                    .Include(c => c.Post)
                    .OrderByDescending(c => c.PostId).ToListAsync();
                return View(comments);
            }
            
        }

        #endregion

        #region Details

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int id, PublicationVM model)
        {
            ViewBag.Comment = _context.Comments.Where(c => c.PostId == id);
           
            if (id == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments
                .Include(c => c.Post)
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        #endregion

        #region Create

// GET: Comments/Create
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,PostId,CommentDate")] Comment comment)
        {
            var publ = _context.Publications.FirstOrDefault(c => c.Id == comment.PostId);
            if (publ != null) comment.ImageUrl = publ.ImageUrl;


            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(comment);
        }



        #endregion

        #region Edit


        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            /*if (id == null)
            {
                return NotFound();
            }*/

            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,PostId,CommentDate,Content")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }
            var searching = await _context.Comments.SingleOrDefaultAsync(s => s.CommentId == id);

            if (ModelState.IsValid)
            {
                try
                {
                    
                    searching.Content = comment.Content;

                    _context.Update(searching);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentId))
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

            return View(comment);
        }

//        private bool CommentExists(int commentId)
//        {
//            throw new NotImplementedException();
//        }

        #endregion

        #region Delete

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            /*if (id == null)
            {
                return NotFound();
            }*/

            var comment = await _context.Comments
                .SingleOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

// POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.CommentId == id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }

        #endregion

        #region Comment

        #region version 1

        [HttpPost]
        public async Task<IActionResult> Comment(int postId, string userId, string content, Comment comment)
        {
            var publ = _context.Publications.FirstOrDefault(c => c.Id == comment.PostId);
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var comm = new Comment
                {
                    UserId = userId,
                    PostId = postId,
                    Content = content,
                    ImageUrl = publ?.ImageUrl,
                    CommentDate = DateTime.Now
                };

                comm.UserId = user.Id;
                _context.Add(comm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        #endregion

        #region version 2

        /*[HttpPost]
        public async Task<IActionResult> Comment(int postId, string userId, string content, Comment comment)
        {
            var publ = _context.Publications.FirstOrDefault(c => c.Id == comment.PostId);

            if (publ == null) return NotFound();

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var comm = new Comment
                {
                    UserId = userId,
                    PostId = postId,
                    Content = content,
//                    ImageUrl = publ != null ? publ.ImageUrl : null,
                    ImageUrl = publ.ImageUrl,
                    CommentDate = DateTime.Now
                };
                comm.UserId = user.Id;
                _context.Add(comm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }*/

        #endregion

        #endregion
    }
}