using Pet.WebAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Entities
{
    public class Cliente : BaseEFObject
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string? IdUsuario { get; set; }

        [Required]
        [StringLength(256)]
        public string? NomeCompleto { get; set; }

        [Required]
        [StringLength(15)]
        public string? CPF { get; set; }

        [Required]
        [StringLength(59)]
        public string? Email { get; set; }

        [StringLength(20)]
        public DateTime? DataNascimento { get; set; }

        [Required]
        [StringLength(15)]
        public string? Telefone1 { get; set; }

        [Required]
        public bool WhatsApp { get; set; }

        [StringLength(15)]
        public string? Telefone2 { get; set; }

        public EnderecoCliente? Endereco { get; set; } 

    }
}
