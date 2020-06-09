using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ResturantController : BaseController
    {
        public ResturantController(BusinessWrapper businessWrapper) : base(businessWrapper)
        {
        }

        [HttpGet]
        [Route("ListOfAllResturent")]
        public async Task<BaseResponse> ListOfAllResturent ()
        {
            return constructResponse(await _businessWrapper.UserBL.ListResturent());
        }


        [HttpGet]
        [Route("ListOfAllResturentByArea")]
        public async Task<BaseResponse> ListOfAllResturentByArea(string Area)
        {
            return constructResponse(await _businessWrapper.UserBL.ListResturentByArea(Area));
        }
    }
}
