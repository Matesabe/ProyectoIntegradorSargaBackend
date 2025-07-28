using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Report
    {
        public Report()
        {
            entries = new List<Entry>();
            date = DateTime.Now;
            totalLines = 0;
            procecedLines = 0;
        }

        public int id { get; set; }
        public DateTime date { get; set; }
        public List<Entry> entries { get; set; }
        public int totalLines { get; set; }
        public int procecedLines { get; set; }
        public string type { get; set; }

    }
}
