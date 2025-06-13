using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.SubProductInterface
{
    public interface IRepoGetByProductId<T>
    {
        IEnumerable<T> GetByProductId(int value);
    }
}
