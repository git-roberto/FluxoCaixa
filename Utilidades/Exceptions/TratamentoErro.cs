using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilidades.Exceptions
{
    public class TratamentoErro
    {
        public static void ErrorController(HttpStatusCode statusCode, string mensagem)
        {
            throw new HttpException((int)statusCode, mensagem);
        }
    }
}
