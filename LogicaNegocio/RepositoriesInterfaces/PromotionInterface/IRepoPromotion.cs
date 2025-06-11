using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.PurchaseInterface;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.PromotionInterface
{
    public interface IRepoPromotion:IRepoAdd<PurchasePromotion>,
        IRepoGetById<PurchasePromotion>,
        IRepoGetAll<PurchasePromotion>,
        IRepoDelete<PurchasePromotion>,
        IRepoUpdate<PurchasePromotion>
    {
    }
}
