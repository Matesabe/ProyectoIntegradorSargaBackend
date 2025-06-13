using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using BusinessLogic.RepositoriesInterfaces.UserInterface;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.ProductsInterface
{
    public interface IRepoProducts: IRepoAdd<Product>,
            IRepoUpdate<Product>,
            IRepoDelete<Product>,
            IRepoGetAll<Product>,
            IRepoGetById<Product>
    {
        
        
  
    }
}
