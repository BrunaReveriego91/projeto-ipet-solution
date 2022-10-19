using Newtonsoft.Json;
using Pet.WebAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet.WebAPI.Domain.Entities
{
    public class EnderecoPrestador : BaseEFObject
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Id_Prestador")]
        [ForeignKey(nameof(Prestador))]
        public int PrestadorId { get; set; }

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
        [StringLength(10)]
        public string? CEP { get; set; }

        [Required]
        [StringLength(15)]  // 11-96398-1794
        public string Telefone { get; set; } = "";

        public bool WhatsApp { get; set; } = false;
    }
}
