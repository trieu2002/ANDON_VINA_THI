using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDON_Application.DTO
{
    public class DefectUpdateDTO
    {
        public int Id { get; set; } 
        public string Reason { get; set; }  =string.Empty;
        public string Countermeasure { get; set; } =string.Empty;
        public string Repairer { get; set; } = string.Empty;
    }
}
