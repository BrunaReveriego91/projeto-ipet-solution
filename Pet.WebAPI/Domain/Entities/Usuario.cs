using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string? NomeCompleto { get; set; }

        [Required]
        [StringLength(256)]
        public string? Senha { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(20)]
        public string? DataCadastro { get; set; }

    }
}
