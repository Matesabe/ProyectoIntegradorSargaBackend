using BusinessLogic.Entities;
using SharedUseCase.DTOs.Redemption;
using SharedUseCase.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Mapper
{
    public class ReportMapper
    {
        public static Report FromDto(ReportDto dto)
        {
            return new Report
            {
                id = dto.Id,
                date = dto.Date,
                entries = EntryMapper.FromListDtoToEntries(dto.Entries).ToList(),
                totalLines = dto.TotalLines,
                procecedLines = dto.ProcessedLines,
                type = dto.type
            };
        }

        public static ReportDto ToDto(Report report)
        {
            return new ReportDto(
                report.id,
                report.date,
                EntryMapper.ToListDto(report.entries),
                report.totalLines,
                report.procecedLines,
                report.type
            );
        }

        public static IEnumerable<ReportDto> ToListDto(IEnumerable<Report> reports)
        {
            List<ReportDto> reportsDto = new List<ReportDto>(); ;
            foreach (var item in reports)
            {
                reportsDto.Add(ToDto(item));
            }
            return reportsDto;
        }

        public static List<Report> FromListDtoToRedemption(List<ReportDto> reportDtos)
        {
            List<Report> reports = new List<Report>();
            foreach (var item in reportDtos)
            {
                reports.Add(FromDto(item));
            }
            return reports;
        }
    }
}
