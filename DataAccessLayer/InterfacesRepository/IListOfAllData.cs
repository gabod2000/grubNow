using Models;
using System.Collections.Generic;

namespace DataAccessLayer.InterfacesRepository
{
    public interface IListOfAllData
    {
        IEnumerable<Area> GetArea();
        Area GetAreaById(int id);
        Area GetAreaByName(string Area);
        IEnumerable<Cuisine> GetCuisine();
        Cuisine GetCuisineById(int id);
        IEnumerable<Category> GetCategory();
        Category GetCategoryById(int id);
        IEnumerable<Vendor> GetVendor();
        Vendor GetVendorById(int Id);
        Vendor GetVendorByUserId(string UserId);
        IEnumerable<Driver> GetDriver();
        Driver GetDriverByUserId(string UserId);
        List<VendorWithCuisine> GetsVendorWithCusineById(int id);
        List<VendorWithArea> GetsVendorWithAreaById(int id);
        IEnumerable<OtherLocation> GetOtherLocationByVendorID(int VendorID);
        List<DriverWithArea> GetsDriverWithAreaById(int id);
        bool CheckAlreadyExistArea(string AreaName);
        bool CheckAlreadyExistCusine(string CusineName);
        bool CheckAlreadyExistCategory(string Category);
        VendorWithArea GetVendorWithAreaById(int id);
        DriverWithArea GetDriverWithAreaById(int id);


    }
}
