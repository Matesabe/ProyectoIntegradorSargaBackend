using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.InterfacesUC.Redemption
{
    public interface IGetRedemptionByUserId <T>
    {
        IEnumerable<T> Execute(int userId);
    }
}
