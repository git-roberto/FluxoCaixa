using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Utilidades.Exceptions;
using Utilidades.Seguranca;

namespace Utilidades.JWT
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class Autorizacao : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext context)
        {
            var request = context.HttpContext.Request;

            try
            {
                var authorization = request.Headers["Authorization"];

                if (string.IsNullOrEmpty(authorization.Trim()))
                {
                    throw new Exception("Não foi informado um token de acesso");
                }

                var mtzToken = authorization.Split(' ');

                if (mtzToken.Length != 2)
                {
                    throw new Exception("Token de acesso inválido");
                }

                if (mtzToken[0] != "Bearer")
                {
                    throw new Exception("Não foi informado um token de acesso");
                }

                var token = mtzToken[1];

                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Não foi informado um token de acesso válido");
                }

                VerificaJwtToken(token);
            }
            catch (Exception ex)
            {
                TratamentoErro.ErrorController(HttpStatusCode.Unauthorized, ex.MontarErro());

            }
        }

        protected void VerificaJwtToken(string token)
        {
            if (JWTService.ValidarTokenJWT(token, out var username))
            {
                using (UsuarioService service = new UsuarioService())
                {
                    var retorno = service.Buscar(x => x.Email.Trim().ToUpper() == username.Trim().ToUpper());

                    if (retorno == null)
                    {
                        throw new Exception("Usuário não encontrado");
                    }
                }
            }
            else
            {
                throw new Exception("Token de acesso inválido");
            }
        }
    }
}
