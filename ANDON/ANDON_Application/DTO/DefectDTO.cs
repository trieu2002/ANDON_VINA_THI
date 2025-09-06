using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDON_Application.DTO
{
    public class DefectDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Mã Line
        /// </summary>
        public string LineCode { get; set; } = string.Empty;
        /// <summary>
        /// Tên công đoạn
        /// </summary>
        public string RouteName { get; set; } = string.Empty;
        /// <summary>
        /// Tên lỗi của công đoạn
        /// </summary>

        public string ErrorName { get; set; } = string.Empty;
        /// <summary>
        /// Mô tả chi tiết lỗi công đoạn
        /// </summary>
        public string ErrorDescription { get; set; } = string.Empty;
        /// <summary>
        /// Người phát hiện
        /// </summary>
        public string DetectedBy { get; set; } = string.Empty;
        /// <summary>
        /// Nguyên nhân
        /// </summary>

        public string Operator { get; set; } = string.Empty;
        /// <summary>
        /// Lý do
        /// </summary>
        public string Reason { get; set; } = string.Empty;
        /// <summary>
        /// Cách khắc phục
        /// </summary>
        public string Countermeasure { get; set; } = string.Empty;
        /// <summary>
        /// Người bao trì
        /// </summary>
        public string Repairer { get; set; } = string.Empty;


        public DateTime? BeginOccur { get; set; }
        public DateTime? BeginFix { get; set; }
        public DateTime? FinishFix { get; set; }
        /// <summary>
        /// Thời gian bảo trì
        /// </summary>
        public int RepairDuration { get; set; }
        /// <summary>
        /// Trạng thái bảo trì
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreatedAt { get; set; }
   
 
  

    }
}
