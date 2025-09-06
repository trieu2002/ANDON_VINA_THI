using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Application.DTO;

namespace ANDON_Application.Interface.Defect
{
    public interface IDefectService
    {
        Task<IEnumerable<DefectDTO>> ViewAllDefectAsync(DateTime? FromDate, DateTime? ToDate, string? GUI = "adon");
        Task<int> InsertDefectAsync(string linecode, string routername, string defectname, string? desc, string? defectby, string? operation);
        Task<int> BeginFixAsync(int id);
        Task<int> CompleteFixAsync(int id, string reason, string countermeasure, string repairer);
        Task<IEnumerable<DefectDTO>> ViewAllDefectExcelAsync(DateTime? FromDate, DateTime? ToDate, string? GUI = "adon");
    }
}
