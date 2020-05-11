using Models;
using System.Collections.Generic;

namespace DataAccessLayer.InterfacesRepository
{
    public interface IListOfAllData
    {
        IEnumerable<Area> GetArea();
        Area GetAreaById(int id);
        IEnumerable<Cuisine> GetCuisine();
        Cuisine GetCuisineById(int id);
        IEnumerable<Category> GetCategory();
        Category GetCategoryById(int id);
        public IEnumerable<Vendor> GetVendor();
        public Vendor GetVendorByUserId(string UserId);
        public IEnumerable<Driver> GetDriver();
        public Driver GetDriverByUserId(string UserId);
        public List<VendorWithCuisine> GetsVendorWithCusineById(int id);
        public List<VendorWithArea> GetsVendorWithAreaById(int id);
        public IEnumerable<OtherLocation> GetOtherLocationByVendorID(int VendorID);
        public List<DriverWithArea> GetsDriverWithAreaById(int id);
        public bool CheckAlreadyExistArea(string AreaName);
        public bool CheckAlreadyExistCusine(string CusineName);
        public bool CheckAlreadyExistCategory(string Category);
        public VendorWithArea GetVendorWithAreaById(int id);
        public DriverWithArea GetDriverWithAreaById(int id);


    }
}
