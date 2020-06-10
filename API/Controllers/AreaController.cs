using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;
using static CommonLayer.DTO;

namespace API.Controllers
{
    public class AreaController : BaseController
    {
        private BusinessBase<Area> _businessBase;
        public AreaController(BusinessWrapper businessWrapper) : base(businessWrapper)
        {
            _businessBase = new BusinessBase<Area>(businessWrapper._serviceProvider);
        }
        // GET: api/City
        [HttpGet]
        public BaseResponse Get()
        {
            return constructResponse(_businessWrapper.AreaBL.Get());
        }

       // GET: api/City/5
        [HttpGet("{id}")]
        public BaseResponse GetById(int id)
        {
            return constructResponse(_businessWrapper.AreaBL.GetById(id));
        }

        // POST: api/City
        [HttpPost]
        public async Task<BaseResponse> Post([FromBody] AreaDTO model)
        {
            return constructResponse(await _businessWrapper.AreaBL.Post(model));
        }

        // PUT: api/City/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> Put(int id, [FromBody] AreaDTO model)
        {
            return constructResponse(await _businessWrapper.AreaBL.Put(model));
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        public async Task<BaseResponse> Delete(int id)
        {
            return constructResponse(await _businessWrapper.AreaBL.Delete(id));
        }
    }
}
