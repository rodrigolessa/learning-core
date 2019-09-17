using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Driver;
using Models;

namespace core.mongo.webapi.Controllers
{
    [RoutePrefix("api/utilitario")]
    public class LogoController : ApiController
    {
        [HttpGet]
        [Route("logo/obterImagens")]
        public ImagemDaLogo ObterImagens()
        {
            var client = AbrirConexaoMongoDB();
            var logoDB = client.GetDatabase("logo");
            var filaTable = logoDB.GetCollection<ImagemDaLogo>("imagem_18set");

            var query = filaTable.Find(c => !c.avaliado);
            var item = query
            .SortByDescending(c => c.cluster)
            .FirstOrDefault();

            if (item == null)
            {
                return null;
            }

            #region

            //var qExistente = filaTable.Find(c => c.avaliado & c.processos.Any(p => p.numeroProcesso == item.numeroProcesso && p.igual));
            //var itemExistente = qExistente.FirstOrDefault();

            //if (itemExistente != null)
            //{
            //    itemExistente.processos.AddRange(item.processos.Where(c => !itemExistente.processos.Any(p => p.numeroProcesso == c.numeroProcesso) && c.numeroProcesso != itemExistente.numeroProcesso && c.similaridade <= 0.01).ToList());

            //    var filter = Builders<ImagemDaLogo>.Filter.Eq(s => s.id, itemExistente.id);
            //    filaTable.ReplaceOne(filter, itemExistente);

            //    item.processos.RemoveAll(c => itemExistente.processos.Any(p => p.numeroProcesso == c.numeroProcesso) || c.numeroProcesso == itemExistente.numeroProcesso);

            //    if (!item.processos.Any())
            //    {
            //        item.avaliado = true;
            //        filter = Builders<ImagemDaLogo>.Filter.Eq(s => s.id, item.id);
            //        filaTable.ReplaceOne(filter, item);

            //        return ObterImagensDuplicadas();
            //    }
            //    else
            //    {
            //        filter = Builders<ImagemDaLogo>.Filter.Eq(s => s.id, item.id);
            //        filaTable.ReplaceOne(filter, item);
            //    }
            //}
            #endregion
             
            return item;
        }

        [HttpPost]
        [Route("logo/salvarImagens")]
        public bool salvarImagens([FromBody]ImagemDaLogo request)
        {
            var client = AbrirConexaoMongoDB();
            var logoDB = client.GetDatabase("logo");
            var filaTable = logoDB.GetCollection<ImagemDaLogo>("imagem_18set");

            if (request != null)
            {
                request.avaliado = true;
                var filter = Builders<ImagemDaLogo>.Filter.Eq(s => s.id, request.id);
                filaTable.ReplaceOne(filter, request);

                return true;
            }

            return false;
        }

        [HttpPost]
        [Route("logo/desfazerImagens")]
        public ImagemDaLogo DesfazerImagens([FromUri]string request)
        {
            var client = AbrirConexaoMongoDB();
            var logoDB = client.GetDatabase("logo");
            var filaTable = logoDB.GetCollection<ImagemDaLogo>("imagem_18set");

            if (!string.IsNullOrWhiteSpace(request))
            {
                var query = filaTable.Find(c => c.id == request);
                var item = query.FirstOrDefault();

                if (item != null)
                {
                    item.avaliado = false;

                    var filter = Builders<ImagemDaLogo>.Filter.Eq(s => s.id, request);
                    filaTable.ReplaceOne(filter, item);

                    return item;
                }
            }

            return null;
        }

        private MongoClient AbrirConexaoMongoDB()
        {
            return new MongoClient(new MongoClientSettings()
            {
                Server = new MongoServerAddress(ConfigurationManager.AppSettings["urlMongoDB"], 27017),
                MaxConnectionPoolSize = 10000000,
                WaitQueueSize = 10000000
            });
        }
    }
}