using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Domain.Entities;
using ANDON_Domain.Interface;
using Dapper;


namespace ANDON_Infrastructure.Repository
{
    public class DefectRepository : IDefectRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public DefectRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> BeginFixAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pId", id);
            var result = await _unitOfWork.Connection.ExecuteAsync(
                "usp_UpdateBeginFixTime",
                parameters,
                 commandType: CommandType.StoredProcedure
              );
            return result;
        }

        public async Task<int> CompleteFixAsync(int id, string reason, string countermeasure, string repairer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pId", id);
            parameters.Add("@pReason", reason);
            parameters.Add("@pCountermeasure", countermeasure);
            parameters.Add("@pRepairer", repairer);
            var result = await _unitOfWork.Connection.ExecuteAsync(
                "usp_UpdateFinishFix",
                parameters,
                commandType: CommandType.StoredProcedure
               );
            return result;

        }

        public async Task<IEnumerable<Defect>> GetDefectsForExportAsync(DateTime? fromDate, DateTime? toDate, string? GUI = "andon")
        {
            var paramenters = new DynamicParameters();

            paramenters.Add("@pFromDate", fromDate);
            paramenters.Add("@pToDate", toDate);
            paramenters.Add("@pFromGUI", GUI);
            var result = await _unitOfWork.Connection.QueryAsync<Defect>(
                   "usp_getAndon_excel",
            paramenters,
             commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<int> InsertDefectAsync(string linecode, string routername, string defectname, string? desc, string? defectby, string? operation)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pUserId", linecode);
                parameters.Add("@pRouterName", routername);
                parameters.Add("@pDefectName", defectname);
                parameters.Add("@pDesc", desc);
                parameters.Add("@pNguoiPhatHien", defectby);
                parameters.Add("@pNguoiThaoTax", operation);


                var result = await _unitOfWork.Connection.ExecuteAsync(
                    "usp_InsertDefect_uid",
                    parameters,
                    commandType: CommandType.StoredProcedure
                 );
                return result;
            }catch(Exception ex)
            {
                Console.WriteLine($"Lỗi khi InsertDefectAsync: {ex.Message}");
                throw new Exception("Lỗi khi thêm lỗi defect", ex);
            }
        }

        public async Task<IEnumerable<Defect>> ViewAllDefectAsync(DateTime? FromDate, DateTime? ToDate, string? GUI = "adon")
        {
            var paramenters = new DynamicParameters();

            paramenters.Add("@pFromDate", FromDate);
            paramenters.Add("@pToDate", ToDate);
            paramenters.Add("@pFromGUI", GUI);
            var result = await _unitOfWork.Connection.QueryAsync<Defect>(
                    "usp_Vietnam_AndonDetail_get_V1",
             paramenters,
              commandType: CommandType.StoredProcedure
             );

            return result;
        }
    }
}
