using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CommonLayer.Helper.Utils;

namespace API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [AllowAnonymous]
    public class BaseController : ControllerBase
    {
        protected BusinessWrapper _businessWrapper;
        public BaseController(BusinessWrapper businessWrapper)
        {
            _businessWrapper = businessWrapper;
        }
        protected BaseResponse constructResponse(object response)
        {
            return new BaseResponse()
            {
                dynamicResult = response,
                isSuccessfull = OtherConstants.isSuccessful,
                statusCode = OtherConstants.isSuccessful == true ? 200 : 500,
                messageType = OtherConstants.messageType,
                message = OtherConstants.responseMsg,
               // errorMessage = OtherConstants.
            };
        }
    }
}
