using Pet.WebAPI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Model
{
    public class NovoCliente
    {
        //public NovoCliente(string? nomeCompleto, string? cpf, DateTime? dataNascimento, string? telefone1, bool whatsApp, string? telefone2, EnderecoCliente? endereco)
        //{
        //    NomeCompleto = nomeCompleto;
        //    CPF = cpf;
        //    DataNascimento = dataNascimento;
        //    Telefone1 = telefone1;
        //    WhatsApp = whatsApp;
        //    Telefone2 = telefone2;
        //    Endereco = endereco;
        //}

        public string? IdUsuario { get; set; }

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
