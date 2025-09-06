using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDON_Application.DTO
{
    public class RouteErrorDTO
    {
        /// <summary>
        /// Mã công đoạn
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Tên công đoạn
        /// </summary>
        public string RouteName { get; set; } = string.Empty;
        /// <summary>
        /// Tên lỗi
        /// </summary>
        public string? ErrorName { get; set; } = string.Empty;
    }
}
