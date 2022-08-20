using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet.WebAPI.Domain.Entities
{
    /// <summary>
    /// Relacionamento entre o Usuário e o Prestador
    /// </summary>
    public class UsuarioPrestador
    {
        [Key]
        public int Id { get; internal set; }

        [ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }

        [ForeignKey(nameof(Prestador))]
        public int PrestadorId { get; set; }

        public UsuarioPrestador(int usuarioId, int prestadorId)
        {
            UsuarioId = usuarioId;
            PrestadorId = prestadorId;
        }
    }
}
