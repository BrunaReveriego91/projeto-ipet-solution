using Pet.WebAPI.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet.WebAPI.Domain.Entities
{
    public class Pets : BaseEFObject
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

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
        public DateTime? DataNascimento { get; set; }

        [StringLength(50)]
        public string? Raca { get; set; }
    }
}