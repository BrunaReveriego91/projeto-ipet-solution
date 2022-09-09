using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet.WebAPI.Domain.Entities
{
    public class ServicoPrestador : BaseEFObject
    {
        [Key]
        public int Id { get; set; }

        public int PrestadorId { get; set; }

        [ForeignKey("PrestadorId")]
        public Prestador Prestador { get; set; }

        public int ServicoId { get; set; }

        [ForeignKey("ServicoId")]
        public Servico Servico { get; set; }

        /// <summary>
        /// Indica se o Serviço está ativado para o Prestador.
        /// </summary>
        public bool Ativo { get; set; } = false;

        /// <summary>
        /// Valor do Serviço
        /// </summary>
        [Column(TypeName = "money")]
        public float Valor { get; set; }

        public ServicoPrestador()
        {
            Servico = new Servico();
            Prestador = new Prestador();
        }

        public ServicoPrestador(int prestador_id, int servico_id, bool ativo) : this()
        {
            PrestadorId = prestador_id;
            ServicoId = servico_id;
            Ativo = ativo;
        }
    }
}
