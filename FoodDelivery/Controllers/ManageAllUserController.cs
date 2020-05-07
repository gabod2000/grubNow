using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.InterfacesRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace FoodDelivery.Controllers
{
   // [Authorize]
    public class ManageAllUserController : Controller
    {
        private readonly UserManager<AppUser> _userManger;
        private IListOfAllData _listOfAll;
        public ManageAllUserController(UserManager<AppUser> userManager,IListOfAllData listOfAll)
        {
            _userManger = userManager;
            _listOfAll = listOfAll;
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

                        var VendorData = _listOfAll.GetVendorById(item.Id);
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

                        var VendorData = _listOfAll.GetVendorById(item.Id);
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
                        var driverData = _listOfAll.GetDriverById(item.Id);
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
            return View(model);
        }


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
                        var driverData = _listOfAll.GetDriverById(item.Id);
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

    }
}