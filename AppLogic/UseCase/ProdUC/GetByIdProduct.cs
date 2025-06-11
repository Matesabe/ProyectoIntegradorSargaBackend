using AppLogic.Mapper;
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
    public class GetByIdProduct : IGetById<ProductDto>
    {
        IRepoProducts _repo;
        public GetByIdProduct(IRepoProducts repo)
        {
            _repo = repo;
        }

        public ProductDto Execute(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
            }
            ProductDto product = ProductMapper.ToDto(_repo.GetById(id));
            if (product == null)
            {
                throw new KeyNotFoundException("Producto no encontrado");
            }
            return product;
        }
    }
}
