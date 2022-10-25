using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Domain.Model
{
    public class AlterarCliente
    {
        public string? NomeCompleto { get; set; }
        public string? CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Telefone1 { get; set; }
        public bool WhatsApp { get; set; }
        public string? Telefone2 { get; set; }
        public EnderecoCliente? Endereco { get; set; }
    }
}
