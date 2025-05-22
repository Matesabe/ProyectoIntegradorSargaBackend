using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.UserInterface
{
    public interface IRepoGetByEmail<T>
    {
        T GetByEmail(string email);
    }
}
