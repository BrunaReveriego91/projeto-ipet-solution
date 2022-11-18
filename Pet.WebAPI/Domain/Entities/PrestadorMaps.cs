namespace Pet.WebAPI.Domain.Entities
{
    public class PrestadorMaps
    {
        public int IdPrestador { get; set; }
        public string? NomeCompleto { get; set; }
        public List<ServicoMaps> Servicos { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static PrestadorMaps CriaPrestadorMaps(int idPrestador, string nomeCompleto, List<ServicoMaps> servicos, double latitude, double longitude)
        {
            return new PrestadorMaps { IdPrestador = idPrestador, NomeCompleto = nomeCompleto, Servicos = servicos, Latitude = latitude, Longitude = longitude };
        }

    }
}
