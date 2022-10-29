namespace SysIPetUI.Models
{
    public class ServicoPrestador
    {
        public int Id { get; set; }
        public int PrestadorId { get; set; }
        public string? Prestador { get; set; }
        public int ServicoId { get; set; }
        public string? Servico { get; set; }
        public bool Ativo { get; set; }
        public float Valor { get; set; }        
    }
}
