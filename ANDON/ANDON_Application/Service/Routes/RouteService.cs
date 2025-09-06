using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Application.DTO;
using ANDON_Application.Interface.Routes;
using ANDON_Domain.Entities;
using ANDON_Domain.Exceptions;
using ANDON_Domain.Interface;
using ANDON_Domain.Model;
using AutoMapper;

namespace ANDON_Application.Service.Routes
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;
        public RouteService(IRouteRepository routeRepository, IMapper mapper)
        {
            _routeRepository = routeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RouteDTO>> GetAllRoutesAsync()
        {
            var entities=await _routeRepository.GetAllRoutesAsync();
            return _mapper.Map<IEnumerable<RouteDTO>>(entities);
        }

        public async Task<IEnumerable<RouteErrorDTO>> GetRoueAndNameByIdAsync(int id)
        {
            var entities = await _routeRepository.GetRoueAndNameByIdAsync(id);
            if (entities == null) throw new NotFoundException(400, "Không tìm thấy tên lỗi", "Không tìm thấy tên lỗi");
            return _mapper.Map<IEnumerable<RouteErrorDTO>>(entities);
        }

        //public async Task<RouteErrorDTO?> GetRoueAndNameByIdAsync(int id)
        //{
        //    var entities = await _routeRepository.GetRoueAndNameByIdAsync(id);
        //    if (entities == null) throw new NotFoundException(400, "Không tìm thấy tên lỗi", "Không tìm thấy tên lỗi");

        //    return _mapper.Map<RouteErrorDTO>(entities);
        //}
    }
}
