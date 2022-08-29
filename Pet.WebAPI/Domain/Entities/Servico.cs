using Pet.WebAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Entities
{
    /// <summary>
    /// Serviços
    /// </summary>
    public class Servico : BaseEFObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = "";

        [Required]
        [StringLength(256)]
        public string Descricao { get; set; } = "";

        public bool Ativo { get; set; } = false;

        public Servico()
        {
            Nome = "";
            Descricao = "";
            Ativo = false;
        }

        public Servico(string nome, string descricao, bool ativo) : this()
        {
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
        }
    }
}
