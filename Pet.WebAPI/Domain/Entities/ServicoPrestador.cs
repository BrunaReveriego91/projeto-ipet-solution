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

        /// <summary>
        /// Indica se o Serviço está ativado para o Prestador.
        /// </summary>
        public bool Ativo { get; set; } = false;

        public DateTime Data_Cadastro { get; set; }

        public ServicoPrestador()
        {

        }

        public ServicoPrestador(int prestador_id, int servico_id, bool ativo) : base()
        {
            PrestadorId = prestador_id;
            ServicoId = servico_id;
            Ativo = ativo;
        }
    }
}
