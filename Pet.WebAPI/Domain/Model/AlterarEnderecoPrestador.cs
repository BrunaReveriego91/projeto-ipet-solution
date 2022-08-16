namespace Pet.WebAPI.Domain.Model
{
    public class AlterarEnderecoPrestador
    {
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
