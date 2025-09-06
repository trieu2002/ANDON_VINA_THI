
using ANDON_Application.DTO;
using ANDON_Application.Interface.Defect;
using ANDON_Domain.Entities;
using ANDON_Domain.Exceptions;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ANDON.Controllers.Defect
{
    [Route("/api/v1/defect")]
    public class DefectController: ControllerBase
    {
        private readonly IDefectService _defectService;
        public DefectController(IDefectService defectService)
        {
            _defectService = defectService;
        }
        [Authorize]
        [HttpGet("list")]

        public async Task<IActionResult> GetDefectListAsync([FromQuery] DateTime? FromDate, [FromQuery] DateTime? ToDate,string? GUI = "adon")
        {
            var result=await _defectService.ViewAllDefectAsync(FromDate, ToDate, GUI);
            return Ok(new
            {
                status = 200,
                message = "lấy dữ liệu thành công",
                defect = result
            });
        }

        [Authorize]
        [HttpPost("insert")]
        public async Task<IActionResult> InsertDefectAsync([FromBody] DefectInsertDTO insertDTO)
        {
           
            var result = await _defectService.InsertDefectAsync(insertDTO.LineCode, insertDTO.RouteName, insertDTO.ErrorName, insertDTO.ErrorDescription, insertDTO.DetectedBy, insertDTO.Operator);
            if (result > 0)
            {
                return Ok(new
                {
                    status = 201,
                    message = "Thêm mới thành công"
                });
            }
            throw new BadRequestException(401, "Thêm mới thất bại", "Thêm mới thất bại");
        }
        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBeginFix([FromRoute] int id)
        {
            var result=await _defectService.BeginFixAsync(id);
            if (result > 0)
            {
                return Ok(new
                {
                    status = 201,
                    message = "Cập nhật thành công"
                });
            }
            throw new BadRequestException(401, "Cập nhật thất bại", "Cập nhật thất bại");
        }
        [Authorize]
        [HttpPut("complete")]
        public async Task<IActionResult> CompleteDefect([FromBody] DefectUpdateDTO defectUpdateDTO)
        {
            var result = await _defectService.CompleteFixAsync(defectUpdateDTO.Id, defectUpdateDTO.Reason, defectUpdateDTO.Countermeasure, defectUpdateDTO.Repairer);
            if (result > 0)
            {
                return Ok(new
                {
                    status = 201,
                    message = "Hoàn thành nhập lỗi thành công"
                });
            }
            throw new BadRequestException(401, "Hoàn thành nhập lỗi thất bại", "Hoàn thành nhập lỗi thất bại");
        }
        [Authorize]
        [HttpGet("export-excel")]

        public async Task<IActionResult> GetDefectExportExcelAsync([FromQuery] DateTime? FromDate, [FromQuery] DateTime? ToDate, string? GUI = "adon")
        {
            try
            {
                var data = await _defectService.ViewAllDefectAsync(FromDate, ToDate, GUI);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Template", "Template.xlsx");
                if (!System.IO.File.Exists(filePath))
                    throw new FileNotFoundException("Không tìm thấy template Excel: " + filePath);

                using var workbook = new XLWorkbook(filePath);
                var worksheet = workbook.Worksheet("Sheet1");

                int startRow = 2;
                int stt = 1;

                foreach (var item in data)
                {
                    worksheet.Cell(startRow, 1).Value = stt;
                    worksheet.Cell(startRow, 2).Value = item.LineCode;
                    worksheet.Cell(startRow, 3).Value = item.RouteName;
                    worksheet.Cell(startRow, 4).Value = item.ErrorName;
                    worksheet.Cell(startRow, 5).Value = item.ErrorDescription;
                    worksheet.Cell(startRow, 6).Value = item.DetectedBy;
                    worksheet.Cell(startRow, 7).Value = item.Operator;
                    worksheet.Cell(startRow, 8).Value = item.Reason;
                    worksheet.Cell(startRow, 9).Value = item.Countermeasure;
                    worksheet.Cell(startRow, 10).Value = item.Repairer;
                    worksheet.Cell(startRow, 11).Value = item.BeginFix;
                    worksheet.Cell(startRow, 12).Value = item.Status;
                    worksheet.Cell(startRow, 13).Value = item.FinishFix;
                    worksheet.Cell(startRow, 14).Value = item.RepairDuration;
                    startRow++;
                    stt++;
                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Defect_Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi export Excel: {ex.Message}");
            }

        }
    }
}
