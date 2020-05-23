using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using DataAccessLayer.InterfacesRepository;

namespace FoodDelivery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManger;
        private IListOfAllData _listOfAll;
        public HomeController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, ILogger<HomeController> logger, IListOfAllData listOfAll)
        {
            _userManger = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _listOfAll = listOfAll;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PublicSite()
        {
            return View();
        }

        // Search For TextBox For AutoComplete
        [HttpGet]
        public async Task<IActionResult> GetSearchValue()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var result = _listOfAll.GetArea()
                                  .Where(x => x.AreaName.Contains(term))
                                  .Select(x => x.AreaName).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        public IActionResult UserProfile()
        {

            var user = User.Identity.Name;
            UserProfileVM userProfileVM = new UserProfileVM();
            if (user != null)
            {
                var result = _userManger.FindByNameAsync(user).Result;
                if (result != null)
                {
                    userProfileVM.CarPic = result.DriverCar;
                    userProfileVM.Email = result.Email;
                    userProfileVM.FirstName = result.FirstName;
                    userProfileVM.LastName = result.LastName;
                    userProfileVM.PhoneNumber = result.PhoneNumber;
                    userProfileVM.ProfilePic = result.ProfilePic;
                }

            }

            return View(userProfileVM);
        }


        [HttpPost]
        public ActionResult UpdatePassword(UserProfileVM model)
        {
            bool Status = false;
            string Message = string.Empty;
            if (ModelState.IsValid)
            {
                var user = _userManger.FindByNameAsync(User.Identity.Name).Result;
                if (user != null)
                {
                    if (model.NewPassword != null)
                    {
                        var result = _userManger.ChangePasswordAsync(user, model.NewPassword, model.Password).Result;
                        if (result.Succeeded)
                        {
                            Status = true;
                            Message = "Change Password Successfully ";
                        }
                        else
                        {
                            Status = false;
                            Message = "Current password is Incorrect.";
                        }
                    }
                    else
                    {
                        Status = false;
                        Message = "Password Is Empty";
                    }
                }
            }
            else
            {
                Message = "Error :Please Provide All Required Fields. ";
                ModelState.AddModelError("", "Error ! Please Provide All Required Fields.");
            }
            return Json(new { status = Status, message = Message });

        }


        [HttpPost]
        public ActionResult UploadCarPic(IFormFile File)
        {
            string rootPath = Directory.GetCurrentDirectory() + "/wwwroot/UploadedFiles";
            string fileName = System.IO.Path.Combine(rootPath, File.FileName);
            //Store file in Directory Folder 
            using (var stream = new FileStream(fileName, FileMode.Create))
            File.CopyTo(stream);
            UpdateProfilesCar(File.FileName);
            return Json(new { response = true });
        }


        [HttpPost]
        public ActionResult Upload(IFormFile File)
        {
            string rootPath = Directory.GetCurrentDirectory() + "/wwwroot/UploadedFiles";
            string fileName = System.IO.Path.Combine(rootPath, File.FileName);
            //Store file in Directory Folder 
            using (var stream = new FileStream(fileName, FileMode.Create))
                File.CopyTo(stream);
            UpdateProfiles(File.FileName);
            return Json(new { response = true });
        }

        [HttpPost]
        public ActionResult UpdateProfilesCar(string FileName)
        {
            bool Status = false;
            string Message = string.Empty;
            var user = _userManger.FindByNameAsync(User.Identity.Name).Result;
            if (user!=null)
            {
                user.DriverCar = FileName;
                var result1 = _userManger.UpdateAsync(user).Result;
                if (result1.Succeeded)
                {
                    Status = true;
                    Message = "Record Update successfully";
                }
                else
                {
                    Message = "Error While Updating record";
                    ModelState.AddModelError("", "Provide all valid data to proceed");
                }
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public ActionResult UpdateProfiles(string FileName)
        {
            bool Status = false;
            string Message = string.Empty;
            var user = _userManger.FindByNameAsync(User.Identity.Name).Result;
            if (user != null)
            {
                user.ProfilePic = FileName;
                var result1 = _userManger.UpdateAsync(user).Result;
                if (result1.Succeeded)
                {
                    Status = true;
                    Message = "Record Update successfully";
                }
                else
                {
                    Message = "Error While Updating record";
                    ModelState.AddModelError("", "Provide all valid data to proceed");
                }
            }
            return RedirectToAction("Index", "Admin");
        }


        public IActionResult GoogleSearch()
        {
            return View();
        }

        public IActionResult GeoLocation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GoogleMap()
        {
            return View();
        }

        public IActionResult DeliveryNearByMe()
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
