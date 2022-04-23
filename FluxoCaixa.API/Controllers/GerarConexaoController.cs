using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilidades;
using Utilidades.Controllers;
using Utilidades.Json;
using Utilidades.JWT;

namespace FluxoCaixa.API.Controllers
{
    public class GerarConexaoController : BaseController
    {
        [Autorizacao]
        public ActionResult Index(string server, string baseDados, string usuario, string senha)
        {
            var valor = Criptografia.CriptografarAES($"Data Source={server};Initial Catalog={baseDados};User Id={usuario};Password={senha}; MultipleActiveResultSets=True;");

            RetornoJson<string> retorno = new RetornoJson<string>()
            {
                Resultado = valor
            };

            return Sucesso(retorno);
        }
    }
}