using AppLogic.Exceptions.RepoException;
using BusinessLogic.RepositoriesInterfaces;
using BusinessLogic.RepositoriesInterfaces.WarehouseInterface;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.WarehouseUC
{
    public class DeleteWarehouse: IRemove
    {
        private IRepoWarehouse _repo;

        public DeleteWarehouse(IRepoWarehouse repo)
        {
            _repo = repo;
        }
        public void Execute(int id)
        {
            try
            {
                _repo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException("Error al eliminar el depósito", ex);
            }
        }
    }
}
