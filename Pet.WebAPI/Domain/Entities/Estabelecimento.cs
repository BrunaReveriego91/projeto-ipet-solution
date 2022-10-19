namespace Pet.WebAPI.Domain.Entities
{
    public class Estabelecimento
    {
        public string? NomeCompleto { get; set; }
        public string? Telefone { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<ServicoPrestador> Servicos { get; set; }


    }
}
