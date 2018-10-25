using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laboratory56.Data;
using Laboratory56.Models;

namespace Laboratory56.Controllers
{
    public class CommentsController : Controller
    {
        #region Conection and ctor

        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Index

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comments.ToListAsync());
        }

        #endregion

        #region Details

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            ViewBag.Comment = _context.Comments.Where(c => c.PostId == id);

            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
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

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,PostId,CommentDate")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        #endregion

        #region Edit

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CommentId,PostId,CommentDate")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
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

        #endregion

        #region Delete

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.CommentId == id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(string id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }

        #endregion

        #region Comment

        #region version 1

        [HttpPost]
        public async Task<IActionResult> Comment(string postId, int userId, string content)
        {
//            Comment comment = new Comment();
            var comment = _context.Comments.SingleOrDefault(c => c.UserId == userId);
            if (ModelState.IsValid)
            {
                comment.UserId = userId;
                comment.PostId = postId;
                comment.CommentDate = DateTime.Now;
                comment.Content = content;

                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(comment);

        }

        #endregion

        #endregion

    }
}