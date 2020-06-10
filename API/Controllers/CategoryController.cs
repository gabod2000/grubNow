using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CategoryController : BaseController
    {
        private BusinessBase<Category> _businessBase;
        public CategoryController(BusinessWrapper businessWrapper) : base(businessWrapper)
        {
            _businessBase = new BusinessBase<Category>(businessWrapper._serviceProvider);
        }
        // GET: api/City
        [HttpGet]
        public BaseResponse Get()
        {
            return constructResponse(_businessWrapper.CategoryBL.Get());
        }

        // GET: api/City/5
        [HttpGet("{id}")]
        public BaseResponse GetById(int id)
        {
            return constructResponse(_businessWrapper.CategoryBL.GetById(id));
        }

        // POST: api/City
        [HttpPost]
        public async Task<BaseResponse> Post([FromBody] Category model)
        {
            return constructResponse(await _businessBase.Post(model));
        }

        // PUT: api/City/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> Put(int id, [FromBody] Category model)
        {
            return constructResponse(await _businessBase.Put(id, model));
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        public async Task<BaseResponse> Delete(int id)
        {
            return constructResponse(await _businessWrapper.CategoryBL.Delete(id));
        }
    }
}
