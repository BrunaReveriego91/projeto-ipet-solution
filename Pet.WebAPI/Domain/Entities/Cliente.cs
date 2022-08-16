﻿using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string? NomeCompleto { get; set; }
        [Required]
        [StringLength(15)]
        public string? CPF { get; set; }
        [StringLength(20)]
        public string? Aniversario { get; set; }
        [Required]
        [StringLength(15)]
        public string? Telefone1 { get; set; }
        [Required]
        public bool WhatsApp { get; set; }

        [StringLength(15)]
        public string? Telefone2 { get; set; }
    }
}
