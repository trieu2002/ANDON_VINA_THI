using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Application.DTO;
using ANDON_Application.Interface.Defect;
using ANDON_Domain.Interface;
using AutoMapper;

namespace ANDON_Application.Service.Defect
{
    public class DefectService : IDefectService
    {
        private readonly IDefectRepository _defectRepository;
        private readonly IMapper _mapper;
        public DefectService(IDefectRepository defectRepository, IMapper mapper)
        {
            _defectRepository = defectRepository;
            _mapper = mapper;
        }

        public async Task<int> BeginFixAsync(int id)
        {
            var result=await _defectRepository.BeginFixAsync(id);   
            return result;  
        }

        public async Task<int> CompleteFixAsync(int id, string reason, string countermeasure, string repairer)
        {
            var result=await _defectRepository.CompleteFixAsync(id,reason, countermeasure, repairer);
            return result;
        }

        public async Task<int> InsertDefectAsync(string linecode, string routername, string defectname, string? desc, string? defectby, string? operation)
        {
            var result=await _defectRepository.InsertDefectAsync(linecode, routername, defectname, desc, defectby, operation);
            return result;
        }

        public async Task<IEnumerable<DefectDTO>> ViewAllDefectAsync(DateTime? FromDate, DateTime? ToDate, string? GUI = "adon")
        {
            var entities=await _defectRepository.ViewAllDefectAsync(FromDate, ToDate, GUI);
            return _mapper.Map<IEnumerable<DefectDTO>>(entities);
        }

        public async Task<IEnumerable<DefectDTO>> ViewAllDefectExcelAsync(DateTime? FromDate, DateTime? ToDate, string? GUI = "adon")
        {
            var entities = await _defectRepository.ViewAllDefectAsync(FromDate, ToDate, GUI);
            return _mapper.Map<IEnumerable<DefectDTO>>(entities);
        }
    }
}
