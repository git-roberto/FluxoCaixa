using FluxoCaixa.Core.Infra;
using FluxoCaixa.DTO;
using FluxoCaixa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.Base;
using Utilidades.EntityHelpers;

namespace FluxoCaixa.Service
{
    public class LancamentoService : ServiceBase<Lancamento>
    {
        public LancamentoService() 
            : base(new Contexto())
        {
        }

        public List<ConsolidadoDiarioDTO> ConsolidadoDiario()
        {
            var lst = base.Listar();

            var consolidado = lst.GroupBy(x => x.DataLancamento).Select(x => new ConsolidadoDiarioDTO()
            {
                Data = x.Key,
                Valor = ((x.Where(y => y.IdTipoLancamento == 1).Select(y => y.Valor).DefaultIfEmpty(0).Sum()) - (x.Where(y => y.IdTipoLancamento == 2).Select(y => y.Valor).DefaultIfEmpty(0).Sum()))
            }).ToList();

            return consolidado;
        }
    }
}
