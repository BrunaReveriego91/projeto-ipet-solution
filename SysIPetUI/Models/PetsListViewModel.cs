namespace SysIPetUI.Models
{
    public class PetsListViewModel
    {
        public int Id { get; set; }

        //Construtor:
        public PetsListViewModel()
        {
            //DropdownList
            this.TipoPetList = new List<TipoPet>();
            this.TamanhoPetList = new List<TamanhoPet>();
            this.GeneroPetList = new List<GeneroPet>();
            this.PetsList = new List<PetsListItem>();            

            //Instância de PetsViewModel
            this.PetsViewModelInstancia = new PetsViewModel();
        }              

        //Get Listas
        public List<TipoPet>? TipoPetList { get; set; }
        public List<TamanhoPet>? TamanhoPetList { get; set; }
        public List<GeneroPet>? GeneroPetList { get; set; }
        public List<PetsListItem>? PetsList { get; set; }        

        //Get Instância
        public PetsViewModel? PetsViewModelInstancia { get; set; }
    }
}
