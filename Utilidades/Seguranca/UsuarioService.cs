using System;
using Utilidades.Base;
using Utilidades.EntityHelpers;
using Utilidades.JWT;

namespace Utilidades.Seguranca
{
    public class UsuarioService : ServiceBase<Usuario>
    {
        public UsuarioService()
            : base(new ContextoBase())
        {
        }

        public string EfetuarLogin(string email, string senha)
        {
            try
            {
                senha = Criptografia.CriptografarSHA256(senha);

                var usuario = Buscar(x => x.Email.ToLower().Trim() == email.ToLower().Trim() && x.Senha == senha && x.Ativo == true);

                if (usuario == null)
                {
                    throw new Exception("O usuário e/ou senha inválidos.");
                }

                string retorno = JWTService.GerarTokenJWT(usuario);

                return retorno;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
