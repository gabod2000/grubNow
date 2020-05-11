﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    public class SignUpVendorVM
    {
        public SignUpVendorVM()
        {
            Area = new List<SelectListItem>();
            Category = new List<SelectListItem>();
            Cuisine = new List<SelectListItem>();
            NunberOfLocation = new List<SelectListItem>();
        }
        public string Id { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 2)]
        public string FirstName { get; set; }


        public string ProfilePic { get; set; }
        [Display(Name = "Store Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 2)]
        public string StoreName { get; set; }

        [Display(Name = "Website  URL")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 2)]
        public string Website_Url { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(150, ErrorMessage = "{0} must be max. of {1} characters")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "{0} is required")]
        public int AreaId { get; set; }
        public List<SelectListItem> Area { get; set; }

        [Display(Name = "Category ")]
        [Required(ErrorMessage = "{0} is required")]
        public int CategoryId { get; set; }
        public List<SelectListItem> Category { get; set; }

        [Display(Name = "Category ")]
        [Required(ErrorMessage = "{0} is required")]
        public int CuisineId { get; set; }
        public List<SelectListItem> Cuisine { get; set; }


        [Display(Name = "Nunber Of Location ")]
        [Required(ErrorMessage = "{0} is required")]
        public string NunberOfLocationName { get; set; }
        public List<SelectListItem> NunberOfLocation { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "Minimum Password Length is 6", MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 3)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "Minimum Password Length is 6", MinimumLength = 3)]
        public string Address { get; set; }
        public IFormFile SubmitterPicture { get; set; }
        public string OtherArea { get; set; }
        public string OtherCatregory { get; set; }
        public string OtherCusine { get; set; }
        public int VendorId { get; set; }
    }
}
