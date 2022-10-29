namespace SysIPetUI.Models
{
    public class AgendamentoViewModel
    {
        public int Id { get; set; }
        public NovoAgendamento? Agendamento { get; set; }
        public Agenda? Agenda { get; set; }
        public ServicoAgenda? ServicoAgenda { get; set; }
        public Servico? Servico { get; set; }
        public PrestadorViewModel? Prestador { get; set; }
        public EnderecoPrestador? EnderecoPrestador { get; set; }
        public ServicoPrestador? ServicoPrestador { get; set; }
    }
}
