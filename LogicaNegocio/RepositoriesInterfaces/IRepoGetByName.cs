using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces
{
    public interface IRepoGetByName <T>
    {
        T GetByName(int id);
    }
}
