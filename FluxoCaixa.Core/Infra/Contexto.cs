using FluxoCaixa.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.EntityHelpers;

namespace FluxoCaixa.Core.Infra
{
    public class Contexto : ContextoBase
    {
        public Contexto()
            : base()
        {

        }
        public DbSet<TipoLancamento> TipoLancamento { get; set; }
        public DbSet<Lancamento> Lancamento { get; set; }
    }
}
