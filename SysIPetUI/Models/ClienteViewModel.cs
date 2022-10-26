namespace SysIPetUI.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string? IdUsuario { get; set; }
        public string? NomeCompleto { get; set; }
        public string? CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Telefone1 { get; set; }
        public bool WhatsApp { get; set; }
        public string? Telefone2 { get; set; }
        public EnderecoClienteViewModel? Endereco { get; set; }
    }
}
