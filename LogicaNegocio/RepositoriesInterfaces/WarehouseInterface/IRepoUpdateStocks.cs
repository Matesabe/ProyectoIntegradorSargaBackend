using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.WarehouseInterface
{
    public interface IRepoUpdateStocks
    {
        void UpdateStocks(SubProduct sub, int stockPdelE, int stockCol, int stockPay, int stockPeat, int stockSal);
    }
}
