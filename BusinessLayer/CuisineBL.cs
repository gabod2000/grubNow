using CommonLayer;
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





    }
}
