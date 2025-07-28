using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Entry
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? ErrorMessage { get; set; }
        public int ReportId { get; set; }           
        public Report Report { get; set; } = null!; 
        public Entry(int id, string description, DateTime date, string type, string status, string errorMessage)
        {
            Id = id;
            Description = description;
            Date = date;
            Type = type;
            Status = status;
            ErrorMessage = errorMessage;
        }
        public Entry() { }
    }
}
