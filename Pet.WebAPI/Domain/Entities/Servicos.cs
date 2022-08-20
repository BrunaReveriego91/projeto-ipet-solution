﻿using System.ComponentModel.DataAnnotations;

namespace Pet.WebAPI.Domain.Entities
{
    /// <summary>
    /// Serviços
    /// </summary>
    public class Servico : IBaseRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = "";

        [Required]
        [StringLength(256)]
        public string Descricao { get; set; } = "";
        public DateTime Data_Cadastro { get; set; }
    }
}