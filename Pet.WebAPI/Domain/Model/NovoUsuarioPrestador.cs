using Newtonsoft.Json;

namespace Pet.WebAPI.Domain.Model
{
    public class NovoUsuarioPrestador
    {
        public string Nome_Usuario { get; set; } = "";
        public string Senha { get; set; } = "";
        public string EMail { get; set; } = "";

        [JsonProperty(PropertyName = "dados_prestador")]
        public Info_Prestador Dados_Prestador { get; set; }

        public NovoUsuarioPrestador()
        {
            Dados_Prestador = new Info_Prestador();
        }

        public class Info_Prestador
        {
            public string Nome_Completo { get; set; }
            public string CPF_CNPJ { get; set; }
            public string Telefone { get; set; }
            public bool WhatsApp { get; set; }

            public Info_Prestador()
            {
                Nome_Completo = "";
                CPF_CNPJ = "";
                Telefone = "";
                WhatsApp = false;
            }
        }
    }
}
