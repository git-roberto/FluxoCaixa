using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utilidades.Exceptions;
using Utilidades.Json;

namespace Utilidades.Controllers
{
    public class BaseController : Controller
    {
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
