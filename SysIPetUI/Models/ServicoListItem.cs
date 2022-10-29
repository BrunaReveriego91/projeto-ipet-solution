namespace SysIPetUI.Models
{
    public class ServicoListItem
    {
        //Serviços
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get; set; } = false;

        //Serviço Prestador        
        public int PrestadorId { get; set; }
        //public string? Prestador { get; set; }
        public int ServicoId { get; set; }
        //public string? Servico { get; set; }
        public float Valor { get; set; }
    }
}
