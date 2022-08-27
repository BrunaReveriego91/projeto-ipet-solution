namespace Pet.WebAPI.Domain.Entities
{
    public abstract class BaseEntity : IBaseRecord
    {
        public string Telefone { get; set; } = "";
        public bool WhatsApp { get; set; } = false;
        public DateTime Data_Cadastro { get; set; }
    }
}
