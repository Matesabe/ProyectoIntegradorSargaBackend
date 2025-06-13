using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.InterfacesUC.Product
{
    public interface IGetByProductId<T>
    {
        /// <summary>
        /// Retrieves a collection of items by the specified product ID.
        /// </summary>
        /// <param name="id">The product ID to filter the items.</param>
        /// <returns>An enumerable collection of items that match the specified product ID.</returns>
        IEnumerable<T> Execute(int id);
    }
}
