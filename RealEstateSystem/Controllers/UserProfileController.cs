using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateSystem.Data;
using RealEstateSystem.Models;
using RealEstateSystem.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateSystem.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public UserProfileController(UserManager<ApplicationUser> userManager , ApplicationDbContext _db)
        {
            _userManager = userManager;
            this._db = _db;
        }

        [AllowAnonymous]
        public async Task<IActionResult> ShowUser(string id)
        {
            HomeVM model = new HomeVM();
            var properies = _db.Properties
                .Include(x => x.District)
                .Include(x => x.OperationType)
                .Include(x => x.PopertType)
                .Include(x => x.PropertyStatus)
                .Include(x => x.Land)
                .Include(x => x.propertyImages)
                .Include(x => x.Home)
                .OrderBy(x => x.datetime).Where(x => x.CreatedBy == id);

            model.Properties = await properies.ToListAsync();
            model.Users = await _db.Users.ToListAsync();
            model.CurrentUser = _userManager.Users.Include(x => x.UserType).Where(x => x.Id == id).FirstOrDefault();
            return View("Index",model);
        }

        //[AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            if(userid == null)
            {
                return RedirectToAction( "Login", "Identity//Account" );
            }
            //else
            //{
            //    ApplicationUser user = _userManager.FindByIdAsync(userid).Result;
            //    return View(user);
            //}

            HomeVM model = new HomeVM();
            var properies = _db.Properties
                .Include(x => x.District)
                .Include(x => x.OperationType)
                .Include(x => x.PopertType)
                .Include(x => x.PropertyStatus)
                .Include(x => x.Land)
                .Include(x => x.propertyImages)
                .Include(x => x.Home)
                .OrderBy(x => x.datetime).Where(x=>x.CreatedBy==userid);

            model.Properties = await properies.ToListAsync();
           model.Users=await _db.Users.ToListAsync();
            model.CurrentUser = _userManager.Users.Include(x=>x.UserType).Where(x=>x.Id==userid).FirstOrDefault();
            return View(model);
        }
    }
}
