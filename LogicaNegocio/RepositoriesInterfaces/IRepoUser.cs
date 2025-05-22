using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.UserInterface;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces
{
    public interface IRepoUser :
            IRepoAdd<User>,
            IRepoUpdate<User>,
            IRepoDelete<User>,
            IRepoGetAll<User>,
            IRepoGetById<User>,
            IRepoGetByEmail<User>,
            IRepoGetByCi<User>,
            IRepoGetByName<User>
    {
        IEnumerable<User> GetByName(string value);
        User GetByCi(string value);
        User GetByEmail(string value);
    }
}
