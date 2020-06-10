using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CommonLayer.DTO;

namespace BusinessLayer
{
    public class CusinieBL : BusinessBase<Category>
    {
        private GrubNowDbContext _context;
        private BusinessBase<Cuisine> _BusinessBase;
        public CusinieBL(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _BusinessBase = new BusinessBase<Cuisine>(serviceProvider); 
            _context = serviceProvider.GetRequiredService<GrubNowDbContext>();
        }

        public async Task<IEnumerable<CuisineDTO>> Get()
        {
            var programs = _BusinessBase.Get().ToList();
            List<CuisineDTO> categorylst = null;
            if (programs != null)
            {
                categorylst = new List<CuisineDTO>();
                foreach (var item in programs)
                {
                    CuisineDTO category = new CuisineDTO();
                    category.Id = item.Id;
                    category.Name = item.Name;
                    categorylst.Add(category);
                }
            }
            return categorylst;
        }

        public async Task<CuisineDTO> GetById(int Id)
        {
            CuisineDTO category = new CuisineDTO();
            var programs = _BusinessBase.GetById(Id);
            if (programs != null)
            {
                category.Id = programs.Id;
                category.Name = programs.Name;
            }
            return category;
        }


        public async Task<int> Post(CuisineDTO model)
        {
            int result = 0;
            if (model != null)
            {
                Cuisine cusine = new Cuisine();
                cusine.Name = model.Name;
                result = await _BusinessBase.Post(cusine);
            }
            return result;
        }

        public async Task<int> Put(CuisineDTO model)
        {
            int result = 0;
            if (model != null)
            {
                var cusine = _BusinessBase.GetById(model.Id);
                if (cusine != null)
                {
                    cusine.Name = model.Name;
                    result = await _BusinessBase.Put(model.Id, cusine);
                }
            }
            return result;
        }


        public async Task<int> Delete(int Id)
        {
            int result = 0;
            if (Id != null)
            {
                var area = _BusinessBase.GetById(Id);
                if (area != null)
                {
                    result = await _BusinessBase.Delete(Id);
                }
            }
            return result;
        }


    }
}
