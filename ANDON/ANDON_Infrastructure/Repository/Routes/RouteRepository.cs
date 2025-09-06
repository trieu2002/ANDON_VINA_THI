using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Domain.Entities;
using ANDON_Domain.Interface;
using ANDON_Domain.Model;
using Dapper;

namespace ANDON_Infrastructure.Repository.Routes
{
    public class RouteRepository : IRouteRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public RouteRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Route>> GetAllRoutesAsync()
        {
            var result = await _unitOfWork.Connection.QueryAsync<Route>(
                    "usp_ViewAllRoutes",
                commandType: CommandType.StoredProcedure
        );
            return result;
        }

        public async Task<IEnumerable<RouteError>> GetRoueAndNameByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@RouteId", id);
            var result = await _unitOfWork.Connection.QueryAsync<RouteError>(
                "usp_GetRouteWithErrors",
                commandType: CommandType.StoredProcedure,
                param: param
                );
            return result;
        }

        //public async Task<RouteError?> GetRoueAndNameByIdAsync(int id)
        //{
        //    var param = new DynamicParameters();
        //    param.Add("@RouteId", id);
        //    var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<RouteError>(
        //        "usp_GetRouteWithErrors",
        //        commandType: CommandType.StoredProcedure,
        //        param: param
        //        );
        //    return result;
        //}


    }
}
