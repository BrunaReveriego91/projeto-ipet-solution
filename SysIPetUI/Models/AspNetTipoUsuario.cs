using System.ComponentModel.DataAnnotations;

namespace SysIPetUI.Models
{
    public class AspNetTipoUsuario
    {
        public int id { get; set; }
        [StringLength(450)]
        public string? UserId { get; set; }
        public bool? TipoUsuario { get; set; }
        public bool? AceiteTermo { get; set; }
    }
}