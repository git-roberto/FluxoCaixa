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
    public class TipoLancamentoDTO : BaseDTO<TipoLancamento, TipoLancamentoDTO>
    {
        public TipoLancamentoDTO()
        {

        }

        public TipoLancamentoDTO(TipoLancamento model)
        {
            Id = model.Id;
            Nome = model.Nome;
            Ativo = model.Ativo;
        }

        public override TipoLancamento Modelo(ContextoBase conexao)
        {
            TipoLancamento model = new TipoLancamento();
            if (Id.HasValue)
            {
                model = conexao.Set<TipoLancamento>().Find(Id);
            }

            model.Nome = Nome;
            model.Ativo = Ativo;

            return model;
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
