using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.InterfacesUC.Purchase
{
    public interface ISetPointsToUser<in T>
    {
        /// <summary>
        /// Sets points to a user.
        /// </summary>
        /// <param name="userId">The identifier of the user.</param>
        /// <param name="points">The points to set.</param>
        void Execute(int userId, int points);
    }
}
