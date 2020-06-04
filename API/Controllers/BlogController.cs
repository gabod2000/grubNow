using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class BlogController : BaseController
    {
        private BusinessBase<Blogs> _businessBase;
        public BlogController(BusinessWrapper businessWrapper) : base(businessWrapper)
        {
            _businessBase = new BusinessBase<Blogs>(businessWrapper._serviceProvider);
        }
        // GET: api/City
        [HttpGet]
        public BaseResponse Get()
        {
            return constructResponse(_businessWrapper.BlogBL.Get());
        }

       // GET: api/City/5
        [HttpGet("{id}")]
        public BaseResponse GetById(int id)
        {
            return constructResponse(_businessWrapper.BlogBL.GetById(id));
        }

        // POST: api/City
        [HttpPost]
        public async Task<BaseResponse> Post([FromBody] Blogs model)
        {
            return constructResponse(await _businessBase.Post(model));
        }

        // PUT: api/City/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> Put(int id, [FromBody] Blogs model)
        {
            return constructResponse(await _businessBase.Put(id, model));
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        public BaseResponse Delete(int id)
        {
            return constructResponse(_businessBase.Delete(id));
        }
    }
}
