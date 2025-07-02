using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLogic.Entities
{
    public  class Product
    {
        public Product(int id, string productCode, string name, double price, string season, string year, string genre, string brand, string type)
        {
            Id = id;
            this.productCode = productCode;
            Name = name;
            Price = price;
            Season = season;
            Year = year;
            Genre = genre; 
            Brand = brand;
            Type = type;
        }

        protected Product() { }
        public int Id { get; set; }
        public string productCode { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Season { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; } 
        public string Brand { get; set; }
        public string Type { get; set; } 
    }
}
