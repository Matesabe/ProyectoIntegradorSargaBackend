using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.DTOs.Product
{
    public record ProductDto
    (
        int id, string productCode, string name, double price, string season, string year, string genre, string brand, string type
    )
    { }
}
