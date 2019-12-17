using System;
using System.Collections.Generic;

namespace Demo.Comum.Infrastructure.Logging
{
    public partial class Log : EntityBase
    {
        public long Id { get; set; }
        public Nullable<int> IdDoUsuario { get; set; }
        public string NomeDoUsuario { get; set; }
        public string Acao { get; set; }
        public string Onde { get; set; }
        public string Descricao { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public string Sistema { get; set; }
        public string SubSistema { get; set; }
        public string Modulo { get; set; }
        public Nullable<int> IdDoCliente { get; set; }
    }
}
