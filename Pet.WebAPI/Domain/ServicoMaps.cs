namespace Pet.WebAPI.Domain
{
    public class ServicoMaps
    {
        public ServicoMaps(int id, string? nome, string? descricao, bool ativo, float valor)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
        }

        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get; set; } = false;
        public float Valor { get; set; }
    }
}
