using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.DTOs.Reports
{
    public record ReportDto(
        int Id,
        DateTime Date,
        IEnumerable<EntryDto> Entries,
        int TotalLines,
        int ProcessedLines,
        string type
    )
    {
    }
}
