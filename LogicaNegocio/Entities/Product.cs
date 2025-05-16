using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLogic.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string productCode { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<Image> Images { get; set; } = new();
    }
}
