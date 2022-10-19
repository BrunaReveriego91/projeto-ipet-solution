namespace SysIPetUI.Models
{
    public class PetsListItem
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string? NomeCompleto { get; set; }
        public int TipoPetId{ get; set; }
        public string? TipoPetNome { get; set; }
        public int TamanhoPetId { get; set; }
        public string? TamanhoPetNome { get; set; }
        public int GeneroPetId { get; set; }
        public string? GeneroPetNome { get; set; }
        public double Peso { get; set; }
        public string? Cor { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Raca { get; set; }
    }
}
