using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Model
{
    [Table("TipoLancamento")]
    public class TipoLancamento
    {
        [Key]
        [Column("IdTipoLancamento")]
        public int? Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }
    }
}
