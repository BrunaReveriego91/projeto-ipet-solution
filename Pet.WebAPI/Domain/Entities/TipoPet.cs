using Pet.WebAPI.Domain.Entities.Enums;

namespace Pet.WebAPI.Domain.Entities
{
    public class TipoPet
    {
        public EnumTipoPet TipoPetId { get; set; }
        public string? Descricao { get; set; }
    }
}
