using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces.ProductsInterface;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.ProductUC
{
    public class GetByProductCodeSubProduct : IGetByProductCode<SubProductDto>
    {
        private IRepoSubProduct _repo;
        public GetByProductCodeSubProduct(IRepoSubProduct repo)
        {
            _repo = repo;
        }

        public IEnumerable<SubProductDto> Execute(string code)
        {
            try
            {
                return SubproductMapper.ToListDto(_repo.GetByProductCode(code));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los sub productos por código de producto", ex);
            }
        }
    }
}
