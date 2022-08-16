using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string NomeCompleto { get; set; } = "";

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        [EmailAddress]
        public string EMail { get; set; } = "";
    }
}
