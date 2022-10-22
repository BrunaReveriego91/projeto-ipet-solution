using Pet.WebAPI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Model
{
    public class NovoCliente
    {
        public string? NomeCompleto { get; set; }
        public string? CPF { get; set; }
        public string DataNascimento { get; set; } = "";
        public string? Telefone1 { get; set; }
        public bool WhatsApp { get; set; }
        public string? Telefone2 { get; set; }
        public EnderecoNovoCliente? Endereco { get; set; }

        public class EnderecoNovoCliente
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
