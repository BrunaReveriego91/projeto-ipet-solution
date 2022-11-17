namespace SysIPetUI.Models
{
    public class EstabelecimentoViewModel
    {
        public EstabelecimentoViewModel(string? nomeCompleto, List<ServicoListItem> servicos, string latitude, string longitude)
        {
            NomeCompleto = nomeCompleto;
            Servicos = servicos;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string? NomeCompleto { get; set; }
        public List<ServicoListItem> Servicos { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }

    public class EstabelecimentosLists
    {
        public IEnumerable<EstabelecimentoViewModel> EstabelecimentosList { get; set; }
    }
}
