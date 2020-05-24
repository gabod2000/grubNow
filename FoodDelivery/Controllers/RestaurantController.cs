using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.InterfacesRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodDelivery.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManger;
        private IListOfAllData _listOfAll;
        public RestaurantController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IListOfAllData listOfAll)
        {
            _userManger = userManager;
            _roleManager = roleManager;
            _listOfAll = listOfAll;
        }
        public IActionResult List()
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
      

        public IActionResult ListResturentByArea(string Area)
        {
            var data=  _listOfAll.GetAreaByName(Area);

            ViewBag.AreaName = Area;
            List<ListOfResturantVM> listOfs = new List<ListOfResturantVM>();
            if (data!=null)
            {
                foreach (var item in data.VendorWithAreas)
                {
                    var vendor = _listOfAll.GetVendorById(item.VendorId ?? 0);
                    if (vendor.Category.Name == "Restaurants")
                    {
                        ListOfResturantVM resturantVM = new ListOfResturantVM();
                        resturantVM.Category = vendor.Category.Name;
                        resturantVM.Id = vendor.Id;
                        resturantVM.StoreName = vendor.StoreName;
                        resturantVM.UniqueFileName = vendor.UniqueFileName;
                        resturantVM.Website_Url = vendor.Website_Url;
                        resturantVM.Address_Location = vendor.Address_Location;
                        listOfs.Add(resturantVM);
                    }
                }
            }
            
            return View(listOfs);
        }

        public IActionResult ListResturent()
        {
            List<ListOfResturantVM> listOfs = new List<ListOfResturantVM>();

            var vendor = _listOfAll.GetVendor();
            if (vendor != null)
            {
                foreach (var item in vendor)
                {
                    if (item.Category.Name == "Restaurants")
                    {
                        ListOfResturantVM resturantVM = new ListOfResturantVM();
                        resturantVM.Category = item.Category.Name;
                        resturantVM.Id = item.Id;
                        resturantVM.StoreName = item.StoreName;
                        resturantVM.UniqueFileName = item.UniqueFileName;
                        resturantVM.Website_Url = item.Website_Url;
                        resturantVM.Address_Location = item.Address_Location;
                        listOfs.Add(resturantVM);
                    }
                }
            }

            return View(listOfs);
        }



    }
}