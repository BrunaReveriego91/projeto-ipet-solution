using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Entities
{
    /// <summary>
    /// Classe que representa um Usuário do Sistema
    /// </summary>
    /// <remarks>Representa um Usuário ou Prestador</remarks>
    public class Usuario : BaseEFObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        [Required]
        [EmailAddress]
        public string EMail { get; set; } = "";
    }
}
