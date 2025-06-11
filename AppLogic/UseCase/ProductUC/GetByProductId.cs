using AppLogic.Mapper;
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
    public class GetByProductId : IGetByProductId<SubProductDto>
    {
        IRepoSubProduct _repo;
        public GetByProductId(IRepoSubProduct repo)
        {
            _repo = repo;
        }
        public IEnumerable<SubProductDto> Execute(int id)
        {
            try
            {
                return SubproductMapper.ToListDto(_repo.GetByProductId(id));               
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los sub productos por ID de producto", ex);
            }
        }
    }
}
