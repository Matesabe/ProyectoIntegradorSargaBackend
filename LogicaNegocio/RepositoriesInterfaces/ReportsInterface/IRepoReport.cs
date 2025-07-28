using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.RedemptionInterface;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.ReportsInterface
{
    public interface IRepoReport : IRepoAdd<Report>,
                                        IRepoGetById<Report>,
                                        IRepoGetAll<Report>                                    
    {
    }
}
