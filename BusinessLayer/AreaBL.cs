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

        public async Task<StateDTO> GetById(int Id)
        {
            var states = _BusinessBase.GetById(Id);
            StateDTO state = new StateDTO();
            if (states != null)
            {
                state.Name = states.AreaName;
                state.Id = states.Id;
            }
            return state;
        }

    }
}
