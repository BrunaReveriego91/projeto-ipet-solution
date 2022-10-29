namespace SysIPetUI.Models
{
    public class ServicoViewModel
    {
        ////Serviços
        //public int Id { get; set; }
        //public string? Nome { get; set; }
        //public string? Descricao { get; set; }
        //public bool Ativo { get; set; } = false;

        ////Serviço Prestador        
        //public int PrestadorId { get; set; }
        //public string? Prestador { get; set; }
        //public int ServicoId { get; set; }
        //public string? Servico { get; set; }
        //public float Valor { get; set; }

        public int Id { get; set; }

        //Construtor:
        public ServicoViewModel()
        {
            this.ServicoList = new List<ServicoListItem>();
        }

        //Get Listas
        public List<ServicoListItem>? ServicoList { get; set; }

    }
        
}
