using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDON_Domain.Entities
{
    public class BaseEntities
    {
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreateUserId { get; set; } = string.Empty;
        /// <summary>
        /// Thời gian thay đổi
        /// </summary>
        public DateTime ChangeDateTime { get; set; }
        /// <summary>
        /// Ai là người thay đổi
        /// </summary>
        public string ChangeUserId { get; set; }= string.Empty;
    }
}
