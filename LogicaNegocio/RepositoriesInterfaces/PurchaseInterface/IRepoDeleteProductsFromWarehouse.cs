using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.PurchaseInterface
{
    public interface IRepoDeleteProductsFromWarehouse <T>
    {
        void DeleteProductsFromWarehouse(T obj);
    }
}
