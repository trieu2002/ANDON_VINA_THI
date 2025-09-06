using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDON_Application.DTO
{
    public class RouteDTO
    {
        /// <summary>
        /// Khoá chính của bản ghi
        /// </summary>
        public int RouteId { get; set; }
        /// <summary>
        /// Mã routecode
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Tên công đoạn đang lỗi
        /// </summary>
        public string RouteName { get; set; } = string.Empty;
    }
}
