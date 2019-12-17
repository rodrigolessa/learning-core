using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Comum.Infrastructure.Logging
{
    public class EscritorLog : IEscritorLog
    {
        private StreamWriter _sw;

        public EscritorLog(string nomeDoArquivo)
        {
            var dir = Constants.ArquivoDeConfiguracao.LogDir;
            var arquivo = dir + nomeDoArquivo;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(arquivo))
                _sw = new StreamWriter(arquivo);
            else
                _sw = File.AppendText(arquivo);
        }

        public IEscritorLog Gravar(string t)
        {
            _sw.WriteLine(DateTime.Now);
            _sw.WriteLine(t);
            _sw.WriteLine();

            return this;
        }

        public IEscritorLog Acrescentar(string t)
        {
            _sw.Write(t);

            return this;
        }

        public IEscritorLog AcrescentarLinha(string t)
        {
            _sw.WriteLine(t);

            return this;
        }

        public IEscritorLog Fechar()
        {
            _sw.Close();

            return this;
        }
    }
}
