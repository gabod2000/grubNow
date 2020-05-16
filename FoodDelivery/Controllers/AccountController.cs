using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.InterfacesRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Newtonsoft.Json;

namespace FoodDelivery.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManger;
        private IEfRepository _efRepository;
        private IListOfAllData _listOfAll;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IEfRepository efRepository, IListOfAllData listOfAll, IHostingEnvironment environment)
        {
            _userManger = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _listOfAll = listOfAll;
            _efRepository = efRepository;
        }

        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HandleSignUp()
        {
            return View();
        }

        public IActionResult ExternalLogin() 
        {
            return View();
        }

        public IActionResult UnAuthorize()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            try
            {
                bool Status = false;
                string Message = string.Empty;
                if (ModelState.IsValid)
                {
                    // Checking Email Is Confirmed 
                    var user = _userManger.FindByNameAsync(model.UserName).Result;
                    if (user != null)
                    {
                        // Login 
                        var result = await _signInManager.PasswordSignInAsync(
                            userName: model.UserName,
                            password: model.Password,
                            isPersistent: model.RememberMe,
                            lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            Status = true;
                            Message = "Login Successfully";
                            return Json(new { status = Status, message = Message });
                        }
                        else
                        {
                            Status = false;
                            Message = "Username / Password is invalid. Try again!";
                            return Json(new { status = Status, message = Message });
                        }


                        //if (!_userManger.IsEmailConfirmedAsync(user).Result)
                        //{
                        //    ModelState.AddModelError("", "Email Not Confirmed!");
                        //    return View(model);
                        //}
                        //else
                        //{
                        //    // Login 
                        //    var result = await _signInManager.PasswordSignInAsync(
                        //        userName: model.UserName,
                        //        password: model.Password,
                        //        isPersistent: model.RememberMe,
                        //        lockoutOnFailure: false);

                        //    if (result.Succeeded)
                        //    {
                        //        return RedirectToAction("Index", new { Areas = "", Controller = "Home" });
                        //    }
                        //}
                    }
                    else
                    {
                        Status = false;
                        Message = "Username / Password is invalid. Try again!";
                        return Json(new { status = Status, message = Message });
                    }
                }
                else
                {
                    Status = false;
                    // Adding Model Error
                    Message = "Username / Password is invalid. Try again!";
                }
                return Json(new { status = Status, message = Message });

            }
            catch (Exception ex)
            {

                throw;
            }


        }
        #endregion

        #region Sign Up
        [HttpGet]
        public IActionResult SignUp(string UserType)
        {
            if (UserType == "Vendor")
            {
                SignUpVendorVM model = new SignUpVendorVM();
                model.Area = _listOfAll.GetArea()?.Select(p => new SelectListItem()
                {
                    Text = p.AreaName,
                    Value = p.Id.ToString()
                }).ToList();
                model.Category = _listOfAll.GetCategory()?.Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList();
                model.Cuisine = _listOfAll.GetCuisine()?.Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList();

                model.NunberOfLocation = new List<SelectListItem>()
                {
                    new SelectListItem() { Value = "1 - 4", Text = "1 - 4" },
                    new SelectListItem() { Value = "4 - 10", Text = "4 - 10" },
                    new SelectListItem() { Value = "10 - 20", Text = "10 - 20" }
                };


                return PartialView("_VendorSignUp", model);
            }
            else if (UserType == "User")
            {
                return PartialView("_User_SigUp");
            }
            else
            {
                SignUpDriverVM model = new SignUpDriverVM();
                model.Area = _listOfAll.GetArea()?.Select(p => new SelectListItem()
                {
                    Text = p.AreaName,
                    Value = p.Id.ToString()
                }).ToList();
                return PartialView("_Driver_SigUp", model);
            }

        }

        #region VendorSignUp
        [HttpPost]
        public async Task<IActionResult> SignUpVendor(SignUpVendorVM model, string AreaIDs,string CusineIds,IFormFile upload)
        {
            bool Status = false;
            string Message = string.Empty;
            try
            {

                //List Of AreaIds
                List<string> ListOfArea = JsonConvert.DeserializeObject<List<string>>(AreaIDs);

                //List Of CusineIds
                List<string> ListOfCusineIds = JsonConvert.DeserializeObject<List<string>>(CusineIds);

                if (ModelState.IsValid)
                {

                    #region Check User Exist 

                    // User Name Already Exsit
                    var userName = _userManger.FindByNameAsync(model.Email).Result;
                    if (userName != null)
                    {
                        Message = "UserName Already Exsit.Please Enter Another UserName";
                        Status = false;
                        return Json(new { status = Status, message = Message });
                    }

                    //User Email Already Exsit
                    var userEmail = _userManger.FindByEmailAsync(model.Email).Result;
                    if (userEmail != null)
                    {
                        Message = "This Email is Already Exsit.Please Enter Another Email";
                        Status = false;
                        return Json(new { status = Status, message = Message });
                    }


                    #endregion

                    // User
                    var user = new AppUser();
                    user.FirstName = model.FirstName;
                    user.Created = DateTime.Now;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.EmailConfirmed = true;
                    user.LockoutEnabled = false;

                    // Add User In Database 
                    var result = _userManger.CreateAsync(user, model.Password).Result;
                    if (result.Succeeded)
                    {

                        string UniqueFileName = "";
                        try
                        {
                            #region Create File Path 
                            
                            var datetime = DateTime.Now;
                            UniqueFileName = datetime.Month + datetime.Day + datetime.Hour + datetime.Minute + datetime.Ticks + "-" + upload.FileName;
                            var path = Directory.GetCurrentDirectory() + "/wwwroot/Uploads/";
                            var filemodel = System.IO.Path.Combine(path, UniqueFileName);
                            //Store file in Directory Folder 
                            using (var stream1 = new FileStream(filemodel, FileMode.Create)) 
                            {
                                upload.CopyToAsync(stream1).Wait();
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            return Json(new { status = Status, message = ex.Message });
                        }

                       





                        //Adding Vendor Information 
                        Vendor vendor = new Vendor();

                        #region Add other Category 
                        Category category = new Category();
                        if (model.OtherCatregory != null)
                        {
                            //Check If Other Area Already Exist 
                            if (!_listOfAll.CheckAlreadyExistCategory(model.OtherCatregory))
                            {
                                // Save Other Area
                                category.Name = model.OtherCatregory;
                                _efRepository.Add(category);
                                //Adding Cusine With vendor 
                                if (_efRepository.SaveChanges())
                                {
                                    vendor.CategoryId = category.Id;
                                }

                            }
                            else
                            {
                                Status = false;
                                Message = "Other Category Already Exist Please Use an Other One ";

                                //Delete User
                                var userdelete = _userManger.DeleteAsync(user).Result;
                                if (userdelete.Succeeded)
                                {
                                    return Json(new { status = Status, message = Message });
                                }
                            }
                        }
                        #endregion

                        if (model.OtherCatregory == null)
                        {
                            vendor.CategoryId = model.CategoryId;
                        }
                        vendor.NumberOfLocation = model.NunberOfLocationName;
                        vendor.StoreName = model.StoreName;
                        vendor.Website_Url = model.Website_Url;
                        vendor.UniqueFileName = UniqueFileName;
                        vendor.UserId = user.Id;
                        vendor.Address_Location = model.Address;
                        _efRepository.Add(vendor);
                        _efRepository.SaveChanges();

                        #region Adding Other Area 

                        Area area = null;
                        if (model.OtherArea != null)
                        {
                            area = new Area();
                            //Check If Other Area Already Exist 
                            if (!_listOfAll.CheckAlreadyExistArea(model.OtherArea))
                            {
                                // Save Other Area
                                area.AreaName = model.OtherArea;
                                _efRepository.Add(area);
                                VendorWithArea driverWithArea = null;
                                if (_efRepository.SaveChanges())
                                {
                                    driverWithArea = new VendorWithArea();
                                    driverWithArea.AreaId = area.Id;
                                    driverWithArea.VendorId = vendor.Id;
                                    _efRepository.Add(driverWithArea);
                                    var resul = _efRepository.SaveChanges();
                                }

                            }
                            else
                            {
                                Status = false;
                                Message = "Other Area Already Exist Please Use an Other One ";

                                //Delete User
                                var userDelete = _userManger.DeleteAsync(user).Result;
                                if (userDelete.Succeeded)
                                {
                                    //Delete Driver 
                                    _efRepository.Delete(vendor);

                                    _efRepository.SaveChanges();
                                }
                                return Json(new { status = Status, message = Message });
                            }
                        }

                        #endregion

                        #region Adding Other Cusine 
                        Cuisine cuisine = new Cuisine();
                        if (model.OtherCusine != null)
                        {

                            //Check If Other Area Already Exist 
                            if (!_listOfAll.CheckAlreadyExistCusine(model.OtherCusine))
                            {
                                // Save Other Area
                                cuisine.Name = model.OtherCusine;
                                _efRepository.Add(cuisine);
                                VendorWithCuisine vendorwithCusine = null;

                                //Adding Cusine With vendor 
                                if (_efRepository.SaveChanges())
                                {
                                    vendorwithCusine = new VendorWithCuisine();
                                    vendorwithCusine.CuisineId = cuisine.Id;
                                    vendorwithCusine.VendorId = vendor.Id;
                                    _efRepository.Add(vendorwithCusine);
                                    var resul = _efRepository.SaveChanges();
                                }

                            }
                            else
                            {
                                Status = false;
                                Message = "Other Cusine Already Exist Please Use an Other One ";

                                //Delete User
                                var userdelete = _userManger.DeleteAsync(user).Result;
                                if (userdelete.Succeeded)
                                {
                                    //Delete Area
                                    if (area != null)
                                    {
                                        _efRepository.Delete(area);
                                        //Delete Vendor With Area 
                                        var vendorarea = _listOfAll.GetVendorWithAreaById(area.Id);
                                        _efRepository.Delete(vendorarea);
                                    }

                                    //Delete Driver 
                                    _efRepository.Delete(vendor);

                                    _efRepository.SaveChanges();
                                }
                                return Json(new { status = Status, message = Message });
                            }
                        }
                        #endregion




                        #region Adding List Of Area and Cusine 
                        // Save Multipale Area
                        if (ListOfArea.Count() > 0)
                        {
                            foreach (var item in ListOfArea)
                            {
                                VendorWithArea driverWithArea = new VendorWithArea();
                                driverWithArea.AreaId = Convert.ToInt32(item);
                                driverWithArea.VendorId = vendor.Id;
                                _efRepository.Add(driverWithArea);
                                var resul = _efRepository.SaveChanges();
                            }
                        }

                        // Save Multipale Cusine 
                        if (ListOfCusineIds.Count() > 0)
                        {
                            foreach (var item in ListOfCusineIds)
                            {
                                VendorWithCuisine cusineWithArea = new VendorWithCuisine();
                                cusineWithArea.CuisineId = Convert.ToInt32(item);
                                cusineWithArea.VendorId = vendor.Id;
                                _efRepository.Add(cusineWithArea);
                                var resul = _efRepository.SaveChanges();
                            }
                        }

                        #endregion

                        //Add Role
                        AddRole("Vendor", user);

                        //Send Email Confirmation
                        //SendEmailConformationLink(user);

                        Status = true;
                        Message = "Successfully Created Your Account";

                        // Redirect To SignInPage
                        return Json(new { status = Status, message = Message });

                    }
                    else
                    {
                        Status = false;
                        Message = "Error While Creating Your Account";
                        return Json(new { status = Status, message = Message });
                    }
                }

                Message = "Provide all required and valid data to proceed";
                return Json(new { status = Status, message = Message });


            }
            catch (Exception ex)
            {
                return Json(new { status = Status, message = ex.StackTrace });
            }

        
        }
        #endregion
        public void SendEmailConformationLink(AppUser user)
        {

            // Send Email Conformation 
            //Token email
            string emailConfrmToken = _userManger
                                      .GenerateEmailConfirmationTokenAsync(user)
                                      .Result;

            // Confomation Link
            string conformtaionLink = Url.Action("ConfirmEmail"
                , "Account", new { UserId = user.Id, token = emailConfrmToken },
                protocol: HttpContext.Request.Scheme);

            // SmtpClient Create
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;

            NetworkCredential obj = new NetworkCredential("nadeem.s.2582@gmail.com", "Nadeem1@");
            client.UseDefaultCredentials = true;
            client.Credentials = obj;
            client.Send("Test@localHost", user.Email, "Confirm Your Email", conformtaionLink);
        }
        public void AddRole(string RoleName, AppUser user)
        {
            string Role = string.Empty;
            // Adding Role If Not Exist
            if (!_roleManager.RoleExistsAsync(RoleName).Result)
            {
                // Add Admin Role If Not Exist
                var adminRole = new AppRole();
                adminRole.Name = RoleName;
                adminRole.Created = DateTime.Now;
                adminRole.Discription = "Perform All Opration";
                // Add Role In Database 
                var addRole = _roleManager.CreateAsync(adminRole).Result;
                if (!addRole.Succeeded)
                {
                    Role = RoleName;
                }
                var role = _userManger.AddToRoleAsync(user, RoleName).Result;

            }
            else
            {
                // Add to Role While Creting User
                var role = _userManger.AddToRoleAsync(user, RoleName).Result;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpUser(SignUpUserVM model)
        {
            bool Status = false;
            string Message = string.Empty;

            if (ModelState.IsValid)
            {
                // User Name Already Exsit
                var userName = _userManger.FindByNameAsync(model.Email).Result;
                if (userName != null)
                {
                    Message = "UserName Already Exsit.Please Enter Another UserName";
                    Status = false;
                    return Json(new { status = Status, message = Message });
                }

                //User Email Already Exsit
                var userEmail = _userManger.FindByEmailAsync(model.Email).Result;
                if (userEmail != null)
                {
                    Message = "This Email is Already Exsit.Please Enter Another Email";
                    Status = false;
                    return Json(new { status = Status, message = Message });
                }

                // User
                var user = new AppUser();
                user.FirstName = model.FirstName;
                user.Created = DateTime.Now;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;

                // Add User In Database 
                var result = _userManger.CreateAsync(user, model.Password).Result;

                if (result.Succeeded)
                {
                    //Add Role
                    AddRole("User", user);

                    //Send Email Confirmation
                    //SendEmailConformationLink(user);

                    Status = true;
                    Message = "Successfully Created Your Account";

                    // Redirect To SignInPage
                    return Json(new { status = Status, message = Message });
                }
                else
                {
                    Status = false;
                    Message = "Error While Creating Your Account";
                    return Json(new { status = Status, message = Message });
                }
            }

            Message = "Provide all required and valid data to proceed";
            return Json(new { status = Status, message = Message });
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult SignUpDriver(SignUpDriverVM model, string AreaIDs)
        {
            bool Status = false;
            string Message = string.Empty;

            //List Of AreaIds
            List<string> ListOfArea = JsonConvert.DeserializeObject<List<string>>(AreaIDs);

            if (ModelState.IsValid)
            {
                // User Name Already Exsit
                var userName = _userManger.FindByNameAsync(model.Email).Result;
                if (userName != null)
                {
                    Message = "UserName Already Exsit.Please Enter Another UserName";
                    Status = false;
                    return Json(new { status = Status, message = Message });
                }

                //User Email Already Exsit
                var userEmail = _userManger.FindByEmailAsync(model.Email).Result;
                if (userEmail != null)
                {
                    Message = "This Email is Already Exsit.Please Enter Another Email";
                    Status = false;
                    return Json(new { status = Status, message = Message });
                }

                // User
                var user = new AppUser();
                user.FirstName = model.FirstName;
                user.Created = DateTime.Now;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;


                // Add User In Database 
                var result = _userManger.CreateAsync(user, model.Password).Result;

                if (result.Succeeded)
                {

                    // Save Driver Information
                    Driver driver = new Driver();
                    driver.Address_Location = model.Address;
                    driver.UserId = user.Id;
                    _efRepository.Add(driver);
                    _efRepository.SaveChanges();

                    Area area = new Area();
                    if (model.OtherArea != null)
                    {

                        //Check If Other Area Already Exist 
                        if (!_listOfAll.CheckAlreadyExistArea(model.OtherArea))
                        {
                            // Save Other Area
                            area.AreaName = model.OtherArea;
                            _efRepository.Add(area);
                            if (_efRepository.SaveChanges())
                            {
                                DriverWithArea driverWithArea = new DriverWithArea();
                                driverWithArea.AreaId = area.Id;
                                driverWithArea.DriverId = driver.Id;
                                _efRepository.Add(driverWithArea);
                                var resul = _efRepository.SaveChanges();
                            }

                        }
                        else
                        {
                            Status = false;
                            Message = "Other Area Already Exist Please Use an Other One ";

                            //Delete User
                            var date = _userManger.DeleteAsync(user).Result;
                            if (date.Succeeded)
                            {
                                //Delete Driver 
                                _efRepository.Delete(driver);
                                _efRepository.SaveChanges();
                            }
                            return Json(new { status = Status, message = Message });
                        }
                    }

                    // Save Multipale Area
                    if (ListOfArea.Count() > 0)
                    {
                        foreach (var item in ListOfArea)
                        {
                            DriverWithArea driverWithArea = new DriverWithArea();
                            driverWithArea.AreaId = Convert.ToInt32(item);
                            driverWithArea.DriverId = driver.Id;
                            _efRepository.Add(driverWithArea);
                            var resul = _efRepository.SaveChanges();
                        }
                    }



                    //Add Role
                    AddRole("Driver", user);

                    //Send Email Confirmation
                    //SendEmailConformationLink(user);

                    Status = true;
                    Message = "Successfully Created Your Account";

                    // Redirect To SignInPage
                    return Json(new { status = Status, message = Message });
                }
                else
                {
                    Status = false;
                    Message = "Error While Creating Your Account";
                    return Json(new { status = Status, message = Message });
                }
            }

            Message = "Provide all required and valid data to proceed";
            return Json(new { status = Status, message = Message });
        }

        public IActionResult CoonfirmEmailMessg()
        {
            ViewBag.message = "Email Sent Successfully Please Check Your Email";
            return View();
        }

        #endregion

        #region Sign Out
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            // TODO: Add Session Clearance code
            await _signInManager.SignOutAsync();

            return RedirectToAction("PublicSite", new { controller = "Home" });
        }
        #endregion

        #region ConfirmEmail
        public IActionResult ConfirmEmail(string userId, string token)
        {
            var user = _userManger.FindByIdAsync(userId).Result;
            var result = _userManger.ConfirmEmailAsync(user, token).Result;

            if (result.Succeeded)
            {
                ViewBag.Message = "Email Confirmed Successfully!";
                return View();
            }
            else
            {
                ViewBag.Message = "Error While Confirming your Email!";
                return View("Error");
            }
        }
        #endregion

        #region PasswordReset
        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;
            var user = _userManger.
                 FindByNameAsync(model.UserName).Result;

            var result = _userManger.ResetPasswordAsync
                      (user, model.Token, model.Password).Result;
            if (result.Succeeded)
            {
                Status = true;
                Message = "Password Reset Successfully";
                return Json(new { status = Status, message = Message });
            }
            else
            {
                Status = false;
                Message = "Error While Password Reset";
                return Json(new { status = Status, message = Message });
            }

        }


        public IActionResult View1Message()
        {
            ViewBag.message = "Password reset successful!";
            return View();
        }

        #endregion

        #region Password Reset Link
        [HttpGet]
        public IActionResult ResetPasswordLink()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPasswordLink(ResetlPasswordViewModel model)
        {
            bool Status = false;
            string Message = string.Empty;

            if (ModelState.IsValid)
            {
                var user = _userManger.FindByNameAsync(model.UserName).Result;
                if (user == null || !(_userManger.IsEmailConfirmedAsync(user).Result))
                {
                    ModelState.AddModelError("", "User Name And Email Are Not Correct");
                    return View(model);
                }

                // Genrate password Reset Token 
                var token = _userManger.
                      GeneratePasswordResetTokenAsync(user).Result;

                // Genrate Reset Link for Password 
                var resetLink = Url.Action("ResetPassword",
                                "Account", new { Token = token },
                                 protocol: HttpContext.Request.Scheme);


                // SmtpClient Create
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = @"C:\Test";
                client.Send("Test@localHost", user.Email, "Confirm Your Email", resetLink);
                Status = true;
                Message = "Please Check Your Email To Reset Your Password";
                return Json(new { status = Status, message = Message });
            }
            else
            {

                ModelState.AddModelError("", "Please Add All Required Fields");
                return View(model);
            }


        }


        public IActionResult ViewMessage()
        {
            ViewBag.message = "Email Sent Successfully Please Check Your Email";
            return View();
        }

        #endregion

    }
}