using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class SubProduct : Product
    {
        public SubProduct() : base() { }
        public SubProduct(int id, string productCode, string name, double price, string Color, string Size, List<Image> images, string season, string year) : base(id, productCode, name, price, season, year)
        {
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public List<Image> Images { get; set; } = new();

    }
}
