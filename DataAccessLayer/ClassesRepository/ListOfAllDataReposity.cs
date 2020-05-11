using DataAccessLayer.InterfacesRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.ClassesRepository
{
    public class ListOfAllDataReposity : IListOfAllData
    {
        private readonly LearningDbContext _context;

        public ListOfAllDataReposity(LearningDbContext context)
        {
            _context = context;
        }

        //Get All Subject include and Exclude Courses
        public IEnumerable<Area> GetArea()
        {
            return _context.Areas;
        }

        //Get subject include and Exclude Course on the basis of ID 
        public Area GetAreaById(int id)
        {
            return _context.Areas
                           .FirstOrDefault(p => p.Id == id);
        }


        /// <summary>
        /// Get All Category 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetCategory()
        {
            return _context.Categories;
        }

        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetCategoryById(int id)
        {
            return _context.Categories
                           .FirstOrDefault(p => p.Id == id);
        }
        /// <summary>
        /// Check Area Already Exist IN database 
        /// </summary>
        /// <param name="AreaName"></param>
        /// <returns></returns>
        public bool CheckAlreadyExistArea(string AreaName)
        {
            return _context.Areas.Any(x => x.AreaName.ToLower() == AreaName.ToLower());
        }


        /// <summary>
        /// Check Cusine Already Exist In database 
        /// </summary>
        /// <param name="CusineName"></param>
        /// <returns></returns>
        public bool CheckAlreadyExistCusine(string CusineName)
        {
            return _context.Cuisines.Any(x => x.Name.ToLower() == CusineName.ToLower());
        }

        /// <summary>
        /// Check Cusine Already Exist In database 
        /// </summary>
        /// <param name="CusineName"></param>
        /// <returns></returns>
        public bool CheckAlreadyExistCategory(string CategoryName)
        {
            return _context.Categories.Any(x => x.Name.ToLower() == CategoryName.ToLower());
        }


        /// <summary>
        /// Get All Cusine Records 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cuisine> GetCuisine()
        {
            return _context.Cuisines;
        }


        /// <summary>
        /// Get Cusine By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Cuisine GetCuisineById(int id)
        {
            return _context.Cuisines
                            .FirstOrDefault(p => p.Id == id);
        }


        /// <summary>
        /// Get Vendor with Area By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VendorWithArea GetVendorWithAreaById(int id)
        {
            return _context.VendorWithAreas
                            .FirstOrDefault(p => p.Id == id);
        }


        /// <summary>
        /// Gets Vendor With Area By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<VendorWithArea> GetsVendorWithAreaById(int id)
        {
            return _context.VendorWithAreas
                            .Where(p => p.VendorId == id).ToList();
        }


        /// <summary>
        /// Get Vendor with Area By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<VendorWithCuisine> GetsVendorWithCusineById(int id)
        {
            return _context.VendorWithCuisines
                            .Where(p => p.VendorId == id).ToList();
        }


        /// <summary>
        /// Get Driver with Area By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<DriverWithArea> GetsDriverWithAreaById(int id)
        {
            return _context.DriverWithAreas
                            .Where(p => p.DriverId == id).ToList();
        }

       


        /// <summary>
        /// Get Vendor with Area By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DriverWithArea GetDriverWithAreaById(int id)
        {
            return _context.DriverWithAreas
                            .FirstOrDefault(p => p.Id == id);
        }



        /// <summary>
        /// Get All Vendor Records
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vendor> GetVendor()
        {
            return _context.Vendors;
        }

        /// <summary>
        /// Get Vender By UserId 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Vendor GetVendorByUserId(string UserId)
        {
            return _context.Vendors
                            .FirstOrDefault(p => p.UserId == UserId);
        }




        /// <summary>
        /// Get All Driver Records
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Driver> GetDriver()
        {
            return _context.Drivers;
        }


        /// <summary>
        /// Get Driver By Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Driver GetDriverByUserId(string UserId)
        {
            return _context.Drivers.Include(x => x.DriverWithAreas)
                            .FirstOrDefault(p => p.UserId == UserId);
        }

        /// <summary>
        /// Get Other Location By Vendor ID
        /// </summary>
        /// <param name="VendorID"></param>
        /// <returns></returns>

        public IEnumerable<OtherLocation> GetOtherLocationByVendorID(int VendorID)
        {
            return _context.OtherLocations.Where(x => x.VendorID == VendorID).ToList();
        }

       
    }


}
