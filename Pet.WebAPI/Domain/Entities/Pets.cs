using Pet.WebAPI.Domain.Entities.Enums;
using Pet.WebAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet.WebAPI.Domain.Entities
{
    public class Pets : BaseEFObject
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Cliente))]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(256)]
        public string? NomeCompleto { get; set; }

        [Required]
        public EnumTipoPet? TipoPet { get; set; }

        [Required]
        public EnumTamanhoPet? TamanhoPet { get; set; }

        [Required]
        public double Peso { get; set; }

        [Required]
        public EnumGenero? Genero { get; set; }

        [StringLength(50)]
        public string? Cor { get; set; }

        [StringLength(20)]
        public string? Aniversario { get; set; }

        [StringLength(50)]
        public string? Raca { get; set; }
    }
}