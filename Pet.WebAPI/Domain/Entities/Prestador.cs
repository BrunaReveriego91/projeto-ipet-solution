using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Entities
{
    public class Prestador : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string? NomeCompleto { get; set; } 

        [Required]
        [StringLength(14)]
        public string? CPF_CNPJ { get; set; }

        public List<EnderecoPrestador> Enderecos { get; set; }
        public List<ServicoPrestador> Servicos { get; set; }

        public Prestador()
        {
            Enderecos = new List<EnderecoPrestador>();
            Servicos = new List<ServicoPrestador>();
        }
    }
}
