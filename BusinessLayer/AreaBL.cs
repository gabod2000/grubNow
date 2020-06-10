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
    public class AreaBL : BusinessBase<Area>
    {
        private GrubNowDbContext _context;
        private BusinessBase<Area> _BusinessBase;
        private BaseResponse response;
        public AreaBL(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _BusinessBase = new BusinessBase<Area>(serviceProvider); 
            response = new BaseResponse();
            _context = serviceProvider.GetRequiredService<GrubNowDbContext>();
        }



        public async Task<IEnumerable<AreaDTO>> Get()
        {
            var states = _BusinessBase.Get();
            List<AreaDTO> Arealst = null;
            if (states != null)
            {
                Arealst = new List<AreaDTO>();
                foreach (var item in states)
                {
                    AreaDTO area = new AreaDTO();
                    area.AreaName = item.AreaName;
                    area.Id = item.Id;
                    Arealst.Add(area);
                }
            }
            return Arealst;
        }

        public async Task<AreaDTO> GetById(int Id)
        {
            var states = _BusinessBase.GetById(Id);
            AreaDTO state = new AreaDTO();
            if (states != null)
            {
                state.AreaName = states.AreaName;
                state.Id = states.Id;
            }
            return state;
        }

        public async Task<int> Post(AreaDTO model)
        {
            int result = 0;
            if (model!=null)
            {
                Area area = new Area();
                area.AreaName = model.AreaName;
                 result=await _BusinessBase.Post(area);
            }
            return result;
        }

        public async Task<int> Put(AreaDTO model)
        {
            int result = 0;
            if (model != null)
            {
                var area = _BusinessBase.GetById(model.Id);
                if (area != null)
                {
                    area.AreaName = model.AreaName;
                    result = await _BusinessBase.Put(model.Id, area);
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
