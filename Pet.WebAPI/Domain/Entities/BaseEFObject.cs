using System.ComponentModel.DataAnnotations;
using Pet.WebAPI.Interfaces;

namespace Pet.WebAPI.Domain.Entities
{
    public abstract class BaseEFObject : IBaseEFObject
    {
        [Required]
        public DateTime Data_Cadastro { get; set; }
    }
}
