using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Comum.Infrastructure.Logging
{
    public interface IEscritorLog
    {
        IEscritorLog Gravar(string t);
        IEscritorLog Acrescentar(string t);
        IEscritorLog AcrescentarLinha(string t);
        IEscritorLog Fechar();
    }
}
