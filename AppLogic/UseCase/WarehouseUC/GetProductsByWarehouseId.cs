using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces.WarehouseInterface;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.WarehouseUC
{
    public class GetProductsByWarehouseId : IGetProductosByWarehouseId<SubProductDto>
    {
        IRepoWarehouse _repo;
        public GetProductsByWarehouseId(IRepoWarehouse repo)
        {
            _repo = repo;
        }
        public IEnumerable<SubProductDto>  Execute(int id){
            if (id <= 0)
            {
                throw new ArgumentException("El ID del depósito debe ser mayor que cero", nameof(id));
            }
            try
            {
                var products = _repo.GetProductsByWarehouseId(id);
                return products.Select(p => SubproductMapper.ToDto(p)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos del depósito: " + ex.Message, ex);
            }
    }
}
}
