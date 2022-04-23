using FluxoCaixa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.DTO;
using Utilidades.EntityHelpers;

namespace FluxoCaixa.DTO
{
    public class LancamentoDTO : BaseDTO<Lancamento, LancamentoDTO>
    {
        public LancamentoDTO()
        {

        }

        public LancamentoDTO(Lancamento model)
        {
            Id = model.Id;
            Valor = model.Valor;
            DataLancamento = model.DataLancamento;
            IdTipoLancamento = model.IdTipoLancamento;

            TipoLancamento = model.TipoLancamento?.ToDTO<TipoLancamento, TipoLancamentoDTO>(); ;
        }

        public override Lancamento Modelo(ContextoBase conexao)
        {
            Lancamento model = new Lancamento();
            if (Id.HasValue)
            {
                model = conexao.Set<Lancamento>().Find(Id);
            }

            model.Valor = Valor;
            model.DataLancamento = DataLancamento;
            model.IdTipoLancamento = IdTipoLancamento;

            model.IdTipoLancamento = IdTipoLancamento;

            return model;
        }

        public int? Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public int IdTipoLancamento { get; set; }
        public virtual TipoLancamentoDTO TipoLancamento { get; set; }
    }
}
