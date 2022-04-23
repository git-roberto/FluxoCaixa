using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utilidades.DTO;
using Utilidades.Exceptions;
using Utilidades.Json;
using Utilidades.JWT;
using Utilidades.Seguranca;

namespace Utilidades.Controllers
{
    public class BaseController : Controller
    {
        protected UsuarioDTO UsuarioLogado { get; set; }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //Recupera as headers
            //Recupera a controller e actions chamados
            var ms = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.GetMethods(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var m = ms.Where(x => x.Name.ToLower() == (filterContext.ActionDescriptor.ActionName.ToLower())).FirstOrDefault();

            //Verifica se não foi informado nenhum atributo de segurança nas ações(métodos) das controllers
            if (!m.CustomAttributes.Any(x => (x.AttributeType == typeof(Autorizacao)) || x.AttributeType == typeof(AllowAnonymousAttribute) || x.AttributeType == typeof(AllowUsersAttribute)))
            {
                TratamentoErro.ErrorController(HttpStatusCode.InternalServerError, $"Nenhum atributo de autorização informado na ação \"{filterContext.ActionDescriptor.ActionName}\" da controller \"{filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}\".");
            }

            if (!m.CustomAttributes.Any(x => x.AttributeType == typeof(AllowAnonymousAttribute)))
            {
                UsuarioLogado = BuscarUsuario();
            }
        }

        private UsuarioDTO BuscarUsuario()
        {
            var token = VerificaToken();

            UsuarioDTO usuario = new UsuarioDTO();

            if (JWTService.ValidarTokenJWT(token, out var username))
            {
                using (UsuarioService service = new UsuarioService())
                {
                    var usuarioModel = service.Buscar(x => x.Email.Trim().ToUpper() == username.Trim().ToUpper());

                    if (usuarioModel == null)
                    {
                        throw new Exception("Usuário não encontrado");
                    }

                    usuario = usuarioModel.ToDTO<Usuario, UsuarioDTO>();
                }
            }

            return usuario;
        }

        private string VerificaToken()
        {
            var authorization = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorization?.Trim()))
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

            return token;
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            var tipoErro = filterContext.Exception.GetType();

            switch (tipoErro.Name)
            {
                case "HttpException":
                    filterContext.HttpContext.Response.StatusCode = ((HttpException)filterContext.Exception).GetHttpCode();
                    break;
                case "UnauthorizedAccessException":
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case "CoreException":
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case "ForbiddenException":
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case "NotImplementedException":
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                    break;
                default:
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            filterContext.Result = Sucesso(filterContext.Exception.MontarErro());
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CustomJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        protected JsonResult Sucesso<T>(RetornoJson<T> retorno)
        {
            var Retorno = retorno;
            return Json(Retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Sucesso(string mensagem)
        {
            return Json(new { Mensagem = mensagem }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult Sucesso(bool valor)
        {
            RetornoJson<bool> retorno = new RetornoJson<bool>
            {
                Resultado = valor,
            };

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult Sucesso(object retorno)
        {
            return Json(
                new
                {
                    Resultado = retorno,
                }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult Sucesso(object retorno, string mensagem)
        {
            return Json(
                new
                {
                    Resultado = retorno,
                    Mensagem = mensagem
                }, JsonRequestBehavior.AllowGet);
        }

        protected string Erro(ModelStateDictionary modelState)
        {
            StringBuilder msgErro = new StringBuilder();
            foreach (ModelState model in modelState.Values)
            {
                foreach (ModelError error in model.Errors)
                {
                    msgErro.AppendLine($"{error.ErrorMessage}");
                }
            }

            return msgErro.ToString();
        }
    }
}
