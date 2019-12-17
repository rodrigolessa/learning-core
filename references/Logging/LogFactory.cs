using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Comum.Infrastructure.Logging
{
    public class LogFactory
    {
        static ILogFactory _current = null;

        public static void SetCurrent(ILogFactory logFactory)
        {
            _current = logFactory;
        }

        public static void SetCurrentUser(string nomeDoUsuario, int idDoUsuario)
        {
            if (_current != null)
            {
                _current.NomeDoUsuarioLogado = nomeDoUsuario;
                _current.IdDoUsuarioLogado = idDoUsuario;
            }
        }

        public static ILogService CreateLog()
        {
            return (_current != null ? _current.Create() : null);
        }

        public static IEscritorLog CreateLog(string nomeDoUsuario)
        {
            return new EscritorLog(Constants.ArquivoDeConfiguracao.ArquivoLogErro).Gravar(nomeDoUsuario);
        }
    }
}
