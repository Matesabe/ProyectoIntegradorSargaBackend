using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.DTOs.Product
{
    public record SubproductDto(int Id,
                             string productCode,
                             string Name,
                             double Price,
                             string Color,
                             string Size,
                             string Season,
                                string Year,
                             List<Image> Images) 
    {}

}
