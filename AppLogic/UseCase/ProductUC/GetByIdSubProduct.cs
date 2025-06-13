using AppLogic.Mapper;
using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.ProductUC
{
    public class GetByIdSubProduct: IGetById<SubProductDto>
    {
        private IRepoSubProduct _repo;
        public GetByIdSubProduct(IRepoSubProduct repo)
        {
            _repo = repo;
        }
        public SubProductDto Execute(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
            }
            SubProductDto subProduct = SubproductMapper.ToDto(_repo.GetById(id));
            if (subProduct == null)
            {
                throw new KeyNotFoundException("Subproducto no encontrado");
            }
            return subProduct;
        }
    }
}
