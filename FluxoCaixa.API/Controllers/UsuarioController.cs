using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilidades.Controllers;
using Utilidades.DTO;
using Utilidades.Json;
using Utilidades.JWT;
using Utilidades.Seguranca;

namespace FluxoCaixa.API.Controllers
{
    public class UsuarioController : BaseController
    {
        [Autorizacao]
        public ActionResult Index()
        {
            using (UsuarioService service = new UsuarioService())
            {
                var lst = service.Listar();

                var lstRetorno = lst.ToList().Select(x => x.ToDTO<Usuario, UsuarioDTO>()).ToList();

                RetornoJson<List<UsuarioDTO>> retorno = new RetornoJson<List<UsuarioDTO>>()
                {
                    Resultado = lstRetorno,
                    QtdRegistros = lst.Count(),
                };

                return Sucesso(retorno);
            }
        }

        [Autorizacao]
        public ActionResult Buscar(int id)
        {
            using (UsuarioService service = new UsuarioService())
            {
                var lstRetorno = service.Buscar(id).ToDTO<Usuario, UsuarioDTO>();

                RetornoJson<UsuarioDTO> retorno = new RetornoJson<UsuarioDTO>()
                {
                    Resultado = lstRetorno,
                };

                return Sucesso(retorno);
            }
        }

        [HttpPost]
        [Autorizacao]
        public ActionResult Inserir(UsuarioDTO registro)
        {
            using (UsuarioService service = new UsuarioService())
            {
                var model = registro.ToEntity<Usuario, UsuarioDTO>(service.Conexao);
                service.Inserir(model);

                RetornoJson<UsuarioDTO> retorno = new RetornoJson<UsuarioDTO>()
                {
                    Mensagem = "Registro incluído com sucesso",
                };

                return Sucesso(retorno);
            }
        }

        [HttpPost]
        [Autorizacao]
        public ActionResult Alterar(UsuarioDTO registro)
        {
            using (UsuarioService service = new UsuarioService())
            {
                var model = registro.ToEntity<Usuario, UsuarioDTO>(service.Conexao);
                service.Alterar(model);

                RetornoJson<UsuarioDTO> retorno = new RetornoJson<UsuarioDTO>()
                {
                    Mensagem = "Registro alterado com sucesso",
                };

                return Sucesso(retorno);
            }
        }

        [HttpPost]
        [Autorizacao]
        public ActionResult Excluir(int id)
        {
            using (UsuarioService service = new UsuarioService())
            {
                service.Excluir(id);

                RetornoJson<UsuarioDTO> retorno = new RetornoJson<UsuarioDTO>()
                {
                    Mensagem = "Registro excluído com sucesso",
                };

                return Sucesso(retorno);
            }
        }
    }
}