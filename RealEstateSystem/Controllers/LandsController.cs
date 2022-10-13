using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateSystem.Data;
using RealEstateSystem.Models;

namespace RealEstateSystem.Controllers
{
    public class LandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lands
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lands.Include(l => l.EarthType).Include(l => l.Property);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Lands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lands == null)
            {
                return NotFound();
            }

            var land = await _context.Lands
                .Include(l => l.EarthType)
                .Include(l => l.Property)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (land == null)
            {
                return NotFound();
            }

            return View(land);
        }

        // GET: Lands/Create
        public IActionResult Create()
        {
            ViewData["EarthTypeId"] = new SelectList(_context.EarthTypes, "id", "Name");
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name");
            return View();
        }

        // POST: Lands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IsTimberLand,Length,Width,WaterWell,WaterWellAtNear,LandForConstruction,LandIsArable,EarthTypeId,PropertyId")] Land land)
        {
            if (ModelState.IsValid)
            {
                _context.Add(land);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EarthTypeId"] = new SelectList(_context.EarthTypes, "id", "Name", land.EarthTypeId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", land.PropertyId);
            return View(land);
        }

        // GET: Lands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lands == null)
            {
                return NotFound();
            }

            var land = await _context.Lands.FindAsync(id);
            if (land == null)
            {
                return NotFound();
            }
            ViewData["EarthTypeId"] = new SelectList(_context.EarthTypes, "id", "Name", land.EarthTypeId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", land.PropertyId);
            return View(land);
        }

        // POST: Lands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IsTimberLand,Length,Width,WaterWell,WaterWellAtNear,LandForConstruction,LandIsArable,EarthTypeId,PropertyId")] Land land)
        {
            if (id != land.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(land);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LandExists(land.Id))
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
            ViewData["EarthTypeId"] = new SelectList(_context.EarthTypes, "id", "Name", land.EarthTypeId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Name", land.PropertyId);
            return View(land);
        }

        // GET: Lands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lands == null)
            {
                return NotFound();
            }

            var land = await _context.Lands
                .Include(l => l.EarthType)
                .Include(l => l.Property)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (land == null)
            {
                return NotFound();
            }

            return View(land);
        }

        // POST: Lands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lands == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lands'  is null.");
            }
            var land = await _context.Lands.FindAsync(id);
            if (land != null)
            {
                _context.Lands.Remove(land);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LandExists(int id)
        {
          return _context.Lands.Any(e => e.Id == id);
        }
    }
}
