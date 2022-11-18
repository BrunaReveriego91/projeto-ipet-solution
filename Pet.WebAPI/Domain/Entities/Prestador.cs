using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Entities
{
    public class Prestador : BaseEFObject
    {
        /// <summary>
        /// Chave Id Prestador.
        /// </summary>
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string? NomeCompleto { get; set; } 

        [Required]
        [StringLength(14)]  //345.999.125.44
        public string? CPF_CNPJ { get; set; }

        [Required]
        [StringLength(15)]  // 11-96398-1794
        public string Telefone { get; set; } = "";

        public bool WhatsApp { get; set; } = false;

        /// <summary>
        /// Chave com o Login do AspNetUsers.
        /// </summary>
        [Required]
        public string? Id_Prestador { get; set; }

        public List<EnderecoPrestador> Enderecos { get; set; }
        public List<ServicoPrestador> Servicos { get; set; }

        public Prestador()
        {
            Enderecos = new List<EnderecoPrestador>();
            Servicos = new List<ServicoPrestador>();
        }
    }
}
