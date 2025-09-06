using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Domain.Entities;
using ANDON_Domain.Model;

namespace ANDON_Domain.Interface
{
    public interface IRouteRepository
    {
        /// <summary>
        /// Lấy ra tất cả các công đoạn
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Route>> GetAllRoutesAsync();
        /// <summary>
        /// Lấy ra tất cả các lỗi thuộc từng công đoạn
        /// </summary>
        /// <param name="id">id của công đoạn đó</param>
        /// <returns></returns>
        Task<IEnumerable<RouteError>> GetRoueAndNameByIdAsync(int id);
    }
}
