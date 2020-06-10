using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;
using static CommonLayer.DTO;

namespace API.Controllers
{
    public class CuisineController : BaseController
    {
        private BusinessBase<Cuisine> _businessBase;
        public CuisineController(BusinessWrapper businessWrapper) : base(businessWrapper)
        {
            _businessBase = new BusinessBase<Cuisine>(businessWrapper._serviceProvider);
        }


        // GET: api/License
        [HttpGet]
        public BaseResponse Get()
        {
            return constructResponse(_businessWrapper.CusinieBL.Get());
        }

        // GET: api/License/5
        [HttpGet("{id}")]
        public BaseResponse GetById(int id)
        {
            return constructResponse(_businessWrapper.CusinieBL.GetById(id));
        }

        // POST: api/License
        [HttpPost]
        public async Task<BaseResponse> Post([FromBody] CuisineDTO model)
        {
            return constructResponse(await _businessWrapper.CusinieBL.Post(model));
        }

        // PUT: api/License/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> Put(int id, [FromBody] CuisineDTO model)
        {
            return constructResponse(await _businessWrapper.CusinieBL.Put(model));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<BaseResponse> Delete(int id)
        {
            return constructResponse(await _businessWrapper.CusinieBL.Delete(id));
        }
    }
}
