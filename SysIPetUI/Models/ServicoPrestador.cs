using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysIPetUI.Models
{
    public class ServicoPrestador
    {
        public int Id { get; set; }
        public int PrestadorId { get; set; }
        public string? Prestador { get; set; }
        public int ServicoId { get; set; }
        public string? Servico { get; set; }
        public bool Ativo { get; set; } = false;
        public float Valor { get; set; }

        //public ServicoPrestador()
        //{
        //    Servico = new Servico();
        //    Prestador = new Prestador();
        //}

        //public ServicoPrestador(int prestador_id, int servico_id, bool ativo) : this()
        //{
        //    PrestadorId = prestador_id;
        //    ServicoId = servico_id;
        //    Ativo = ativo;
        //}
    }
}
