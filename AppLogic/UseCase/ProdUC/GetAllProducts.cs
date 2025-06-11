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
    public class GetAllProducts : IGetAll<ProductDto>
    {
        IRepoProducts _repo;

        public GetAllProducts(IRepoProducts repo)
        {
            _repo = repo;
        }
        public IEnumerable<ProductDto> Execute()
        {
            try
            {
                return ProductMapper.ToListDto(_repo.GetAll());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los productos", ex);
            }
    }
    }
}
