using CommonLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CommonLayer.DTO;

namespace BusinessLayer
{
    public class BlogBL : BusinessBase<Blogs>
    {
        private GrubNowDbContext _context;
        private BusinessBase<Blogs> _BusinessBase;
        private BaseResponse response;
        public BlogBL(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _BusinessBase = new BusinessBase<Blogs>(serviceProvider); 
            response = new BaseResponse();
            _context = serviceProvider.GetRequiredService<GrubNowDbContext>();
        }



        public async Task<IEnumerable<BlogsDTO>> Get()
        {
            var states = _BusinessBase.Get();
            List<BlogsDTO> stateslst = null;
            if (states != null)
            {
                stateslst = new List<BlogsDTO>();
                foreach (var item in states)
                {
                    BlogsDTO state = new BlogsDTO();
                    state.Id = item.Id;
                    state.Description = item.Description;
                    state.Heading = item.Heading;
                    state.ImagesUrl = item.ImagesUrl;
                    state.OtherImagesUrl = item.OtherImagesUrl;
                    state.UserName = item.UserName;
                    state.UserId = item.UserId;
                    stateslst.Add(state);
                }
            }
            return stateslst;
        }

        public async Task<BlogsDTO> GetById(int Id)
        {
            var states = _BusinessBase.GetById(Id);
            BlogsDTO state = new BlogsDTO();
            if (states != null)
            {
                state.Id = states.Id;
                state.Description = states.Description;
                state.Heading = states.Heading;
                state.ImagesUrl = states.ImagesUrl;
                state.OtherImagesUrl = states.OtherImagesUrl;
                state.UserName = states.UserName;
                state.UserId = states.UserId;
            }
            return state;
        }

    }
}
