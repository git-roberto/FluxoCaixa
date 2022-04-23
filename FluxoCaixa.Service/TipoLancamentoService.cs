using FluxoCaixa.Core.Infra;
using FluxoCaixa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.Base;

namespace FluxoCaixa.Service
{
    public class TipoLancamentoService : ServiceBase<TipoLancamento>
    {
        public TipoLancamentoService()
            : base(new Contexto())
        {
        }
    }
}
