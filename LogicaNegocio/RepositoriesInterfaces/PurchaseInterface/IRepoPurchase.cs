using BusinessLogic.Entities;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.PurchaseInterface
{
    public interface IRepoPurchase: IRepoPurchaseSetPointsToUser<int>, 
                                        IRepoAdd<Purchase>,
        IRepoGetById<Purchase>,
        IRepoGetAll<Purchase>,
        IRepoDelete<Purchase>,
        IRepoUpdate<Purchase>,
        IRepoGetPurchaseByClientId<Purchase>,
        IRepoDeleteProductsFromWarehouse<Purchase>
    {
    }
}
