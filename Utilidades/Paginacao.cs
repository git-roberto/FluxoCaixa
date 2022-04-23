using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class Paginacao
    {
        public int Pagina { get; set; }
        public int Quantidade { get; set; }
        public string Filtro { get; set; }

        public Ordenacao Ordenacao { get; set; }
    }

    public class Ordenacao
    {
        public string Coluna { get; set; }
        public bool Sentido { get; set; }
    }
}
