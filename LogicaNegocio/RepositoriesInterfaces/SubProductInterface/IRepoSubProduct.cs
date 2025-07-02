using BusinessLogic.Entities;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.SubProductInterface
{
    public interface IRepoSubProduct:
            IRepoAdd<SubProduct>,
            IRepoUpdate<SubProduct>,
            IRepoDelete<SubProduct>,
            IRepoGetAll<SubProduct>,
            IRepoGetById<SubProduct>
    {
        IEnumerable<SubProduct> GetByProductCode(string value);
        IEnumerable<SubProduct> GetByProductId(int id);
        void Clear();
    }
}