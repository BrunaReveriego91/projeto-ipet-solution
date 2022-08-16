using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Model
{
    /// <summary>
    /// Novo Endereço do Prestador
    /// </summary>
    public class NovoEnderecoPrestador
    {
        public int PrestadorId { get; set; }
        public string Logradouro { get; set; } = "";

        public string Bairro { get; set; } = "";

        public string Complemento { get; set; } = "";

        public string Referencia { get; set; } = "";

        public int Numero { get; set; } = 0;

        public bool SemNumero { get; set; } = false;

        public string Cidade { get; set; } = "";

        public string UF { get; set; } = "";

        public string CEP { get; set; } = "";
    }
}
