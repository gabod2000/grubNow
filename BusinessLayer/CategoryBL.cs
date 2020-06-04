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
    public class CategoryBL : BusinessBase<Category>
    {
        private GrubNowDbContext _context;
        private BusinessBase<Category> _BusinessBase;
        public CategoryBL(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _BusinessBase = new BusinessBase<Category>(serviceProvider); 
            _context = serviceProvider.GetRequiredService<GrubNowDbContext>();
        }

        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            var programs = _BusinessBase.Get().ToList();
            List<CategoryDTO> categorylst = null;
            if (programs != null)
            {
                categorylst = new List<CategoryDTO>();
                foreach (var item in programs)
                {
                    CategoryDTO category = new CategoryDTO();
                    category.Id = item.Id;
                    category.Name = item.Name;
                    categorylst.Add(category);
                }
            }
            return categorylst;
        }

        public async Task<CategoryDTO> GetById(int Id)
        {
            CategoryDTO category = new CategoryDTO();
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
