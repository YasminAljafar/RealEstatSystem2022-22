using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RealEstateSystem.Data;
using RealEstateSystem.Models;
using RealEstateSystem.ViewModels;

namespace RealEstateSystem.Controllers
{
    public class AdvertisingsController : Controller
    {
        private readonly ApplicationDbContext _context;
     
        private readonly IWebHostEnvironment Hosting;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdvertisingsController(ApplicationDbContext context, IWebHostEnvironment hosting, UserManager<ApplicationUser> userManager) 
        {
            _context = context;
            Hosting = hosting;
            _userManager = userManager;
        }

        // GET: Advertisings
        public async Task<IActionResult> Index()
        {
              return View(await _context.Advertisings.ToListAsync());
        }

        // GET: Advertisings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Advertisings == null)
            {
                return NotFound();
            }

            var advertising = await _context.Advertisings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertising == null)
            {
                return NotFound();
            }


            return View(advertising);
        }

        // GET: Advertisings/Create
        public async Task<IActionResult> CreateAsync()
        {
            //var ss = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //HttpContext.User.Claims
            //var user = await _userManager.GetUserAsync(HttpContext.User);

            return View();
        }

        // POST: Advertisings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,Image,Title,Description,PublishDate,ExpiryDate,Count,Status,ApplicationUserId")] Advertising advertising*/  AdvertisigViewModel advertising)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (advertising.ImageFile != null)
                {
                    string uploads = Path.Combine(Hosting.WebRootPath, "Uploads\\Advertisings");
                    fileName = advertising.ImageFile.FileName;
                    string fullPath = Path.Combine(uploads, fileName);
                    advertising.ImageFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                var model = new Advertising
                {
                    Id = advertising.Id,
                    ImageURL = fileName,
                    Title = advertising.Title,
                    Description = advertising.Description,
                    PublishDate = DateTime.Now,
                    ApplicationUserId = advertising.ApplicationUserId
                };
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advertising);
        }

        // GET: Advertisings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Advertisings == null)
            {
                return NotFound();
            }

            var advertising = await _context.Advertisings.FindAsync(id);
            if (advertising == null)
            {
                return NotFound();
            }
            return View(advertising);
        }

        // POST: Advertisings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Title,Description,PublishDate,ExpiryDate,Count,Status,ApplicationUserId")] Advertising advertising)
        {
            if (id != advertising.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertising);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisingExists(advertising.Id))
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
            return View(advertising);
        }

        // GET: Advertisings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Advertisings == null)
            {
                return NotFound();
            }

            var advertising = await _context.Advertisings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertising == null)
            {
                return NotFound();
            }

            return View(advertising);
        }

        // POST: Advertisings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Advertisings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Advertisings'  is null.");
            }
            var advertising = await _context.Advertisings.FindAsync(id);
            if (advertising != null)
            {
                _context.Advertisings.Remove(advertising);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisingExists(int id)
        {
          return _context.Advertisings.Any(e => e.Id == id);
        }
    }
}
