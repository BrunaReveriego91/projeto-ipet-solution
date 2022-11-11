namespace Pet.WebAPI.Domain.Entities
{
    public class PrestadorMaps
    {
        public string? NomeCompleto { get; set; }
        public List<ServicoMaps> Servicos { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static PrestadorMaps CriaPrestadorMaps(string nomeCompleto, List<ServicoMaps> servicos, double latitude, double longitude)
        {
            return new PrestadorMaps { NomeCompleto = nomeCompleto, Servicos = servicos, Latitude = latitude, Longitude = longitude };
        }

    }
}
