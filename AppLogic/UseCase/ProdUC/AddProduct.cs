using BusinessLogic.RepositoriesInterfaces.ProductsInterface;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.ProdUC
{
    public class AddProduct : IAdd<ProductDto>
    {
        IRepoProducts _repo;
        public AddProduct(IRepoProducts repo)
        {
            _repo = repo;
        }
        public int Execute(ProductDto obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                if (string.IsNullOrWhiteSpace(obj.productCode))
                {
                    throw new ArgumentException("El campo 'productCode' no puede estar vacío", nameof(obj.productCode));
                }
                if (string.IsNullOrWhiteSpace(obj.name))
                {
                    throw new ArgumentException("El campo 'Name' no puede estar vacío", nameof(obj.name));
                }
                if (obj.price <= 0)
                {
                    throw new ArgumentException("El campo 'Price' debe ser mayor que cero", nameof(obj.price));
                }
                return _repo.Add(Mapper.ProductMapper.FromDto(obj));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el producto: " + ex.Message, ex);
            }
    }
    }
}
