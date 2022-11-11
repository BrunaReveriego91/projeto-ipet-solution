namespace SysIPetUI.Models
{
    public class EstabelecimentoViewModel
    {
        public EstabelecimentoViewModel(string? nomeCompleto, List<ServicoListItem> servicos, double latitude, double longitude)
        {
            NomeCompleto = nomeCompleto;
            Servicos = servicos;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string? NomeCompleto { get; set; }
        public List<ServicoListItem> Servicos { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }

    public class EstabelecimentosLists
    {
        public IEnumerable<EstabelecimentoViewModel> EstabelecimentosList { get; set; }
    }
}
