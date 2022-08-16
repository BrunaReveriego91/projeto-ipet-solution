using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet.WebAPI.Domain.Entities
{
    public class EnderecoCliente
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Cliente))]
        public int ClienteId { get; set; }

        [Required]
        [StringLength(256)]
        public string? Logradouro { get; set; }

        [StringLength(256)]
        public string? Bairro { get; set; }

        [StringLength(256)]
        public string? Complemento { get; set; }

        [StringLength(256)]
        public string? Referencia { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public bool SemNumero { get; set; }

        [Required]
        [StringLength(256)]
        public string? Cidade { get; set; }

        [Required]
        [StringLength(2)]
        public string? UF { get; set; }

        [Required]
        [StringLength(8)]
        public string? CEP { get; set; }
    }
}
