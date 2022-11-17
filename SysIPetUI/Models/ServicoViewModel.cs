namespace SysIPetUI.Models
{
    public class ServicoViewModel
    {
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
