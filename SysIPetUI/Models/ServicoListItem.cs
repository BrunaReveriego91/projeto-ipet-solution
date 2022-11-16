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
        public int Prestador_Id { get; set; }
        public string? Prestador { get; set; }
        public int Servico_Id { get; set; }
        public string? Servico { get; set; }
        public float Valor { get; set; }
        public int Id_Servico_Prestador { get; set; }
        public int Id_Endereco_Prestador { get; set; }
        public string? Mensagem_Cliente { get; set; }
    }
}
