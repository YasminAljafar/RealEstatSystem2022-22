using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateSystem.Data;
using RealEstateSystem.Models;
using RealEstateSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateSystem.Controllers
{
    [Authorize/*(Roles = "User")*/]
    public class PropertiesController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hosting;
        private readonly UserManager<ApplicationUser> _userManager;

        public object ApplicationUserName { get; private set; }

        public PropertiesController(ApplicationDbContext db, IWebHostEnvironment hosting, IHttpContextAccessor hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _hosting = hosting;
            _userManager = userManager;
        }
        public JsonResult Governorate()
        {
            var governorate = _db.Governorates.ToList();
            return new JsonResult(governorate);

        }
        public JsonResult City(int id)
        {
            var city = _db.Cities.Where(e => e.Governorate.Id == id).ToList();
            return new JsonResult(city);
        }

        public JsonResult City1(int id)
        {
            var city = _db.Cities.ToList();
            return new JsonResult(city);
        }
        public JsonResult District(int id)
        {
            var district = _db.Districts.Where(e => e.City.Id == id).ToList();
            return new JsonResult(district);
        }

        [AllowAnonymous]
        //for visitor
        public async Task<IActionResult> Index(PropertyFilterParams filterParams)
        {
            var userName = _userManager.GetUserName(HttpContext.User);
            HomeVM model = new HomeVM();
            var properies = _db.Properties
                .Include(x => x.District)
                .Include(x => x.OperationType)
                .Include(x => x.PopertType)
                .Include(x => x.PropertyStatus)
                .Include(x => x.Land)
                .Include(x => x.propertyImages)
                .Include(x => x.Home)
                .AsQueryable();


            //Applay Filters
            if (filterParams.PropertyType != null)
                properies = properies.Where(x => x.PropertyTypeId == filterParams.PropertyType.Value);

            if (filterParams.OperationType != null)
                properies = properies.Where(x => x.OperationTypeId == filterParams.OperationType.Value);

            if (filterParams.City != null)
                properies = properies.Where(x => x.District.CityId == filterParams.City.Value);

            if (filterParams.District != null)
                properies = properies.Where(x => x.DistrictId == filterParams.District.Value);

            if (!string.IsNullOrWhiteSpace(filterParams.roomcount))
            {
                if (filterParams.roomcount != "more")
                {
                    var rCount = Convert.ToInt32(filterParams.roomcount);
                    properies = properies.Where(x => x.Home.RoomCount == rCount);
                }
                else
                {
                    properies = properies.Where(x => x.Home.RoomCount > 5);
                }
            }

            //min Price
            if (filterParams.price != null)
                properies = properies.Where(x => x.Price <= filterParams.price);


            switch (filterParams.OrderType)
            {
                case PropertyOrderType.Normal:
                    //No Action Needed
                    break;
                case PropertyOrderType.Newer:
                    properies = properies.OrderByDescending(x => x.datetime);
                    break;
                case PropertyOrderType.LowerProce:
                    properies = properies.OrderBy(x => x.Price);
                    break;
                default:
                    break;
            }

            properies = properies.Skip((filterParams.PageNumber - 1) * filterParams.PageSize).Take(filterParams.PageSize);

            model.FilterParams = filterParams;
            model.Properties = await properies.ToListAsync();
            model.Advertisings = await _db.Advertisings.ToListAsync();
            model.Users = await _db.Users.ToListAsync();
            model.Districts = await _db.Districts.ToListAsync();
            model.Cities = await _db.Cities.ToListAsync();
            model.Governorates = await _db.Governorates.ToListAsync();
            model.OperationTypes = await _db.OperationTypes.ToListAsync();
            model.PropertyTypes = await _db.PropertyTypes.ToListAsync();
            model.CurrentUser = _userManager.Users.Include(x => x.UserType).Where(x => x.UserName == userName).FirstOrDefault();

            //ApplicationUserId = a.ApplicationUserId,
            //ApplicationUserName = _userManager.Users.Where(x => x.Id == a.ApplicationUserId).Select(x => x.UserName).FirstOrDefault()
            //var applicationDbContext = _db.Properties.Include(x => x.District).Include(x => x.OperationType).Include(x => x.PopertType).Include(x => x.PropertyStatus).Include(x => x.Land).Include(x => x.propertyImages).Include(x => x.Home);
            //var res = await applicationDbContext.OrderBy(x => x.Price).ToListAsync();
            return View(model);

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string term)
        {
            var model = _db.Properties.Where(m => m.Name.Contains(term))
                 .Select(m => new Property
                 {
                     Name = m.Name,
                     Description = m.Description,
                     propertyImages = m.propertyImages,
                     PropertyStatus = m.PropertyStatus,
                     Land = m.Land,
                     PopertType = m.PopertType,
                     OperationType = m.OperationType,
                     Home = m.Home,
                     District = m.District,
                     Price = m.Price,
                     
                 });
            return View(model);
        }

        //for user
        public async Task<IActionResult> Index1()
        {
            var applicationDbContext = _db.Properties.Include(x => x.District).Include(x => x.OperationType).Include(x => x.PopertType).Include(x => x.PropertyStatus).Include(x => x.Land).Include(x => x.propertyImages).Include(x => x.Home);
            var res = await applicationDbContext.ToListAsync();


            return View(res);
        }



        [HttpGet]
        public async Task<IActionResult> Create(Property properties)
        {

            ViewData["PropertyId"] = new SelectList(_db.Properties, "Id", "Name");
            var ViewModel = new PropertyViewModel
            {
                PropertyTypes = await _db.PropertyTypes.ToListAsync(),
                PropertyStatuses = await _db.PropertyStatuses.ToListAsync(),
                OperationTypes = await _db.OperationTypes.ToListAsync(),
                Governorates = await _db.Governorates.ToListAsync(),
                Cities = await _db.Cities.ToListAsync(),
                Districts = await _db.Districts.ToListAsync()
            };
            return View("CreateAndEdit", ViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(PropertyViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userid = _userManager.GetUserId(HttpContext.User);
                Property property = new Property()
                {
                    Description = model.Description,
                    DistrictId = model.DistrictId,
                    Name = model.Name,
                    OperationTypeId = model.OperationTypeId,
                    Price = model.Price,
                    PropertyStatusId = model.PropertyStatusId,
                    PropertyTypeId = model.PropertyTypeId,
                    Space = model.Space,
                    lat = model.lat,
                    lng = model.lng,
                    datetime = DateTime.Now,
                    CreatedBy = userid
                };

                _db.Properties.Add(property);
                _db.SaveChanges();
                int id = property.Id;

                if (property.Land != null)
                {
                    Land land1 = new Land()
                    {
                        IsTimberLand = model.IsTimberLand,
                        Length = model.Length,
                        Width = model.Width,
                        WaterWell = model.WaterWell,
                        WaterWellAtNear = model.WaterWellAtNear,
                        LandForConstruction = model.LandForConstruction,
                        LandIsArable = model.LandForConstruction,
                        EarthTypeId = model.EarthTypeId,
                        PropertyId = id,
                    };
                    _db.Lands.Add(land1);
                    _db.SaveChanges();
                }
                Home home = new Home()
                {
                    PropertyId = id,
                    RoomCount = model.RoomCount,
                    Floor = model.Floor,
                    IsSunny = model.IsSunny,
                };
                _db.Homes.Add(home);
                _db.SaveChanges();

                foreach (var item in model.Images)
                {
                    string stringFileName = UploadFile(item);
                    var propertyImage = new PropertyImage
                    {
                        imageUrl = stringFileName,
                        Property = model.Property,
                        PropertyId = id
                    };
                    _db.PropertyImages.Add(propertyImage);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            //Error 

            return View("CreateAndEdit", model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (_db.Properties == null)
            {
                return NotFound();
            }

            var property = await _db.Properties.FindAsync(id);
            var land = _db.Lands.Where(x => x.PropertyId == id).FirstOrDefault();
            var home = _db.Homes.Where(x => x.PropertyId == id).FirstOrDefault();
        
            if (property == null)
                return NotFound();

            PropertyViewModel ViewModel = new PropertyViewModel
            {
                Id = id,
                Description = property.Description,
                Name = property.Name,
                OperationTypeId = property.OperationTypeId,
                Price = property.Price,
                PropertyStatusId = property.PropertyStatusId,
                PropertyTypeId = property.PropertyTypeId,
                Space = property.Space,
                lat = property.lat,
                lng = property.lng,
                datetime = property.datetime,
                PropertyTypes = await _db.PropertyTypes.ToListAsync(),
                PropertyStatuses = await _db.PropertyStatuses.ToListAsync(),
                OperationTypes = await _db.OperationTypes.ToListAsync(),
                DistrictId = property.DistrictId,
                Districts = await _db.Districts.ToListAsync(),
                CityId = property.District.CityId,
                Cities = await _db.Cities.ToListAsync(),
                GovernorateId = property.District.City.GovernorateId,
                Governorates = await _db.Governorates.ToListAsync(),
                CreatedBy = property.CreatedBy,
                IsTimberLand = land.IsTimberLand,
                Width = land.Width,
                Length = land.Length,
                WaterWell = land.WaterWell,
                WaterWellAtNear = land.WaterWellAtNear,
                LandForConstruction = land.LandForConstruction,
                LandIsArable = land.LandIsArable,
                EarthTypeId = land.EarthTypeId,
                PropertyId = land.PropertyId,
                RoomCount = home.RoomCount,
                Floor = home.Floor,
                IsSunny = home.IsSunny,
                PropertyId1 = home.PropertyId,
                propertyImages = await _db.PropertyImages.Where(x => x.PropertyId == id).ToListAsync(),
            };
            return View("CreateAndEdit", ViewModel);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(int id, PropertyViewModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    //Property property = await _db.Properties.FindAsync(model.Id);

                    _db.Database.BeginTransaction();

                    Property property = new Property()
                    {
                        Id = model.Id,
                        Commerial = model.Commerial,
                        Description = model.Description,
                        DistrictId = model.DistrictId,
                        District = model.District,
                        
                        CreatedBy = model.CreatedBy,
                        Name = model.Name,
                        OperationTypeId = model.OperationTypeId,
                        Price = model.Price,
                        PropertyStatusId = model.PropertyStatusId,
                        PropertyTypeId = model.PropertyTypeId,
                        Space = model.Space,
                        lat = model.lat,
                        lng = model.lng,
                        //datetime=model.datetime,
                    };
                    _db.Properties.Update(property);
                    _db.SaveChanges();

                    var oldLand = _db.Lands.Where(x => x.PropertyId == id).FirstOrDefault();
                    if (oldLand != null)
                    {
                        _db.Lands.Remove(oldLand);
                        _db.SaveChanges();
                    }


                    Land land = new Land()
                        {

                            IsTimberLand = model.IsTimberLand,
                            Width = model.Width,
                            Length = model.Length,
                            WaterWell = model.WaterWell,
                            WaterWellAtNear = model.WaterWellAtNear,
                            LandForConstruction = model.LandForConstruction,
                            LandIsArable = model.LandIsArable,
                            EarthTypeId = model.EarthTypeId,
                            PropertyId = id,
                        };
                        _db.Lands.Add(land);
                        _db.SaveChanges();
                    


                   
                        var oldHome = _db.Homes.Where(x => x.PropertyId == id).FirstOrDefault();
                        if (oldHome != null)
                        {
                            _db.Homes.Remove(oldHome);
                            _db.SaveChanges();
                        }

                        Home home = new Home()
                        {
                            //Id = id,
                            RoomCount = model.RoomCount,
                            Floor = model.Floor,
                            IsSunny = model.IsSunny,
                            PropertyId = id
                        };
                        _db.Homes.Add(home);
                        _db.SaveChanges();
                   


                    if (model.Images?.Count > 0)
                    {
                        var oldImages = _db.PropertyImages.Where(x => x.PropertyId == id).ToList();
                        _db.PropertyImages.RemoveRange(oldImages);
                        _db.SaveChanges();

                        foreach (var item in model.Images)
                        {
                            string stringFileName = UploadFile(item);
                            var propertyImage = new PropertyImage
                            {
                                imageUrl = stringFileName,
                                Property = model.Property,
                                PropertyId = id
                            };
                            _db.PropertyImages.Add(propertyImage);
                        }
                        _db.SaveChanges();
                    }
                    _db.Database.CommitTransaction();
                    return RedirectToAction("Index");
                }
                //Error 
                return View(model);
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();
                throw;
            }
        }


        // GET: Properties1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Properties == null)
            {
                return NotFound();
            }

            var _property = await _db.Properties
                .Include(x => x.District)
                .Include(x => x.OperationType)
                .Include(x => x.PopertType)
                .Include(x => x.PropertyStatus)
                //.Include(x=>x.propertyImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            var land = await _db.Lands
              .Include(l => l.EarthType)
              .Include(l => l.Property)
              .FirstOrDefaultAsync(m => m.Id == id);

            var home = await _db.Homes.FirstOrDefaultAsync(m => m.PropertyId == id);
            if (_property == null)
            {
                return NotFound();
            }
            
            if (home != null || land != null)
            {
                _property.Land = land;
                _property.Home = home;
            }

            var images = await _db.PropertyImages.Where(x => x.PropertyId == id).ToListAsync();
            _property.propertyImages = images;

            return View(_property);
        }


        // GET: Properties1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Properties == null)
            {
                return NotFound();
            }

            var @property = await _db.Properties
                .Include(@property => @property.District)
                .Include(@property => @property.OperationType)
                .Include(@property => @property.PopertType)
                .Include(@property => @property.PropertyStatus)
                .FirstOrDefaultAsync(m => m.Id == id);

            var land = await _db.Lands
                .Include(land => land.EarthType)
                .FirstOrDefaultAsync(m => m.Id == id);

            var home = await _db.Homes.FirstOrDefaultAsync(m => m.Id == id);



            if (@property == null)
            {
                return NotFound();
            }

            @property.Land = land;
            @property.Home = home;

            var images = await _db.PropertyImages.Where(x => x.PropertyId == id).ToListAsync();
            @property.propertyImages = images;

            return View(@property);
        }

        // POST: Properties1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Properties == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Properties'  is null.");
            }
            var @property = await _db.Properties.FindAsync(id);
            var land = _db.Lands.Where(x => x.PropertyId == id).FirstOrDefault();
            var home = await _db.Homes.FirstOrDefaultAsync(m => m.Id == id);

            @property.Land = land;
            @property.Home = home;
            if (@property != null)
            {
                _db.Properties.Remove(@property);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
            return _db.Properties.Any(e => e.Id == id);
        }

        private string UploadFile(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                string uploadDirection = Path.Combine(_hosting.WebRootPath, "Uploads\\Properties");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDirection, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return fileName;
        }

        [AllowAnonymous]
        public async Task<IActionResult> SingleProperty(int id)
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            //var user=await _db.Users.Include(x=>x.UserType).FirstOrDefaultAsync(x=>x.c)
            if (_db.Properties == null)
            {
                return NotFound();
            }
            var user = await _db.Users.Include(x => x.UserType).FirstOrDefaultAsync(m=>m.Id==userid);
            var _property = await _db.Properties
                .Include(x => x.District)
                .Include(x => x.OperationType)
                .Include(x => x.PopertType)
                .Include(x => x.PropertyStatus)
                .Include(x=>x.Users)
                .Include(x=>x.UserType)
                .FirstOrDefaultAsync(m => m.Id == id);

            var land = await _db.Lands
              .Include(l => l.EarthType)
              .Include(l => l.Property)
              .FirstOrDefaultAsync(m => m.PropertyId == id);

            var home = await _db.Homes.FirstOrDefaultAsync(m => m.PropertyId == id);
            if (_property == null)
            {
                return NotFound();
            }

            if (home != null || land != null)
            {
                _property.Land = land;
                _property.Home = home;
            }

            var images = await _db.PropertyImages.Where(x => x.PropertyId == id).ToListAsync();
            _property.propertyImages = images;
           

            return View(_property);

        }


    }
}
