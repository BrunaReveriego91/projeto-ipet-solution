using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet.WebAPI.Domain.Entities
{
    /// <summary>
    /// Relacionamento entre o Usuário e o Prestador
    /// </summary>
    public class UsuarioPrestador : BaseEFObject
    {
        [Key]
        public int Id { get; internal set; }

        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        public int PrestadorId { get; set; }

        [ForeignKey("PrestadorId")]
        public Prestador Prestador { get; set; }

        public UsuarioPrestador()
        {
            Usuario = new Usuario();
            Prestador = new Prestador();
        }

        public UsuarioPrestador(int usuarioId, int prestadorId) : this()
        {
            UsuarioId = usuarioId;
            PrestadorId = prestadorId;
        }
    }
}
