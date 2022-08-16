using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet.WebAPI.Domain.Entities
{
    public class ServicoPrestador : IBaseRecord
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Prestador))]
        public int PrestadorId { get; set; }

        [ForeignKey(nameof(Servico))]
        public int ServicoId { get; set; }
        
        public DateTime Data_Cadastro { get; set; }
    }
}
