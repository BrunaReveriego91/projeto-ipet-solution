namespace SysIPetUI.Models
{
    public class EstabelecimentoViewModel
    {
        public EstabelecimentoViewModel(int locationId, string title, string description, double latitude, double longitude)
        {
            LocationId = locationId;
            Title = title;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
        }

        public int LocationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }

    public class EstabelecimentosLists
    {
        public IEnumerable<EstabelecimentoViewModel> EstabelecimentosList { get; set; }
    }
}
