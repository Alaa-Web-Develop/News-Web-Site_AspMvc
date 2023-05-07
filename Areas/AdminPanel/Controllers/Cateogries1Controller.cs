using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsWebSite_ASP.Models;

namespace NewsWebSite_ASP.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class Cateogries1Controller : Controller
    {
        private readonly NewContext _context;

        public Cateogries1Controller(NewContext context)
        {
            _context = context;
        }

        // GET: Cateogries1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cateogries.ToListAsync());
        }

        // GET: Cateogries1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cateogries == null)
            {
                return NotFound();
            }

            var cateogry = await _context.Cateogries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cateogry == null)
            {
                return NotFound();
            }

            return View(cateogry);
        }

        // GET: Cateogries1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cateogries1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cateogry cateogry)
        {
            ModelState.Remove("News");
            if (ModelState.IsValid)
            {
                _context.Add(cateogry);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cateogry);
        }

        // GET: Cateogries1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Cateogries == null)
            {
                return NotFound();
            }

            var cateogry = await _context.Cateogries.FindAsync(id);
            if (cateogry == null)
            {
                return NotFound();
            }
            return View(cateogry);
        }

        // POST: Cateogries1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Cateogry cateogry)
        {
            ModelState.Remove("News");
            if (id != cateogry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cateogry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CateogryExists(cateogry.Id))
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
            return View(cateogry);
        }

        // GET: Cateogries1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cateogries == null)
            {
                return NotFound();
            }

            var cateogry = await _context.Cateogries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cateogry == null)
            {
                return NotFound();
            }

            return View(cateogry);
        }

        // POST: Cateogries1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cateogries == null)
            {
                return Problem("Entity set 'NewContext.Cateogries'  is null.");
            }
            var cateogry = await _context.Cateogries.FindAsync(id);
            if (cateogry != null)
            {
                _context.Cateogries.Remove(cateogry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CateogryExists(int id)
        {
            return _context.Cateogries.Any(e => e.Id == id);
        }
    }
}
