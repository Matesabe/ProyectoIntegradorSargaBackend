using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class SubProduct : Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

    }
}
