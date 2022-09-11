namespace Pet.WebAPI.Domain.Model
{
    public class AlterarPet
    {
        public int ClienteId { get; set; }

        public string? NomeCompleto { get; set; }

        public double Peso { get; set; }

        public DateTime? DataNascimento { get; set; }

    }
}
