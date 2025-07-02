using AppLogic.Mapper;
using BusinessLogic.Entities;
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
    public class UpdateStocks : IUpdateStocks
    {
        private IRepoWarehouse _repo;
        public UpdateStocks(IRepoWarehouse repo)
        {
            _repo = repo;
        }
        public void Execute(SubProductDto sub, int stockPdelE, int stockCol, int stockPay, int stockPeat, int stockSal)
        {
            try
            {
                int id = sub.Id;
                _repo.UpdateStocks(SubproductMapper.FromDto(sub), stockPdelE, stockCol, stockPay, stockPeat, stockSal);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar los stocks del subproducto", ex);
            }
        }
    }
}
