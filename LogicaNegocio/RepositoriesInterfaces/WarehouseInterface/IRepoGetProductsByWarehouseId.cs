using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.WarehouseInterface
{
    public interface IRepoGetProductsByWarehouseId<T>
    {

        IEnumerable<T> GetProductsByWarehouseId(int warehouseId);
    }
}
