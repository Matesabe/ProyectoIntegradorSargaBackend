using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces.WarehouseInterface;
using SharedUseCase.DTOs.Warehouse;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.WarehouseUC
{
    public class UpdateWarehouse: IUpdate<WarehouseDto>
    {
        private IRepoWarehouse _repo;
        public UpdateWarehouse(IRepoWarehouse repo)
        {
            _repo = repo;
        }

        public void Execute(int id, WarehouseDto obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(obj.Name))
            {
                throw new ArgumentException("El campo 'Name' no puede estar vacío", nameof(obj.Name));
            }
            if (obj.Stocks == null || !obj.Stocks.Any())
            {
                throw new ArgumentException("El campo 'SubProducts' no puede estar vacío", nameof(obj.Stocks));
            }
            try
            {
                _repo.Update(id, WarehouseMapper.FromDto(obj));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el depósito: " + ex.Message, ex);
            }
    }
    
    }
}
