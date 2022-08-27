namespace Pet.WebAPI.Domain.Model
{
    public class NovoServicoPrestador
    {
        public int PrestadorId { get; set; }
        public int ServicoId { get; set; }
        public bool Ativo { get; set; }
    }
}
