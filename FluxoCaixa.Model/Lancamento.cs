using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Model
{
    [Table("Lancamento")]
    public class Lancamento
    {
        [Key]
        [Column("IdLancamento")]
        public int? Id { get; set; }

        [Column("Valor")]
        public decimal Valor { get; set; }

        [Column("DataLancamento")]
        public DateTime DataLancamento { get; set; }

        [Column("IdTipoLancamento")]
        public int IdTipoLancamento { get; set; }
        [ForeignKey("IdTipoLancamento")]
        public virtual TipoLancamento TipoLancamento { get; set; }
    }
}
