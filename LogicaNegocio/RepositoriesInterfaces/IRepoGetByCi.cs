using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces
{
    public interface IRepoGetByCi <T>
    {
        T GetByCi(string value);
    }
}
