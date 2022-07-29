using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateSystem.Data;
using RealEstateSystem.Models;
using RealEstateSystem.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateSystem.Controllers
{
    public class PropertiesController : Controller
    {

        private readonly ApplicationDbContext _db;
        public PropertiesController(ApplicationDbContext db)
        {
            _db = db;

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
        public JsonResult District(int id)
        {
            var district = _db.Districts.Where(e => e.City.Id == id).ToList();
            return new JsonResult(district);
        }
        public async Task<IActionResult> Index()
        {
            var properties = await _db.Properties.ToListAsync();
            return View(properties);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var ViewModel = new PropertyViewModel
            {
                PropertyTypes = await _db.PropertyTypes.ToListAsync(),
                PropertyStatuses = await _db.PropertyStatuses.ToListAsync(),
                OperationTypes = await _db.OperationTypes.ToListAsync(),
            };
            return View(ViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(PropertyViewModel model)
        {


            if (ModelState.IsValid)
            {
                Property property = new Property()
                {
                    Commerial=model.Commerial,
                    Description = model.Description,
                    DistrictId = model.DistrictId,
                    Name =model.Name,
                    OperationTypeId=model.OperationTypeId,
                    Price=model.Price,
                    PropertyStatusId=model.PropertyStatusId,
                    PropertyTypeId=model.PropertyTypeId,
                    Space=model.Space,
                };

                _db.Properties.Add(property);
                _db.SaveChanges();

                return RedirectToAction("Edit", new { id = property.Id });

               // return View("Index");//منبدلها باللي فوق;
            }

            //Error 
           
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {

            var property = await _db.Properties.FindAsync(id);

            if (property == null)
                return NotFound();



            var ViewModel = new PropertyViewModel
            {
                Id = id,
                //Age = property.Age,
                Commerial = property.Commerial,
                Description = property.Description,
                //DistrictId = property.DistrictId,
                //IsPuplish = property.IsPuplish,
                //MeterPrice = property.MeterPrice,
                Name = property.Name,
                OperationTypeId = property.OperationTypeId,
                //Position = property.Position,
                Price = property.Price,
                PropertyStatusId = property.PropertyStatusId,
                PropertyTypeId = property.PropertyTypeId,
                Space = property.Space,
            //    propertyImages = property.propertyImages,
                

                //Districts = await _db.Districts.ToListAsync(),
                PropertyTypes = await _db.PropertyTypes.ToListAsync(),
                PropertyStatuses = await _db.PropertyStatuses.ToListAsync(),
                OperationTypes = await _db.OperationTypes.ToListAsync(),
            };
            return View(ViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PropertyViewModel model)
        {

            if (ModelState.IsValid)
            {
                //Property property = await _db.Properties.FindAsync(model.Id);

                Property property = new Property()
                {
                    Id = model.Id,
                    //Age = model.Age,
                    Commerial = model.Commerial,
                    Description = model.Description,
                    DistrictId = model.DistrictId,
                    //IsPuplish = model.IsPuplish,
                 //   MeterPrice = model.MeterPrice,
                    Name = model.Name,
                    OperationTypeId = model.OperationTypeId,
                    //Position = model.Position,
                    Price = model.Price,
                    PropertyStatusId = model.PropertyStatusId,
                    PropertyTypeId = model.PropertyTypeId,
                    Space = model.Space,
               //     propertyImages = await _db.PropertyImages.ToListAsync(),
                };

                _db.Properties.Update(property);
                _db.SaveChanges();


                return View("Index");
            }

            //Error 

            return View(model);
        }
  
    
    }



}
