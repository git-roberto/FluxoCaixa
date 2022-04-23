using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilidades.EntityHelpers
{
    public class ContextoBase : DbContext
    {
        public static string strConexao
        {
            get
            {
                var arquivoConexao = ConfigurationManager.AppSettings["arquivoConexao"];
                var pathArquivo = HttpContext.Current.Server.MapPath($"~/App_Data/{arquivoConexao}");

                if (string.IsNullOrWhiteSpace(pathArquivo))
                {
                    throw new Exception("Informe o caminho do arquivo de conexão do sistema");
                }
                return BuscarConexao.Conexao(pathArquivo);
            }
        }

        public ContextoBase()
            : base(strConexao)
        {
        }
    }
}
