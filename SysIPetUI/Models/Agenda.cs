namespace SysIPetUI.Models
{
    public class Agenda
    {        
        public int Id { get; set; }        
        public int ClienteId { get; set; }        
        public string? Cliente { get; set; }        
        public int PrestadorId { get; set; }        
        public string? Prestador { get; set; }
        public DateTime Data_Agenda { get; set; }
        public DateTime? Data_Cancelamento { get; set; }
        public DateTime Data_Cadastro { get; set; }
        public List<ServicoAgenda>? Servicos { get; set; }

        //public Agenda()
        //{
        //    Servicos = new List<ServicoAgenda>();
        //    Prestador = new Prestador();
        //    Cliente = new Cliente();
        //}
    }
}
