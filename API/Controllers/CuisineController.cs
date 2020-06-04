using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

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
            return constructResponse(_businessBase.Get());
        }

        // GET: api/License/5
        [HttpGet("{id}")]
        public BaseResponse GetById(int id)
        {
            return constructResponse(_businessBase.GetById(id));
        }

        // POST: api/License
        [HttpPost]
        public async Task<BaseResponse> Post([FromBody] Cuisine model)
        {
            return constructResponse(await _businessBase.Post(model));
        }

        // PUT: api/License/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> Put(int id, [FromBody] Cuisine model)
        {
            return constructResponse(await _businessBase.Put(id, model));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public BaseResponse Delete(int id)
        {
            return constructResponse(_businessBase.Delete(id));
        }
    }
}
