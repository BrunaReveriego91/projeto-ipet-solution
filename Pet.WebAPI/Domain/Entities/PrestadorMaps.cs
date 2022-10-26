namespace Pet.WebAPI.Domain.Entities
{
    public class PrestadorMaps
    {
        public string? NomeCompleto { get; set; }
        public List<ServicoPrestador> Servicos { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
