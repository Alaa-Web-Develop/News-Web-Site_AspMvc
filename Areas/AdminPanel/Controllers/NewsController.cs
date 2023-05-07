using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsWebSite_ASP.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace NewsWebSite_ASP.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class NewsController : Controller
    {
        private readonly NewContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
        public NewsController(NewContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            _context = context;
            Environment = _environment;//receive info about Hosting
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var newContext = _context.News.Include(n => n.Cateogry);
            return View(await newContext.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Cateogry)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["CateogryId"] = new SelectList(_context.Cateogries, "Id", "Name");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(News news, IFormFile? file)
        {
            
            if (ModelState.IsValid)
            {
                string wwwrootpath = Environment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwrootpath, @"images\News");
                    var extension = Path.GetExtension(file.FileName);
                    using (var filestream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    news.Image = @"\images\News\" + filename + extension;
                }
                
                _context.Add(news);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CateogryId"] = new SelectList(_context.Cateogries, "Id", "Name", news.CateogryId);
            return View(news);
        }

        //===========================Method to copy image to project=====================================
        //private void uploadphoto(News model)
        //{
        //    if (model.File != null)
        //    {
        //        string projPath = Path.Combine(this.Environment.WebRootPath,@"images\News");
        //        string uniqueimgname = Guid.NewGuid().ToString()+ ".jpg";
        //        string imgPathInProj = Path.Combine(projPath, uniqueimgname);

        //        using (var filestream = new FileStream(imgPathInProj, FileMode.Create))//str path,fileMode
        //        {
        //            model.File.CopyTo(filestream);//copyto(stream taget);

        //        }
        //        model.Image = uniqueimgname;

        //    }
        //}

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["CateogryId"] = new SelectList(_context.Cateogries, "Id", "Name", news.CateogryId);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, News news)
        {
            //ModelState.Remove("Cateogry");

            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            ViewData["CateogryId"] = new SelectList(_context.Cateogries, "Id", "Name", news.CateogryId);
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Cateogry)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'NewContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
