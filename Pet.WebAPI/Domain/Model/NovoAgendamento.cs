namespace Pet.WebAPI.Domain.Model
{
    public class NovoAgendamento
    {
        public int Id_Cliente { get; set; }
        public int Id_Prestador { get; set; }
        public DateTime Data_Agendamento { get; set; }
        public List<Servico_Agendamento> Servicos { get; set; }

        public NovoAgendamento()
        {
            Servicos = new List<Servico_Agendamento>();
        }

        public class Servico_Agendamento
        {
            public int Id_Servico_Prestador { get; set; }
            public int Id_Endereco_Prestador { get; set; }
            public string Mensagem_Cliente { get; set; }

            public Servico_Agendamento()
            {
                Id_Servico_Prestador = 0;
                Id_Endereco_Prestador = 0;
                Mensagem_Cliente = "";
            }
        }
    }
}
