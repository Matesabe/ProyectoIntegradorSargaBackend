using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Warehouse
    {
        protected Warehouse() { }
        public Warehouse(int id, string name, IEnumerable<SubProduct> enumerable)
        {
            Id = id;
            Name = name;
            this.SubProducts = enumerable;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SubProduct> SubProducts { get; set; }
    }
}
