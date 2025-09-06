using ANDON_Application.Interface.Defect;
using ANDON_Application.Interface.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ANDON.Controllers.Routes
{
    [Route("/api/v1/router")]
    public class RouteController:ControllerBase
    {
        
        private readonly IRouteService _routeService;
        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }
        [Authorize]
        [HttpGet("list")]

        public async Task<IActionResult> GetDefectListAsync()
        {
            var result = await _routeService.GetAllRoutesAsync();
            return Ok(new
            {
                status = 200,
                message = "lấy dữ liệu thành công",
                routes = result
            });
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteByIdAsync(int id)
        {
            var result = await _routeService.GetRoueAndNameByIdAsync(id);

            return Ok(new
            {
                status = 200,
                message = "Lấy dữ liệu thành công",
                errors = result
            });
        }
    
    }
}
