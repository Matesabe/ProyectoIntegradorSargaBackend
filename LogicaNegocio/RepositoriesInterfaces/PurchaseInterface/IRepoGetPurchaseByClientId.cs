using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.PurchaseInterface
{
    public interface IRepoGetPurchaseByClientId <T>
    {
        IEnumerable<Purchase> GetPurchaseByClientId (int clientId);
    }
}
