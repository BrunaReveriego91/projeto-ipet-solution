namespace Pet.WebAPI.Domain.Model
{
    /// <summary>
    /// Novo Endereço do Prestador
    /// </summary>
    public class NovoCliente
    {

        public string? NomeCompleto { get; set; }
        public string? CPF { get; set; }
        public string? Aniversario { get; set; }
        public string? Telefone1 { get; set; }
        public bool WhatsApp { get; set; }
        public string? Telefone2 { get; set; }
    }
}
