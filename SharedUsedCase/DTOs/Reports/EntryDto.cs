using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.DTOs.Reports
{
    public record EntryDto(
        int Id,
        string? Description,
        DateTime Date,
        string? Type,
        string? Status,
        string? ErrorMessage
    )
    {

    }
}
