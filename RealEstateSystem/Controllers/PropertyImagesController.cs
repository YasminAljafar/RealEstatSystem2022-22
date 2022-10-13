using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateSystem.Data;
using RealEstateSystem.Models;
using RealEstateSystem.ViewModels;

namespace RealEstateSystem.Controllers
{
    public class PropertyImagesController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly ApplicationDbContext _hostingEnvironment;

        public PropertyImagesController(ApplicationDbContext context, ApplicationDbContext hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: PropertyImages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PropertyImages.Include(p => p.Property);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PropertyImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PropertyImages == null)
            {
                return NotFound();
            }

            var propertyImage = await _context.PropertyImages
                .Include(p => p.Property)
                .FirstOrDefaultAsync(m => m.id == id);
            if (propertyImage == null)
            {
                return NotFound();
            }

            return View(propertyImage);
        }

        // GET: PropertyImages/Create
        public IActionResult Create()
        {
            PropertyViewModel vm =new PropertyViewModel();
            ViewBag.Images = new SelectList(_context.Properties.ToList(), "Id", "Description");
      
            //ViewData["PropertyId"] = new SelectList(_context.Properties.ToList(), "Id", "Description");
            return View(vm);
        }

        // POST: PropertyImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(propertyImage);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));

                foreach(var item in vm.Images)
                {
                    string stringFileName=UploadFile(item);
                    var propertyImage = new PropertyImage
                    {
                        imageUrl = stringFileName,
                        Property = vm.Property
                    };
                    _context.PropertyImages.Add(propertyImage);
                }
                _context.SaveChanges();

             
            }
            //ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Description", propertyImage.PropertyId);
            //return View(propertyImage);

            return RedirectToAction("Index");
        }

        private string UploadFile(IFormFile file)
        {
            string fileName = null;
            if(file != null)
            {
                string uploadDirection = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads\\Properties");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath=Path.Combine(uploadDirection, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return fileName;
        }
        // GET: PropertyImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PropertyImages == null)
            {
                return NotFound();
            }

            var propertyImage = await _context.PropertyImages.FindAsync(id);
            if (propertyImage == null)
            {
                return NotFound();
            }
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Description", propertyImage.PropertyId);
            return View(propertyImage);
        }

        // POST: PropertyImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,imageUrl,Description,PropertyId")] PropertyImage propertyImage)
        {
            if (id != propertyImage.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyImageExists(propertyImage.id))
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
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Description", propertyImage.PropertyId);
            return View(propertyImage);
        }

        // GET: PropertyImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PropertyImages == null)
            {
                return NotFound();
            }

            var propertyImage = await _context.PropertyImages
                .Include(p => p.Property)
                .FirstOrDefaultAsync(m => m.id == id);
            if (propertyImage == null)
            {
                return NotFound();
            }

            return View(propertyImage);
        }

        // POST: PropertyImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PropertyImages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PropertyImages'  is null.");
            }
            var propertyImage = await _context.PropertyImages.FindAsync(id);
            if (propertyImage != null)
            {
                _context.PropertyImages.Remove(propertyImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyImageExists(int id)
        {
          return _context.PropertyImages.Any(e => e.id == id);
        }
    }
}
