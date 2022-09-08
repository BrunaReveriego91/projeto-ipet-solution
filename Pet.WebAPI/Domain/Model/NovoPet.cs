using Pet.WebAPI.Domain.Entities.Enums;

namespace Pet.WebAPI.Domain.Model
{
    public class NovoPet
    {
        public NovoPet(int clienteId, string? nomeCompleto, EnumTipoPet? tipoPet, EnumTamanhoPet? tamanhoPet, double peso, EnumGenero? genero, string? cor, string? dataNascimento, string? raca)
        {
            ClienteId = clienteId;
            NomeCompleto = nomeCompleto;
            TipoPet = tipoPet;
            TamanhoPet = tamanhoPet;
            Peso = peso;
            Genero = genero;
            Cor = cor;
            DataNascimento = dataNascimento;
            Raca = raca;
        }

        public int ClienteId { get; set; }

        public string? NomeCompleto { get; set; }

        public EnumTipoPet? TipoPet { get; set; }

        public EnumTamanhoPet? TamanhoPet { get; set; }

        public double Peso { get; set; }
       public EnumGenero? Genero { get; set; }

        public string? Cor { get; set; }

        public string? DataNascimento { get; set; }

        public string? Raca { get; set; }
    }
}
