using AppLogic.Mapper;
using BusinessLogic.Entities;
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
    public class GetByProductCode : IGetByProductCode<ProductDto>
    {
        private IRepoProducts _repo;
        public GetByProductCode(IRepoProducts repo)
        {
            _repo = repo;
        }

        public IEnumerable<ProductDto> Execute(string code)
        {
            try
            {
                return ProductMapper.ToListDto(_repo.GetByProductCode(code));
            }catch(Exception ex)
            {
                throw new Exception("Error al obtener los sub productos por código de producto", ex);
            }
        }
    }
}
