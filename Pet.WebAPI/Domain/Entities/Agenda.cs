using Newtonsoft.Json;
using Pet.WebAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet.WebAPI.Domain.Entities
{
    public class Agenda : IBaseEFObject
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Id_Cliente")]
        public int ClienteId { get; set; }

        [JsonIgnore]
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [JsonProperty(PropertyName = "Id_Prestador")]
        public int PrestadorId { get; set; }

        [JsonIgnore]
        [ForeignKey("PrestadorId")]
        public Prestador Prestador { get; set; }

        public DateTime Data_Agenda { get; set; }

        public DateTime? Data_Cancelamento { get; set; }

        public DateTime Data_Cadastro { get; set; }

        public List<ServicoAgenda> Servicos { get; set; }

        public Agenda()
        {
            Servicos = new List<ServicoAgenda>();
            Prestador = new Prestador();
            Cliente = new Cliente();
        }
    }
}
