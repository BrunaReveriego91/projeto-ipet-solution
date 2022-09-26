namespace SysIPetUI.Models
{
    public class PetsViewModel
    {        
        public int Id { get; set; }        
        public int ClienteId { get; set; }        
        public string? Cliente { get; set; }        
        public string? NomeCompleto { get; set; }        
        public string? TipoPet { get; set; }        
        public string? TamanhoPet { get; set; }        
        public double Peso { get; set; }        
        public string? Genero { get; set; }        
        public string? Cor { get; set; }        
        public DateTime? DataNascimento { get; set; }        
        public string? Raca { get; set; }
    }
}
