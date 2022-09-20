﻿using Pet.WebAPI.Domain.Entities;

namespace Pet.WebAPI.Domain.Model
{
    /// <summary>
    /// Novo Endereço do Prestador
    /// </summary>
    public class NovoCliente
    {
        public NovoCliente(string? nomeCompleto, string? cpf, DateTime? dataNascimento, string? telefone1, bool whatsApp, string? telefone2, List<EnderecoCliente> enderecos)
        {
            NomeCompleto = nomeCompleto;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Telefone1 = telefone1;
            WhatsApp = whatsApp;
            Telefone2 = telefone2;
            Enderecos = enderecos;
        }

        public string? NomeCompleto { get; set; }
        public string? CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Telefone1 { get; set; }
        public bool WhatsApp { get; set; }
        public string? Telefone2 { get; set; }
        public List<EnderecoCliente>? Enderecos { get; set; } = new List<EnderecoCliente>();
    }
}
