namespace Pet.WebAPI.Domain.Entities
{
    public class Maps
    {
        public int PrestadorId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Maps(int prestadorId, double latitude, double longitude)
        {
            PrestadorId = prestadorId;
            Latitude = latitude;
            Longitude = longitude;
        }

        public class MapsLists
        {
            public IEnumerable<Maps> MapsList { get; set; }
        }

    }
}
