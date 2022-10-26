namespace SysIPetUI.Models
{
    public class Prestador
    {
        public int Id { get; set; }
        public string? NomeCompleto { get; set; }
        public string? CPF_CNPJ { get; set; }
        public string Telefone { get; set; } = "";
        public bool WhatsApp { get; set; } = false;

        public List<EnderecoPrestador>? Enderecos { get; set; }
        public List<ServicoPrestador>? Servicos { get; set; }

        //public Prestador()
        //{
        //    Enderecos = new List<EnderecoPrestador>();
        //    Servicos = new List<ServicoPrestador>();
        //}
    }
}
