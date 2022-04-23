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

namespace FluxoCaixa.API.Controllers
{
    public class LancamentoController : BaseController
    {
        public ActionResult Index()
        {
            using (LancamentoService service = new LancamentoService())
            {
                var lst = service.Listar();

                var lstRetorno = lst.ToList().Select(x => x.ToDTO<Lancamento, LancamentoDTO>()).ToList();

                RetornoJson<List<LancamentoDTO>> retorno = new RetornoJson<List<LancamentoDTO>>()
                {
                    Resultado = lstRetorno,
                    QtdRegistros = lst.Count(),
                };

                return Sucesso(retorno);
            }
        }

        public ActionResult Buscar(int id)
        {
            using (LancamentoService service = new LancamentoService())
            {
                var lstRetorno = service.Buscar(id).ToDTO<Lancamento, LancamentoDTO>();

                RetornoJson<LancamentoDTO> retorno = new RetornoJson<LancamentoDTO>()
                {
                    Resultado = lstRetorno,
                };

                return Sucesso(retorno);
            }
        }

        [HttpPost]
        public ActionResult Inserir(LancamentoDTO registro)
        {
            using (LancamentoService service = new LancamentoService())
            {
                var model = registro.ToEntity<Lancamento, LancamentoDTO>(service.Conexao);
                service.Inserir(model);

                RetornoJson<LancamentoDTO> retorno = new RetornoJson<LancamentoDTO>()
                {
                    Mensagem = "Registro incluído com sucesso",
                };

                return Sucesso(retorno);
            }
        }

        [HttpPost]
        public ActionResult Alterar(LancamentoDTO registro)
        {
            using (LancamentoService service = new LancamentoService())
            {
                var model = registro.ToEntity<Lancamento, LancamentoDTO>(service.Conexao);
                service.Alterar(model);

                RetornoJson<LancamentoDTO> retorno = new RetornoJson<LancamentoDTO>()
                {
                    Mensagem = "Registro alterado com sucesso",
                };

                return Sucesso(retorno);
            }
        }

        [HttpPost]
        public ActionResult Excluir(int id)
        {
            using (LancamentoService service = new LancamentoService())
            {
                service.Excluir(id);

                RetornoJson<LancamentoDTO> retorno = new RetornoJson<LancamentoDTO>()
                {
                    Mensagem = "Registro excluído com sucesso",
                };

                return Sucesso(retorno);
            }
        }

        public ActionResult ConsolidadoDiario()
        {
            using (LancamentoService service = new LancamentoService())
            {
                var lstRetorno = service.ConsolidadoDiario();

                RetornoJson<List<ConsolidadoDiarioDTO>> retorno = new RetornoJson<List<ConsolidadoDiarioDTO>>()
                {
                    Resultado = lstRetorno,
                    QtdRegistros = lstRetorno.Count(),
                };

                return Sucesso(retorno);
            }
        }
    }
}