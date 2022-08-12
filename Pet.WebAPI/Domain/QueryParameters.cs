using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain
{
    public class QueryParameters
    {
        [Required]
        public int Id { get; set; }
    }
}
