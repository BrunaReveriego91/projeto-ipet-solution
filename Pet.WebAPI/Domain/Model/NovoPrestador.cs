namespace Pet.WebAPI.Domain.Model
{
    public class NovoPrestador
    {
        public string NomeCompleto { get; set; }
        public string CPF_CNPJ { get; set; }
        public string Telefone { get; set; }
        public bool WhatsApp { get; set; }

        public NovoPrestador()
        {
            NomeCompleto = "";
            CPF_CNPJ = "";
            Telefone = "";
            WhatsApp = false;
        }
    }
}
