using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        // Update password 
        [HttpPost]
        [Route("ResetPasswordLink")]
        public async Task<BaseResponse> ResetPasswordLink([FromBody] ResetlPasswordViewModel model)
        {
            return constructResponse(await _businessWrapper.UserBL.ResetPasswordLink(model));
        }


        //[HttpPost]
        //[Route("EmailConfirmation")]
        //public async Task<BaseResponse> EmailConfirmation([FromBody] EmailConfirmationVM model)
        //{
        //    return constructResponse(_businessWrapper.UserBL.ConfirmEmail(model.UserIdentifire, model.Token));
        //}

        // Update password 
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<BaseResponse> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            return constructResponse(await _businessWrapper.UserBL.ResetPassword(model));
        }


        //[HttpGet]
        //public BaseResponse GetByStateAndCity(StateAndCityVMDTO  model)
        //{
        //    return constructResponse(_businessWrapper.UserBL.GetByStateAndCity(model.State,model.City));
        //}
    }
}
