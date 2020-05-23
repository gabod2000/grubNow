using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.ErrorLog;
using FoodDelivery.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Models;

namespace FoodDelivery.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        //private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<AppUser> userManager
            //IEmailSender emailSender
            )
        {
            _userManager = userManager;
          //  _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(
                //    Input.Email,
                //    "Reset Password",
                //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");


                try
                {
                    EmailSender sndEmail = new EmailSender("smtp.gmail.com", 587, true, "nadeem.sa.2582@gmail.com", "03461578803");

                    string message = "Hello User.<br/> Welcome to FoodDelivery. " + "<br/>" + " You can " + "<a href=" + callbackUrl + ">Click Here</a>" + " to change your password. <br/> Thanks.";

                    await sndEmail.SendEmailAsync(user.Email, "Email Confirmation", message);

                    return RedirectToPage("./ForgotPasswordConfirmation");

                }
                catch (Exception ex)
                {
                    WriteLog.AddLog(ex.Message);
                    WriteLog.AddLog(ex.StackTrace);
                    WriteLog.AddLog(ex.InnerException.ToString());
                }
            }

            return Page();
        }
    }
}
