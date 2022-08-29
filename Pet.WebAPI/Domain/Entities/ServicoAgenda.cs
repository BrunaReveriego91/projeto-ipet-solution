using Pet.WebAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet.WebAPI.Domain.Entities
{
    public class ServicoAgenda : IBaseEFObject
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Agenda))]
        public int AgendaId { get; set; }
        
        public int ServicoId { get; set; }

        [ForeignKey("ServicoId")]
        public Servico Servico { get; set; }

        public int EnderecoPrestadorId { get; set; }

        [ForeignKey("EnderecoPrestadorId")]
        public EnderecoPrestador EnderecoPrestador { get; set; }

        /// <summary>
        /// Mensagem do Cliente para o Prestador/Profissional.
        /// </summary>
        [StringLength(4000)]
        public string Mensagem_Cliente { get; set; } = "";

        /// <summary>
        /// Mensagem do Prestador/Profissional para o Cliente.
        /// </summary>
        [StringLength(4000)]
        public string Mensagem_Profissional_Executante { get; set; } = "";

        /// <summary>
        /// Data do Cancelamento do Serviço
        /// </summary>
        public DateTime Data_Cancelamento { get; set; }

        /// <summary>
        /// Data de Conclusão do Serviço.
        /// </summary>
        public DateTime Data_Conclusao { get; set; }

        public DateTime Data_Cadastro { get; set; }

        public ServicoAgenda()
        {
            Servico = new Servico();
            EnderecoPrestador = new EnderecoPrestador();
        }
    }
}
