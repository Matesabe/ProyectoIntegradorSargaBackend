using BusinessLogic.Entities;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.RedemptionInterface
{
    public interface IRepoRedemption : IRepoAdd<Redemption>,
                                        IRepoGetById<Redemption>,
                                        IRepoGetAll<Redemption>,
                                        IRepoUpdate<Redemption>,
                                        IRepoDelete<Redemption>,
                                        IRepoGetRedemptionsByUserId<Redemption>
    {
    }
}
