using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.DTOs.Product
{
    public record SubProductDto(int Id,
                                int ProductId,
                             string productCode,
                             string Name,
                             double Price,
                             string Color,
                             string Size,
                             string Season,
                                string Year,
                             List<Image>? Images,
                             string genre,
                             string brand,
                             string type,
                             int? stockPdelE, int? stockCol, int? stockPay, int? stockPeat, int? stockSal) 
    {
        public override string ToString()
        {
            var imagesStr = Images != null ? string.Join(", ", Images.Select(img => $"{{id: {img.id}, url: '{img.url}'}}")) : "null";
            return $"SubProductDto {{ Id = {Id}, ProductId = {ProductId}, productCode = '{productCode}', Name = '{Name}', Price = {Price}, Color = '{Color}', Size = '{Size}', Season = '{Season}', Year = '{Year}', Images = [{imagesStr}], genre = '{genre}', brand = '{brand}', type = '{type}' }}";
        }
    
            }


}
