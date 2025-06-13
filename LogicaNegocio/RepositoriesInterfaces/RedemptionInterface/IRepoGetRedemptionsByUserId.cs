using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RepositoriesInterfaces.RedemptionInterface
{
    public interface IRepoGetRedemptionsByUserId <T>
    {
        IEnumerable<Redemption> GetRedemptionByUserId(int clientId);
    }
}
