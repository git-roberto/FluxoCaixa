using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.Json
{
    public class RetornoJson<T>
    {
        public T Resultado { get; set; }
        public int QtdRegistros { get; set; }
        public string Mensagem { get; set; }
    }
}
