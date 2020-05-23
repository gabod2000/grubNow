using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.InterfacesRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace FoodDelivery.Controllers
{
    [Authorize]
    public class ManageAllUserController : Controller
    {
        private readonly UserManager<AppUser> _userManger;
        private IListOfAllData _listOfAll;
        private IEfRepository _efRepository;
        public ManageAllUserController(UserManager<AppUser> userManager,IListOfAllData listOfAll,IEfRepository efRepository)
        {
            _userManger = userManager;
            _listOfAll = listOfAll;
            _efRepository = efRepository;
        }
        public IActionResult Index()
        {

            var data = _userManger.Users.ToList();
            List<SignUpVendorVM> model = new List<SignUpVendorVM>();
            if (data != null)
            {
            
                foreach (var item in data)
                {
                    if (_userManger.IsInRoleAsync(item, "Vendor").Result)
                    {

                        var VendorData = _listOfAll.GetVendorByUserId(item.Id);
                        if (VendorData!=null)
                        {
                            SignUpVendorVM listVM = new SignUpVendorVM();
                            listVM.Id = item.Id;
                            listVM.FirstName = item.FirstName;
                            listVM.LastName = item.LastName;
                            listVM.Email = item.Email;
                            listVM.PhoneNumber = item.PhoneNumber;
                            listVM.StoreName = VendorData.StoreName;
                            listVM.Address = VendorData.Address_Location;
                            listVM.VendorId = VendorData.Id;
                            model.Add(listVM);
                        }
                        
                    }
                }
            }

            return View(model);
        }


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


        [HttpGet]
        public IActionResult EditVendor(string id)
        {
            EditVendorVM vendorVM = null;
            var user= _userManger.FindByIdAsync(id).Result;
            if (user!=null)
            {
                
                vendorVM= new EditVendorVM();
                vendorVM.Id = user.Id;
                vendorVM.FirstName = user.FirstName;
                vendorVM.LastName = user.LastName;
                vendorVM.PhoneNumber = user.PhoneNumber;
                vendorVM.Email = user.Email;

                // Geting Vendor Data
                var vendor = _listOfAll.GetVendorByUserId(id);
                if (vendor!=null)
                {
                    vendorVM.CategoryId = vendor.Category.Id;
                    vendorVM.StoreName = vendor.StoreName;
                    vendorVM.Website_Url = vendor.Website_Url;
                    vendorVM.Address = vendor.Address_Location;
                    vendorVM.NunberOfLocationName = vendor.NumberOfLocation;
                }

                //vendorVM.Area = _listOfAll.GetArea()?.Select(p => new SelectListItem()
                //{
                //    Text = p.AreaName,
                //    Value = p.Id.ToString()
                //}).ToList();
                vendorVM.Category = _listOfAll.GetCategory()?.Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList();
                //vendorVM.Cuisine = _listOfAll.GetCuisine()?.Select(p => new SelectListItem()
                //{
                //    Text = p.Name,
                //    Value = p.Id.ToString()
                //}).ToList();

                vendorVM.NunberOfLocation = new List<SelectListItem>()
                {
                    new SelectListItem() { Value = "1 - 4", Text = "1 - 4" },
                    new SelectListItem() { Value = "4 - 10", Text = "4 - 10" },
                    new SelectListItem() { Value = "10 - 20", Text = "10 - 20" }
                };
            }
            return View(vendorVM);
        }


        [HttpPost]
        public IActionResult EditVendor(EditVendorVM model,IFormFile upload)
        {
            string Message = string.Empty;
            bool Status = false;

            if (ModelState.IsValid)
            {
                var userVernder = _userManger.FindByIdAsync(model.Id).Result;
                if (userVernder != null)
                {
                    userVernder.Id = model.Id;
                    userVernder.FirstName = model.FirstName;
                    userVernder.LastName = model.LastName;
                    userVernder.PhoneNumber = model.PhoneNumber;
                    userVernder.Email = model.Email;
                    var updateuser=_userManger.UpdateAsync(userVernder).Result;
                    if (updateuser.Succeeded)
                    {

                        // Geting Vendor Data
                        var vendor = _listOfAll.GetVendorByUserId(model.Id);
                        if (vendor != null)
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
                            vendor.CategoryId = model.CategoryId;
                            vendor.StoreName = model.StoreName;
                            vendor.Website_Url = model.Website_Url;
                            vendor.UniqueFileName = UniqueFileName;
                            vendor.Address_Location = model.Address;
                            _efRepository.Update(vendor);
                            if (_efRepository.SaveChanges())
                            {
                                Message = " Successfully Update Record";
                                Status = true;
                            }
                            else
                            {
                                Message = " Error While Updating Record";
                                Status = true;
                            }
                        }
                    }

                }
            }
            else
            {
                //model.Area = _listOfAll.GetArea()?.Select(p => new SelectListItem()
                //{
                //    Text = p.AreaName,
                //    Value = p.Id.ToString()
                //}).ToList();
                model.Category = _listOfAll.GetCategory()?.Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList();
                //model.Cuisine = _listOfAll.GetCuisine()?.Select(p => new SelectListItem()
                //{
                //    Text = p.Name,
                //    Value = p.Id.ToString()
                //}).ToList();

                model.NunberOfLocation = new List<SelectListItem>()
                {
                    new SelectListItem() { Value = "1 - 4", Text = "1 - 4" },
                    new SelectListItem() { Value = "4 - 10", Text = "4 - 10" },
                    new SelectListItem() { Value = "10 - 20", Text = "10 - 20" }
                };
            }
            return Json(new { status = Status, message = Message });
        }



        [HttpGet]
        public IActionResult EditDriver(string id)
        {
            EditDriverVM model = null;
            var user = _userManger.FindByIdAsync(id).Result;
            if (user != null)
            {
                model = new EditDriverVM();
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
                model.Id = user.Id;

                var driver = _listOfAll.GetDriverByUserId(id);
                if (driver!=null)
                {
                    model.Address = driver.Address_Location;
                }
                model.Area = _listOfAll.GetArea()?.Select(p => new SelectListItem()
                {
                    Text = p.AreaName,
                    Value = p.Id.ToString()
                }).ToList();
            }

            return View(model);
        }


        [HttpPost]
        public IActionResult EditDriver(EditDriverVM model)
        {
            string Message = string.Empty;
            bool Status = false;

            if (ModelState.IsValid)
            {
                var userDriver = _userManger.FindByIdAsync(model.Id).Result;
                if (userDriver != null)
                {
                    userDriver.Id = model.Id;
                    userDriver.FirstName = model.FirstName;
                    userDriver.LastName = model.LastName;
                    userDriver.PhoneNumber = model.PhoneNumber;
                    userDriver.Email = model.Email;
                    var user=  _userManger.UpdateAsync(userDriver).Result;
                    if (user.Succeeded)
                    {
                        var driver = _listOfAll.GetDriverByUserId(model.Id);
                        if (driver != null)
                        {
                            driver.Address_Location = model.Address;
                            _efRepository.Update(driver);
                            if (_efRepository.SaveChanges())
                            {
                                Message = " Successfully Update Record";
                                Status = true;
                            }
                            else
                            {
                                Message = " Error While Updating Record";
                                Status = true;
                            }
                        }
                    }
                   
                }
                else
                {

                    model.Area = _listOfAll.GetArea()?.Select(p => new SelectListItem()
                    {
                        Text = p.AreaName,
                        Value = p.Id.ToString()
                    }).ToList();
                }

            }
            else
            {

                model.Area = _listOfAll.GetArea()?.Select(p => new SelectListItem()
                {
                    Text = p.AreaName,
                    Value = p.Id.ToString()
                }).ToList();
            }
            return Json(new { status = Status, message = Message });
        }

        [HttpGet]
        public IActionResult EditUser(string id)
        {
            EditUserVM model = null;
            var user = _userManger.FindByIdAsync(id).Result;
            if (user != null)
            {
                model = new EditUserVM();
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
                model.Id = user.Id;
            } 
            return View(model);
        }


        [HttpPost]
        public IActionResult EditUser(EditUserVM model)
        {

            string Message = string.Empty;
            bool Status = false;

            if (ModelState.IsValid)
            {
                var userDriver = _userManger.FindByIdAsync(model.Id).Result;
                if (userDriver != null)
                {
                    userDriver.Id = model.Id;
                    userDriver.FirstName = model.FirstName;
                    userDriver.LastName = model.LastName;
                    userDriver.PhoneNumber = model.PhoneNumber;
                    userDriver.Email = model.Email;
                    var user=  _userManger.UpdateAsync(userDriver).Result;
                    if (user.Succeeded)
                    {
                        Message = " Successfully Update Record";
                        Status = true;
                    }
                    else
                    {
                        Message = " Error While Updating Record";
                        Status = true;
                    }

                }
            }
            return Json(new { status = Status, message = Message });
        }

        [AllowAnonymous]
        public IActionResult ListOfVendor()
        {

            var data = _userManger.Users.ToList();
            List<SignUpVendorVM> model = new List<SignUpVendorVM>();
            if (data != null)
            {

                foreach (var item in data)
                {
                    if (_userManger.IsInRoleAsync(item, "Vendor").Result)
                    {

                        var VendorData = _listOfAll.GetVendorByUserId(item.Id);
                        SignUpVendorVM listVM = new SignUpVendorVM();
                        listVM.Id = item.Id;
                        listVM.FirstName = item.FirstName;
                        listVM.LastName = item.LastName;
                        listVM.Email = item.Email;
                        listVM.PhoneNumber = item.PhoneNumber;
                        listVM.StoreName = VendorData.StoreName;
                        listVM.Address = VendorData.Address_Location;
                        model.Add(listVM);
                    }
                }
            }

            return View(model);
        }


        public IActionResult IndexDriver()
        {

            var data = _userManger.Users.ToList();
            List<SignUpDriverVM> model = new List<SignUpDriverVM>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (_userManger.IsInRoleAsync(item, "Driver").Result)
                    {
                        var driverData = _listOfAll.GetDriverByUserId(item.Id);
                        if (driverData!=null)
                        {
                            SignUpDriverVM listVM = new SignUpDriverVM();
                            listVM.Id = item.Id;
                            listVM.FirstName = item.FirstName;
                            listVM.LastName = item.LastName;
                            listVM.Email = item.Email;
                            listVM.PhoneNumber = item.PhoneNumber;
                            listVM.Address = driverData.Address_Location;
                            model.Add(listVM);
                        }
                       
                    }
                 
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ListOfDriverShowCustomer()
        {

            var data = _userManger.Users.ToList();
            List<SignUpDriverVM> model = new List<SignUpDriverVM>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (_userManger.IsInRoleAsync(item, "Driver").Result)
                    {
                        var driverData = _listOfAll.GetDriverByUserId(item.Id);
                        SignUpDriverVM listVM = new SignUpDriverVM();
                        listVM.Id = item.Id;
                        listVM.FirstName = item.FirstName;
                        listVM.LastName = item.LastName;
                        listVM.Email = item.Email;
                        listVM.PhoneNumber = item.PhoneNumber;
                        listVM.Address = driverData.Address_Location;
                        listVM.CarPic = item.DriverCar;
                        listVM.ProfilePic = item.ProfilePic;
                        model.Add(listVM);
                    }

                }
            }
            return View(model);
        }

        public IActionResult IndexUser()
        {

            var data = _userManger.Users.ToList();
            List<SignUpUserVM> model = new List<SignUpUserVM>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (_userManger.IsInRoleAsync(item, "User").Result)
                    {
                        SignUpUserVM listVM = new SignUpUserVM();
                        listVM.Id = item.Id;
                        listVM.FirstName = item.FirstName;
                        listVM.LastName = item.LastName;
                        listVM.Email = item.Email;
                        listVM.PhoneNumber = item.PhoneNumber;
                        model.Add(listVM);
                    }
                  
                }
            }

            return View(model);
        }



        // POST: Vendor/Delete/5
        [HttpPost]
        public IActionResult DeleteVendor(string id)
        {
            bool Status = false;
            string Message = string.Empty;


            // Finde vendor 
            var Vendor = _listOfAll.GetVendorByUserId(id);

            //Vendor With Area
            var VendorWithArea = _listOfAll.GetsVendorWithAreaById(Vendor.Id);
            foreach (var item in VendorWithArea)
            {
                //Delete Driver 
                _efRepository.Delete(item);
            }

            //Vendor With Cusine
            var VendorWithCusine = _listOfAll.GetsVendorWithCusineById(Vendor.Id);
            foreach (var item in VendorWithCusine)
            {
                //Delete Driver 
                _efRepository.Delete(item);
            }

            //Check Vendor Other Location
            var VendorOtherLocation = _listOfAll.GetOtherLocationByVendorID(Vendor.Id);
            foreach (var item in VendorOtherLocation)
            {
                //Delete Other Location 
                _efRepository.Delete(item);
            }


            //Delete Driver 
            _efRepository.Delete(Vendor);

            _efRepository.SaveChanges();

            var user = _userManger.FindByIdAsync(id).Result;
            var result =  _userManger.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
                Status = true;
                Message = "Record Successfully Deleted.";

            }
            else
            {
                Status = false;
                Message = "Error While Deleted Record.";
            }
            return Json(new { status = Status, message = Message });
        }


        // POST: Vendor/Delete/5
        [HttpPost]
        public IActionResult DeleteDriver(string id)
        {
            bool Status = false;
            string Message = string.Empty;


            // Finde vendor 
            var Driver = _listOfAll.GetDriverByUserId(id);

            //Vendor With Area
            var DriverWithArea = _listOfAll.GetsDriverWithAreaById(Driver.Id);
            foreach (var item in DriverWithArea)
            {
                //Delete Driver 
                _efRepository.Delete(item);
            }

            //Delete Driver 
            _efRepository.Delete(Driver);

            _efRepository.SaveChanges();

            var user = _userManger.FindByIdAsync(id).Result;
            var result = _userManger.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
                Status = true;
                Message = "Record Successfully Deleted.";

            }
            else
            {
                Status = false;
                Message = "Error While Deleted Record.";
            }
            return Json(new { status = Status, message = Message });
        }


        // POST: User/Delete/5
        [HttpPost]
        public IActionResult DeleteUser(string id)
        {
            bool Status = false;
            string Message = string.Empty;
            var user = _userManger.FindByIdAsync(id).Result;
            var result = _userManger.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
                Status = true;
                Message = "Record Successfully Deleted.";

            }
            else
            {
                Status = false;
                Message = "Error While Deleted Record.";
            }
            return Json(new { status = Status, message = Message });
        }

        [HttpGet]
        public IActionResult Addlocation(int VendorId)
        {
            var otherLocaction = new OtherLocationVM();
            var getOtherLocationByVendorID = _listOfAll.GetOtherLocationByVendorID(VendorId).ToList();
            if (getOtherLocationByVendorID!=null)
            {
                otherLocaction.VendorID = VendorId;
            }
            return View(otherLocaction);
        }

        [HttpPost]
        public IActionResult Addlocation(OtherLocationVM Model)
        {
            string Message = string.Empty;
            bool Status = false;
            if (ModelState.IsValid)
            {
               
                OtherLocation obj = new OtherLocation();
                obj.VendorID = Model.VendorID;
                obj.LocationAddress = Model.LocationAddress;
                obj.LocationName = Model.LocationName;
                _efRepository.Add(obj);
                var result = _efRepository.SaveChanges();
                if (result)
                {
                    Status = true;
                    Message = "Record Successfully Created.";
                }
                else
                {
                    Status = false;
                    Message = "Error While Creating Record.";
                }
            }
            return Json(new { status = Status, message = Message });
        }


        [HttpGet]
        public IActionResult ListOfLocation(int VendorId)
        {
            List<OtherLocationList> otherLocaction = new List<OtherLocationList>();
            var getOtherLocationByVendorID = _listOfAll.GetOtherLocationByVendorID(VendorId).ToList();
            foreach (var item in getOtherLocationByVendorID)
            {
                otherLocaction.Add(new OtherLocationList()
                {
                    LocationAddress = item.LocationAddress,
                    LocationName = item.LocationName
                });

            }
            return View(otherLocaction);
        }

        

    }
}