using AppLogic.Mapper;
using BusinessLogic.Entities;
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
    public class AddWarehouse : IAdd<WarehouseDto>
    {
        IRepoWarehouse _repo;
        public AddWarehouse(IRepoWarehouse repo)
        {
            _repo = repo;
        }
        public int Execute(WarehouseDto obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(obj.Name))
            {
                throw new ArgumentException("El campo 'Name' no puede estar vacío", nameof(obj.Name));
            }
            if (obj.SubProducts == null || !obj.SubProducts.Any())
            {
                throw new ArgumentException("El campo 'SubProducts' no puede estar vacío", nameof(obj.SubProducts));
            }
            try
            {
                return _repo.Add(WarehouseMapper.FromDto(obj));
                
            }catch (Exception ex) {
                throw new Exception("Error al agregar el almacén: " + ex.Message, ex);
            }
        }
    }
}
