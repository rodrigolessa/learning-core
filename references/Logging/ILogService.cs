using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Comum.Infrastructure.Logging
{
    public interface ILogService
    {
        Boolean Gerar(Log log);
        Boolean Gerar(string acao, string onde, string descricao, string sistema, string subSistema, string modulo, Nullable<Int32> idDoCliente);
        IEnumerable<Log> Obter(int idDoUsuario, DateTime dataDeInicio, DateTime dataDeFim);
        IEnumerable<Log> Obter(int idDoCliente);
    }
}
