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
            this.Id = id;
            this.ProductId = ProductId;
            this.Code = productCode;
            this.Name = name;
            this.Price = price;
            this.Season = season;
            this.Year = year;
            this.Images = images ?? new List<Image>();  
            this.Genre = genre;
            this.Brand = brand;
            this.Type = type;
            this.Color = Color;
            this.Size = Size;
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Season { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public List<Image> Images { get; set; } 
        public string Genre { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public List<WarehouseStock> Stocks { get; set; } = new();
    }
}
