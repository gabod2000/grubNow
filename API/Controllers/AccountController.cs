using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static CommonLayer.DTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : BaseController
    {

        public AccountController(BusinessWrapper businessWrapper) : base(businessWrapper)
        {
        }

        [HttpPost]
        [Route("SignUpVendor")]
        [AllowAnonymous]
        public async Task<BaseResponse> SignUpVendor([FromBody] SignUpVendorVM model)
        {
            return constructResponse(await _businessWrapper.UserBL.SignUpVendor(model,model.AreaIds,model.CuisineIds,model.SubmitterPicture));
        }

        [HttpPost]
        [Route("SignUpDriver")]
        [AllowAnonymous]
        public async Task<BaseResponse> SignUpDriver([FromBody] SignUpDriverVM model)
        {
            return constructResponse(await _businessWrapper.UserBL.SignUpDriver(model,model.AreaIds));
        }


        [HttpPost]
        [Route("SignUpUser")]
        [AllowAnonymous]
        public async Task<BaseResponse> SignUpUser([FromBody] SignUpUserVM model)
        {
            return constructResponse(await _businessWrapper.UserBL.SignUpUser(model));
        }


        [HttpPost]
        [Route("DeleteVendor")]
        public async Task<BaseResponse> DeleteVendor([FromBody] string Id)
        {
            return constructResponse(await _businessWrapper.UserBL.DeleteVendor(Id));
        }

        [HttpPost]
        [Route("DeleteDriver")]
        public async Task<BaseResponse> DeleteDriver([FromBody] string  Id)
        {
            return constructResponse(await _businessWrapper.UserBL.DeleteDriver(Id));
        }

        [HttpPost]
        [Route("DeleteUser")]
        public async Task<BaseResponse> DeleteUser([FromBody] string Id)
        {
            return constructResponse(await _businessWrapper.UserBL.DeleteUser(Id));
        }

        [HttpGet]
        [Route("GetAllVendor")]
        public async Task<BaseResponse> GetAllVendor()
        {
            return constructResponse(await _businessWrapper.UserBL.AllVendor());
        }

        [HttpGet]
        [Route("GetAllDriver")]
        public async Task<BaseResponse> GetAllDriver()
        {
            return constructResponse(await _businessWrapper.UserBL.AllDriver());
        }
        [HttpGet]
        [Route("GetAllUser")]
        public async Task<BaseResponse> GetAllUser()
        {
            return constructResponse(await _businessWrapper.UserBL.AllUser());
        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<BaseResponse> Login([FromBody] LoginViewModel model)
        {
            return constructResponse(await _businessWrapper.UserBL.ProcessLogin(model));
        }
      
        // Update password 
        [HttpPost]
        [Route("UpdatePassword")]
        public async Task<BaseResponse> UpdatePassword([FromBody] UpdatePaswordVM model)
        {
            return constructResponse(await _businessWrapper.UserBL.UpdatePassword(model));
        }

        [HttpPost]
        [Route("EmailConfirmation")]
        public async Task<BaseResponse> EmailConfirmation([FromBody] EmailConfirmationVM model)
        {
            return constructResponse(_businessWrapper.UserBL.ConfirmEmail(model.UserIdentifire, model.Token));
        }
        /// <summary>
        /// First Call Reset Password Link Using This methods Pass User Name and Generate Token for reset Password
        /// </summary>
        /// <param name="model">User Name</param>
        /// <returns></returns>
        // Update password 
        [HttpPost]
        [Route("ResetPasswordLink")]
        public async Task<BaseResponse> ResetPasswordLink([FromBody] ResetlPasswordViewModel model)
        {
            return constructResponse(await _businessWrapper.UserBL.ResetPasswordLink(model));
        }
       

        // Update password 
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<BaseResponse> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            return constructResponse(await _businessWrapper.UserBL.ResetPassword(model));
        }



        // Add Vendor location
        [HttpPost]
        [Route("AddVendorlocation")]
        public async Task<BaseResponse> AddVendorlocation([FromBody] OtherLocationVM  model)
        {
            return constructResponse(_businessWrapper.UserBL.Addlocation(model));
        }

        // Add Vendor location
        [HttpGet]
        [Route("ListOfVendorLocation")]
        public async Task<BaseResponse> ListOfVendorLocation(int VendorId)
        {
            return constructResponse(_businessWrapper.UserBL.ListOfLocation(VendorId));
        }


        // Add Vendor location
        [HttpGet]
        [Route("EditVendor")]
        public async Task<BaseResponse> EditVendor(string UserId)
        {
            return constructResponse(_businessWrapper.UserBL.EditVendor(UserId));
        }

        // Add Vendor location
        [HttpPost]
        [Route("EditVendor")]
        public async Task<BaseResponse> EditVendor(EditVendorVM model)
        {
            return constructResponse(_businessWrapper.UserBL.EditVendor(model, model.SubmitterPicture));
        }


        // Add Vendor location
        [HttpGet]
        [Route("EditDriver")]
        public async Task<BaseResponse> EditDriver(string UserId)
        {
            return constructResponse(_businessWrapper.UserBL.EditDriver(UserId));
        }

        // Add Vendor location
        [HttpPost]
        [Route("EditDriver")]
        public async Task<BaseResponse> EditDriver(EditDriverVM model)
        {
            return constructResponse(_businessWrapper.UserBL.EditDriver(model));
        }


        // Add Vendor location
        [HttpGet]
        [Route("EditUser")]
        public async Task<BaseResponse> EditUser(string UserId)
        {
            return constructResponse(_businessWrapper.UserBL.EditUser(UserId));
        }

        // Add Vendor location
        [HttpPost]
        [Route("EditUser")]
        public async Task<BaseResponse> EditUser(EditUserVM model)
        {
            return constructResponse(_businessWrapper.UserBL.EditUser(model));
        }

    }
}
