using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Domain.Entities;

namespace ANDON_Domain.Model
{
    public class DefectInsert:BaseEntities
    {
        /// <summary>
        /// Tên Line thực hiện
        /// </summary>
        public string LineCode { get; set; } = string.Empty;
        /// <summary>
        /// Tên công đoạn
        /// </summary>
        public string RouteName { get; set; } = string.Empty;
        /// <summary>
        ///  Tên lỗi của công đoạn
        /// </summary>
        public string ErrorName { get; set; } = string.Empty;
        /// <summary>
        ///  Mô tả chi tiết lỗi
        /// </summary>
        public string ErrorDescription { get;set; } = string.Empty;
        /// <summary>
        /// Người phát hiện
        /// </summary>
        public string DetectedBy { get; set; } = string.Empty;
        /// <summary>
        /// Người thao tác
        /// </summary>
        public string Operator {  get; set; } = string.Empty;   
    }
}
