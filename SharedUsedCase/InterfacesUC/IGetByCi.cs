using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.InterfacesUC
{
    public interface IGetByCi <T>
    {
        T Execute(string ci);
    }
}
