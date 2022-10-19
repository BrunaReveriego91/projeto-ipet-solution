using Pet.WebAPI.Domain.Entities.Enums;

namespace Pet.WebAPI.Domain.Model
{
    public class AlterarPet
    {
        public int ClienteId { get; set; }
        public string? NomeCompleto { get; set; }
        public double Peso { get; set; }
        public DateTime? DataNascimento { get; set; }

        //Bruna, adicionei os Demais campos do Alterar pois o usuário pode precisar alterá-los
        public EnumTipoPet? TipoPet { get; set; }
        public EnumTamanhoPet? TamanhoPet { get; set; }
        public EnumGenero? Genero { get; set; }
        public string? Cor { get; set; }
        public string? Raca { get; set; }

    }
}
