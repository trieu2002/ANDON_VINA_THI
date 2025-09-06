using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Domain.Entities;

namespace ANDON_Domain.Interface
{
    public interface IDefectRepository
    {
        Task<IEnumerable<Defect>> ViewAllDefectAsync(DateTime ?FromDate, DateTime ?ToDate, string ?GUI="adon");
        Task<int> InsertDefectAsync(string linecode, string routername, string defectname, string? desc, string? defectby, string? operation);
        Task<int> BeginFixAsync(int id);
        Task<int> CompleteFixAsync(int id, string reason, string countermeasure, string repairer);
        Task<IEnumerable<Defect>> GetDefectsForExportAsync(DateTime? fromDate, DateTime? toDate, string? GUI = "andon");

    }
}
