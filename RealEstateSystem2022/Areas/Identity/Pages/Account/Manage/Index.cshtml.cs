using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstateSystem2022.Data;
using RealEstateSystem2022.Data.RealEstateContext;
using RealEstateSystem2022.Helpers;
using RealEstateSystem2022.Models;

namespace RealEstateSystem2022.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
         RealEstateDbContext db;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RealEstateDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Image")]
            public byte[] Image { get; set; }

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

            [Display(Name = "Active")]
            public bool Active { get; set; }

            [Display(Name = "RegisterDate ")]
            public DateTime RegisterDate { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public UserType UserType { get; set; } 
        }

        private async Task LoadAsync(IdentityUser user)
        {
            
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
           
            Username = userName;

            var currentuser = db.User.FirstOrDefault(User => User.Id == GenericVariables.CurrentUser);
            Input = new InputModel
            {
                UserName = userName,
                PhoneNumber = phoneNumber,
                FirstName=currentuser.FirstName,
                LastName=currentuser.LastName,
                Gender = currentuser.Gender,
                Country = currentuser.Country,  
                City = currentuser.City,    
                UserType=currentuser.UserType,
            };

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
          

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
           //await LoadAsync(user1);
            return Page();
        }

        private Task LoadAsync(RealEstateDbContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser applicationUser = new ApplicationUser();
            var user = await _userManager.GetUserAsync(User);

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
            //var FirstName = applicationUser.FirstName;

            //if (Input.FirstName != FirstName)
            //{
            //    var setFirstNameResult = await _userManager.SetPhoneNumberAsync(user, Input.FirstName);
            //    if (!setFirstNameResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
