using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Comum.Infrastructure.Logging
{
    public interface ILogFactory
    {
        string NomeDoUsuarioLogado { get; set; }
        Nullable<Int32> IdDoUsuarioLogado { get; set; }
        ILogService Create();
    }
}