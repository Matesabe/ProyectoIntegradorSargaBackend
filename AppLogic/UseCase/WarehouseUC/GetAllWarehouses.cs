using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces;
using BusinessLogic.RepositoriesInterfaces.WarehouseInterface;
using SharedUseCase.DTOs.User;
using SharedUseCase.DTOs.Warehouse;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.WarehouseUC
{
    public class GetAllWarehouses : IGetAll<WarehouseDto>
    {
        private IRepoWarehouse _repo;

        public GetAllWarehouses(IRepoWarehouse repo)
        {
            _repo = repo;
        }

        public IEnumerable<WarehouseDto> Execute()
        {
            try
            {
                return WarehouseMapper.ToListDto(_repo.GetAll());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los depósitos", ex);
            }
        }
    }
}
