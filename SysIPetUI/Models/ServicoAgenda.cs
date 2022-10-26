using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SysIPetUI.Models
{
    public class ServicoAgenda
    {        
        public int Id { get; set; }
        public int AgendaId { get; set; }
        public int ServicoPrestadorId { get; set; }
        public string? ServicoPrestador { get; set; }
        public int EnderecoPrestadorId { get; set; }
        public string? EnderecoPrestador { get; set; }
        public string Mensagem_Cliente { get; set; } = "";
        public string Mensagem_Profissional_Executante { get; set; } = "";
        public float Valor_Desconto { get; set; }
        public DateTime? Data_Cancelamento { get; set; }
        public DateTime? Data_Conclusao { get; set; }
        public DateTime Data_Cadastro { get; set; }

        //public ServicoAgenda()
        //{
        //    ServicoPrestador = new ServicoPrestador();
        //    EnderecoPrestador = new EnderecoPrestador();
        //}
    }
}
