using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.InterfacesUC.Warehouse
{
    public interface IGetProductosByWarehouseId <T>
    {
        /// <summary>
        /// Gets the products by warehouse identifier.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <returns>A list of products associated with the specified warehouse.</returns>
        IEnumerable<T> Execute(int warehouseId);
    }
}
