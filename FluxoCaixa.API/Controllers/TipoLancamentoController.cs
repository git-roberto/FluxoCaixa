using FluxoCaixa.DTO;
using FluxoCaixa.Model;
using FluxoCaixa.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilidades.Controllers;
using Utilidades.DTO;
using Utilidades.Json;
using Utilidades.JWT;

namespace FluxoCaixa.API.Controllers
{
    public class TipoLancamentoController : BaseController
    {
        [Autorizacao]
        public ActionResult Index()
        {
            using (TipoLancamentoService service = new TipoLancamentoService())
            {
                var lst = service.Listar();

                var lstRetorno = lst.ToList().Select(x => x.ToDTO<TipoLancamento, TipoLancamentoDTO>()).ToList();

                RetornoJson<List<TipoLancamentoDTO>> retorno = new RetornoJson<List<TipoLancamentoDTO>>()
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
            using (TipoLancamentoService service = new TipoLancamentoService())
            {
                var lstRetorno = service.Buscar(id).ToDTO<TipoLancamento, TipoLancamentoDTO>();

                RetornoJson<TipoLancamentoDTO> retorno = new RetornoJson<TipoLancamentoDTO>()
                {
                    Resultado = lstRetorno,
                };

                return Sucesso(retorno);
            }
        }

        [HttpPost]
        [Autorizacao]
        public ActionResult Inserir(TipoLancamentoDTO registro)
        {
            using (TipoLancamentoService service = new TipoLancamentoService())
            {
                var model = registro.ToEntity<TipoLancamento, TipoLancamentoDTO>(service.Conexao);
                service.Inserir(model);

                RetornoJson<TipoLancamentoDTO> retorno = new RetornoJson<TipoLancamentoDTO>()
                {
                    Mensagem = "Registro incluído com sucesso",
                };

                return Sucesso(retorno);
            }
        }

        [HttpPost]
        [Autorizacao]
        public ActionResult Alterar(TipoLancamentoDTO registro)
        {
            using (TipoLancamentoService service = new TipoLancamentoService())
            {
                var model = registro.ToEntity<TipoLancamento, TipoLancamentoDTO>(service.Conexao);
                service.Alterar(model);

                RetornoJson<TipoLancamentoDTO> retorno = new RetornoJson<TipoLancamentoDTO>()
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
            using (TipoLancamentoService service = new TipoLancamentoService())
            {
                service.Excluir(id);

                RetornoJson<TipoLancamentoDTO> retorno = new RetornoJson<TipoLancamentoDTO>()
                {
                    Mensagem = "Registro excluído com sucesso",
                };

                return Sucesso(retorno);
            }
        }
    }
}