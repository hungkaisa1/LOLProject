using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LOLProject.Data;
using LOLProject.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
//Add reference to authorization nuget package
using Microsoft.AspNetCore.Authorization;

namespace LOLProject.Controllers
{
    public class ChampionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        //Dependency Injection
        public ChampionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Champions
        // Search function (by Champion Name)
        [AllowAnonymous]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index(string champSearch)
        {
            ViewData["ChampionName"] = champSearch;
            var champQuery = from x in _context.Champions.Include(c => c.Position).Include(c => c.Role).OrderBy(o => o.ChampionName) select x;
            if (!String.IsNullOrEmpty(champSearch))
            {
                champQuery = champQuery.Where(x => x.ChampionName.Contains(champSearch));
            }
            return View(await champQuery.AsNoTracking().ToListAsync());
        }

        // GET: Champions/Details/5
        // This attribute will allow anybody  to access Champions/Details/{id}
        [AllowAnonymous]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var champion = await _context.Champions
                .Include(c => c.Position)
                .Include(c => c.Role)
                .FirstOrDefaultAsync(m => m.ChampionId == id);
            if (champion == null)
            {
                return NotFound();
            }

            return View(champion);
        }

        // GET: Champions/Create.
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions.OrderBy(a => a.PositionName), "PositionId", "PositionName");
            ViewData["RoleId"] = new SelectList(_context.Roles.OrderBy(c => c.RoleName), "RoleId", "RoleName");
            return View("Create");
        }

        // POST: Champions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChampionId,ChampionName,RoleId,Price,PositionId,Description")] Champion champion, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                // upload photo and attach to the new product if any
                if (Image != null)
                {
                    var filePath = Path.GetTempFileName(); // get image from cache
                    var fileName = Guid.NewGuid() + "-" + Image.FileName; // add unique id as prefix to file name
                    var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\champions\\" + fileName;

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    // add unique Image file name to the new product object before saving
                    champion.Image = fileName;
                }

                _context.Add(champion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName", champion.PositionId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", champion.RoleId);
            return View(champion);
        }

        // GET: Champions/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var champion = await _context.Champions.FindAsync(id);
            if (champion == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = new SelectList(_context.Positions.OrderBy(x => x.PositionName), "PositionId", "PositionName", champion.PositionId);
            ViewData["RoleId"] = new SelectList(_context.Roles.OrderBy(b => b.RoleName), "RoleId", "RoleName", champion.RoleId);
            return View(champion);
        }

        // POST: Champions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ChampionId,ChampionName,RoleId,Price,PositionId,Description")] Champion champion, IFormFile Image, string CurrentImage)
        {
            if (id != champion.ChampionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // upload photo and attach to the new product if any
                    if (Image != null)
                    {
                        var filePath = Path.GetTempFileName(); // get image from cache
                        var fileName = Guid.NewGuid() + "-" + Image.FileName; // add unique id as prefix to file name
                        var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\champions\\" + fileName;

                        using (var stream = new FileStream(uploadPath, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }

                        // add unique Image file name to the new product object before saving
                        champion.Image = fileName;
                    }
                    else
                    {
                        //keep current image from getting wiped out if nothing new uploaded.
                        champion.Image = CurrentImage;
                    }
                    _context.Update(champion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChampionExists(champion.ChampionId))
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
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionId", champion.PositionId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", champion.RoleId);
            return View(champion);
        }

        // GET: Champions/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var champion = await _context.Champions
                .Include(c => c.Position)
                .Include(c => c.Role)
                .FirstOrDefaultAsync(m => m.ChampionId == id);
            if (champion == null)
            {
                return NotFound();
            }

            return View(champion);
        }

        // POST: Champions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var champion = await _context.Champions.FindAsync(id);

            //remove img file if any
            var Image = champion.Image;
            //delete file from wwwroot\img\champions first

            _context.Champions.Remove(champion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChampionExists(int id)
        {
            return _context.Champions.Any(e => e.ChampionId == id);
        }
    }
}
