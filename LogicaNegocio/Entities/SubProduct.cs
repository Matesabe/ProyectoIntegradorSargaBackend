using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class SubProduct 
    {
        protected SubProduct() { } 
        public SubProduct(int id, int ProductId, string productCode, string name, double price, string Color, string Size, List<Image> images, string season, string year, string genre, string brand, string type) 
        {
            this.ProGenre = genre;
            this.ProBrand = brand;
            this.ProType = type;
            this.Color = Color;
            this.Size = Size;
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public List<Image> Images { get; set; } 
        public string ProGenre { get; set; }
        public string ProBrand { get; set; }
        public string ProType { get; set; }

    }
}
