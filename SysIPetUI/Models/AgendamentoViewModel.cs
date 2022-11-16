namespace SysIPetUI.Models
{
    public class AgendamentoViewModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int Id_Cliente { get; set; }
        public string? Cliente { get; set; }
        public int PrestadorId { get; set; }
        //public string? Prestador { get; set; }
        public DateTime? Data_Agendamento { get; set; }
        public DateTime? Data_Cancelamento { get; set; }
        public DateTime? Data_Conclusao { get; set; }
        public string? EnderecoPrestador { get; set; }
        //public string? Mensagem_Cliente { get; set; }
        public string? Mensagem_Profissional_Executante { get; set; }
        public float Valor { get; set; }
        public float Valor_Desconto { get; set; }

        //--------------------------------------------------------------------------
        //Prestadores        
        public int Id_Prestador { get; set; }
        public string? NomeCompleto { get; set; }
        public string? CPF_CNPJ { get; set; }
        public string? Telefone { get; set; }
        public bool WhatsApp { get; set; }

        //Endereço Prestadores
        public int IdEnderecoPrestador { get; set; }        
        public string? Logradouro { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        public string? Referencia { get; set; }
        public int Numero { get; set; }
        public bool SemNumero { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? CEP { get; set; }

        //Construtor:
        public AgendamentoViewModel()
        {
            this.PrestadorList = new List<PrestadorListItem>();
            this.Servicos = new List<ServicoListItem>();
            this.AgendamentoList = new List<AgendamentoListItem>();            
        }

        //Get Listas
        public List<PrestadorListItem>? PrestadorList { get; set; }
        public List<ServicoListItem>? Servicos { get; set; }        
        public List<AgendamentoListItem>? AgendamentoList { get; set; }

        public Prestador? Prestador { get; set; }
        public EnderecoPrestador? Endereco { get; set; }
        
    }
}
