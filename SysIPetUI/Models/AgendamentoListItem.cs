namespace SysIPetUI.Models
{
    public class AgendamentoListItem
    {
        public int Id { get; set; }
        public int AgendamentoId { get; set; }
        public DateTime? Data_Agendamento { get; set; }
        public string? Servico { get; set; }
        public string? Servico_Descricao { get; set; }
        public string? Nome_Prestador { get; set; }
        public string? Rua_Prestador { get; set; }
        public int Numero_Prestador { get; set; }
        public string? Bairro_Prestador { get; set; }
        public string? Cidade_Prestador { get; set; }
        public string? UF_Prestador { get; set; }
        public string? Telefone_Prestador { get; set; }
        public DateTime? Data_Cadastro_Prestador { get; set; }
        public float Valor_Servico { get; set; }
        public float Desconto_Servico { get; set; }
        public DateTime? Data_Conclusao { get; set; }
        public DateTime? Data_Cancelamento { get; set; }
        public string? Mensagem_Prestador { get; set; }
        public string? Nome_Cliente { get; set; }
        public string? CPF_Cliente { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Telefone_Cliente { get; set; }
        public DateTime? Data_Cadastro_Cliente { get; set; }
        public string? Nome_Pet { get; set; }
        public string? Tipo_Pet { get; set; }
        public string? Genero_Pet { get; set; }
        public string? Tamanho_Pet { get; set; }
        public string? Mensagem_Cliente { get; set; }
        public string? Rua_Cliente { get; set; }
        public int Numero_Cliente { get; set; }
        public string? Bairro_Cliente { get; set; }
        public string? Cidade_Cliente { get; set; }
        public string? UF_Cliente { get; set; }
    }
}
