using Newtonsoft.Json;

namespace Pet.WebAPI.Domain.Model
{
    public class NovoUsuarioPrestador
    {
        public string Nome_Usuario { get; set; } = "";
        public string Senha { get; set; } = "";
        public string EMail { get; set; } = "";

        [JsonProperty(PropertyName = "dados_prestador")]
        public Prestador Dados_Prestador { get; set; }

        public NovoUsuarioPrestador()
        {
            Dados_Prestador = new Prestador();
        }

        public class Prestador
        {
            public string Nome_Completo { get; set; } = "";
            public string CPF_CNPJ { get; set; } = "";
            public string Telefone { get; set; } = "";
            public bool WhatsApp { get; set; }

            [JsonProperty(PropertyName = "enderecos_prestador")]
            public List<EnderecoPrestadorDto> Enderecos_Prestador { get; set; }

            public Prestador()
            {
                Enderecos_Prestador = new List<EnderecoPrestadorDto>();
            }

            [JsonObject(Description = "endereco_prestador")]
            public class EnderecoPrestadorDto
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
    }
}
