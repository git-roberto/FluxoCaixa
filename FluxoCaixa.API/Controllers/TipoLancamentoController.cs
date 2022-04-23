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
    public class TipoLancamentoController : BaseController
    {
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
    }
}