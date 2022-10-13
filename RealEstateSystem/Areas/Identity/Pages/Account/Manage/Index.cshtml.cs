using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateSystem.Data;
using RealEstateSystem.Models;

namespace RealEstateSystem.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;


        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public string Username { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public IEnumerable<UserType> userTypes { get; set; }


        public class InputModel
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Display(Name = "Description")]
            public string Description { get; set; }
            [Display(Name = "Country")]
            public string Country { get; set; }
            [Display(Name = "City")]
            public string City { get; set; }
            [Display(Name = "Birthday")]
            public DateTime Birthday { get; set; }
            [Display(Name = "Gender")]
            public bool Gender { get; set; }

            //[Display(Name = "Active")]
            //public bool Active { get; set; }

            //[Display(Name = "RegisterDate ")]
            //public DateTime RegisterDate { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Image")]
            public byte[] Image { get; set; }

            public int usertype { get; set; }
            public int UserTypeId { get; set; }

           

          
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var CurrentUser = _userManager.Users.Include(x => x.UserType).Where(x => x.UserName == userName).FirstOrDefault();



            Username = userName;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                Country = user.Country,
                City = user.City,
                PhoneNumber = phoneNumber,
                Gender = user.Gender,
                Birthday = user.birthday,
                UserTypeId = CurrentUser.UserTypeId,
                Image=CurrentUser.Image,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            userTypes = await _db.UserTypes.ToListAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
          userTypes = await _db.UserTypes.ToListAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
             var firstname=user.FirstName;
            var lastname=user.LastName;
           var description=user.Description;
            var country=user.Country;
            var city=user.City;
            var gender=user.Gender;
            var birthday=user.birthday;
            var userTypeId=user.UserTypeId;
           

            if (Input.FirstName != firstname ||
               Input.LastName != lastname||
               Input.Description != description ||
               Input.Country != country ||
               Input.City != city ||
               Input.Gender != gender ||
               Input.Birthday !=birthday  )
            {
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.Description = Input.Description;
                user.Country = Input.Country;
                user.City = Input.City;
                user.Gender = Input.Gender;
                user.birthday=Input.Birthday;
              //  user.UserType = userTypes.Where(x => x.Id == Input.usertype).FirstOrDefault();
                await _userManager.UpdateAsync(user);

            }

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if(Request.Form.Files.Count>0)
            {
                var file = Request.Form.Files.FirstOrDefault();

                using (var dataStream =new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.Image=dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
