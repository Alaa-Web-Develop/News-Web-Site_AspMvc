using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsWebSite_ASP.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.IO;

namespace NewsWebSite_ASP.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class TeamMembersController : Controller
    {
        private readonly NewContext _context;
        private IHostingEnvironment Environment;
        public TeamMembersController(NewContext context, IHostingEnvironment _envir)
        {
            Environment = _envir;
            _context = context;
        }

        // GET: TeamMembers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teammembers.ToListAsync());
        }

        // GET: TeamMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teammembers == null)
            {
                return NotFound();
            }

            var teamMember = await _context.Teammembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }

        // GET: TeamMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeamMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamMember teamMember, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                //upload and copy image :
                string projectPath = Environment.WebRootPath;

                if (file != null)
                {
                    string imageName = Guid.NewGuid().ToString();
                    string imgextension = Path.GetExtension(file.FileName);
                    string fullimgPath = Path.Combine(projectPath, @"images\TeamMember");

                    using (var filestream = new FileStream(Path.Combine(fullimgPath, imageName + imgextension), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    teamMember.Image = @"\images\teamMember\" + imageName + imgextension;
                }
                _context.Add(teamMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teamMember);
        }

        // GET: TeamMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teammembers == null)
            {
                return NotFound();
            }

            var teamMember = await _context.Teammembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }
            return View(teamMember);
        }

        // POST: TeamMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamMember teamMember, IFormFile? file)
        {
            if (id != teamMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string projectPath = Environment.WebRootPath;
                    if (file != null)
                    {
                        string imageName = Guid.NewGuid().ToString();
                        string imgextension = Path.GetExtension(file.FileName);
                        string fullimgPath = Path.Combine(projectPath, @"images\TeamMember");

                        using (var filestream = new FileStream(Path.Combine(fullimgPath, imageName + imgextension), FileMode.Create))
                        {
                            file.CopyTo(filestream);
                        }
                        teamMember.Image = @"\images\teamMember\" + imageName + imgextension;
                    }

                    _context.Update(teamMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamMemberExists(teamMember.Id))
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
            return View(teamMember);
        }

        // GET: TeamMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teammembers == null)
            {
                return NotFound();
            }

            var teamMember = await _context.Teammembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }

        // POST: TeamMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teammembers == null)
            {
                return Problem("Entity set 'NewContext.Teammembers'  is null.");
            }
            var teamMember = await _context.Teammembers.FindAsync(id);
            if (teamMember != null)
            {
                _context.Teammembers.Remove(teamMember);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamMemberExists(int id)
        {
            return _context.Teammembers.Any(e => e.Id == id);
        }
    }
}
