namespace SysIPetUI.Models
{
    public class PrestadorListItem
    {
        public int Id { get; set; }
        public string? Id_Prestador { get; set; }
        public string? NomeCompleto { get; set; }
        public string? CPF_CNPJ { get; set; }
        public string? Telefone { get; set; }
        public bool WhatsApp { get; set; }

        public int PrestadorId { get; set; }
        public string? Logradouro { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        public string? Referencia { get; set; }
        public int Numero { get; set; }
        public bool SemNumero { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? CEP { get; set; }
    }
}
