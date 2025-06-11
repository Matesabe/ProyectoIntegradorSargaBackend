using BusinessLogic.Entities;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.WarehouseInterface
{
    public interface IRepoWarehouse: IRepoAdd<Warehouse>,
                                     IRepoUpdate<Warehouse>,
                                     IRepoDelete<Warehouse>,
                                     IRepoGetAll<Warehouse>,
                                     IRepoGetById<Warehouse>

    {
        IEnumerable<SubProduct> GetProductsByWarehouseId(int id);
       
    }
}
