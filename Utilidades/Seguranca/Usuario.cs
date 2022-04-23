using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.Seguranca
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("IdUsuario")]
        public int? Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Email")]
        public string Email { get; set; }
        
        [Column("Senha")]
        public string Senha { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }
    }
}
