using BusinessLogic.Entities;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Mapper
{
    public class EntryMapper
    {
        public static Entry FromDto(EntryDto dto)
        {
            return new Entry
            {
                Id = dto.Id,
                Description = dto.Description,
                Date = dto.Date,
                Type = dto.Type,
                Status = dto.Status,
                ErrorMessage = dto.ErrorMessage
            };
        }

        public static EntryDto ToDto(Entry entry)
        {
            return new EntryDto(
                Id: entry.Id,
                Description: entry.Description,
                Date: entry.Date,
                Type: entry.Type,
                Status: entry.Status,
                ErrorMessage: entry.ErrorMessage
            );
        }


        public static List<EntryDto> ToListDto(List<Entry> entries)
        {
            List<EntryDto> entriesDto = new List<EntryDto>();
            foreach (var item in entries)
            {
                entriesDto.Add(ToDto(item));
            }
            return entriesDto;
        }

        public static List<Entry> FromListDtoToEntries(IEnumerable<EntryDto> entriesDto)
        {
            List<Entry> entries = new List<Entry>();
            foreach (var item in entriesDto)
            {
                entries.Add(FromDto(item));
            }
            return entries;
        }
    }
}
