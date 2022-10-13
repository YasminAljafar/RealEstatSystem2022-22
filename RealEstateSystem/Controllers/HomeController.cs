using RealEstateSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RealEstateSystem.Data;
using RealEstateSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace RealEstateSystem2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> IndexAsync()
        {
            HomeVM model = new HomeVM();
            model.Properties = await _db.Properties.Include(x => x.District).Include(x => x.OperationType).Include(x => x.PopertType).Include(x => x.PropertyStatus).Include(x => x.Land).Include(x => x.propertyImages).Include(x => x.Home).ToListAsync();
            model.Advertisings=await _db.Advertisings.ToListAsync();
            model.Users=await _db.Users.ToListAsync();
            //var applicationDbContext = _db.Properties.Include(x => x.District).Include(x => x.OperationType).Include(x => x.PopertType).Include(x => x.PropertyStatus).Include(x => x.Land).Include(x => x.propertyImages).Include(x => x.Home);
            //var res = await applicationDbContext.OrderBy(x => x.Price).ToListAsync();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
