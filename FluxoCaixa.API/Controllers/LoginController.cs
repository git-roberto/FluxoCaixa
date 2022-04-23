using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilidades.Controllers;
using Utilidades.Json;
using Utilidades.Seguranca;

namespace FluxoCaixa.API.Controllers
{
    public class LoginController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        public ActionResult EfetuarLogin(string login, string senha)
        {
            try
            {
                if (login == null)
                {
                    throw new Exception("Nenhuma informação de login informada.");
                }

                using (UsuarioService service = new UsuarioService())
                {
                    string token = service.EfetuarLogin(login, senha);

                    RetornoJson<string> retorno = new RetornoJson<string>()
                    {
                        Resultado = token,
                        QtdRegistros = 1,
                        Mensagem = "Login efetuado com sucesso."
                    };

                    return Sucesso(retorno);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}