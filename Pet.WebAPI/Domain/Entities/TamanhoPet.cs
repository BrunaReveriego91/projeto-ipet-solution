using Pet.WebAPI.Domain.Entities.Enums;

namespace Pet.WebAPI.Domain.Entities
{
    public class TamanhoPet
    {
        public EnumTamanhoPet TamanhoPetId { get; set; }
        public string? Descricao { get; set; }
    }
}
