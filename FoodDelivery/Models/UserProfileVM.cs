using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    public class UserProfileVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string StoreName { get; set; }

        public string ProfilePic { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }


        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 3)]
        public string NewPassword { get; set; }

        public string CarPic { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 3)]
        public string Password { get; set; }
    }
}
