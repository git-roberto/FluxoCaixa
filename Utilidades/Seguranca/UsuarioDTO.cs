using Utilidades.DTO;
using Utilidades.EntityHelpers;

namespace Utilidades.Seguranca
{
    public class UsuarioDTO : BaseDTO<Usuario, UsuarioDTO>
    {
        public UsuarioDTO()
        {

        }

        public UsuarioDTO(Usuario model)
        {
            Id = model.Id;
            Nome = model.Nome;
            Email = model.Email;
            Senha = model.Senha;
            Ativo = model.Ativo;
        }

        public override Usuario Modelo(ContextoBase conexao)
        {
            Usuario model = new Usuario();
            if (Id.HasValue)
            {
                model = conexao.Set<Usuario>().Find(Id);
            }

            model.Nome = Nome;
            model.Email = Email;
            model.Senha = Senha;
            model.Ativo = Ativo;

            return model;
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }

}
