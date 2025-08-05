using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces.ProductsInterface;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.ProdUC
{
    public class GetByBrand : IGetByBrand<ProductDto>
    {
        IRepoProducts _repo;
        public GetByBrand(IRepoProducts repo)
        {
            _repo = repo;
        }
        public IEnumerable<ProductDto> Execute(string brand)
        {
            try
            {
                return ProductMapper.ToListDto(_repo.GetByBrand(brand));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos por marca", ex);
            }
        }
    }
}
