using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.EntityHelpers
{
    public class BuscarConexao
    {
        public static string Conexao(string caminhoArquivo)
        {
            if (string.IsNullOrEmpty(caminhoArquivo))
            {
                throw new Exception($"Informe o caminho do arquivo de conexão");
            }

            if (!File.Exists(caminhoArquivo))
            {
                throw new Exception($"O arquivo de conexão não foi encontrado");
            }

            //Lê o aquivo criptografado de strings de conexão
            StreamReader sr = File.OpenText(caminhoArquivo);

            //Joga em uma string
            string linhas = sr.ReadToEnd();

            if (string.IsNullOrEmpty(linhas))
            {
                throw new Exception("Não existe informações no arquivo de conexão");
            }

            //Descriptografa a string
            linhas = Criptografia.DescriptografarAES(linhas);

            //Associa a um leitor de strings
            StringReader str = new StringReader(linhas);

            string strConexao = string.Empty;
            string linha = string.Empty;

            while ((linha = str.ReadLine()) != null)
            {
                strConexao = linha;
            }
            str.Close();
            return strConexao;
        }
    }
}
