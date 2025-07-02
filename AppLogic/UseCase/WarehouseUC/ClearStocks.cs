using BusinessLogic.RepositoriesInterfaces.WarehouseInterface;
using SharedUseCase.InterfacesUC.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.WarehouseUC
{
    public class ClearStocks : IClearStocks
    {
        private IRepoWarehouse _repo;
        public ClearStocks(IRepoWarehouse repo)
        {
            _repo = repo;
        }

            public void Execute()
            {
                try
                {
                    _repo.ClearStocks();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al limpiar los stocks de los depósitos", ex);
                }
            }
    }
}
