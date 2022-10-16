namespace SysIPetUI.Models
{
    public class PetsViewModel
    {        
        public int Id { get; set; }        
        public int ClienteId { get; set; }
        public string? NomeCompleto { get; set; }
        public int TipoPet { get; set; }
        public int TamanhoPet { get; set; }
        public int Genero { get; set; }
        public double Peso { get; set; }
        public string? Cor { get; set; }        
        public DateTime? DataNascimento { get; set; }        
        public string? Raca { get; set; }
        
    }
}
