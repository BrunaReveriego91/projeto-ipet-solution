namespace Pet.WebAPI.Domain.Entities
{
    public class PrestadorMaps
    {
        public string? NomeCompleto { get; set; }
        public List<Servico> Servicos { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static PrestadorMaps CriaPrestadorMaps(string nomeCompleto, List<Servico> servicos, double latitude, double longitude)
        {
            return new PrestadorMaps { NomeCompleto = nomeCompleto, Servicos = servicos, Latitude = latitude, Longitude = longitude };
        }

    }
}
