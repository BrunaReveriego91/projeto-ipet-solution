namespace SysIPetUI.Models
{
    public class Servico
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Descricao { get; set; } = "";
        public bool Ativo { get; set; } = false;

        //public Servico()
        //{
        //    Nome = "";
        //    Descricao = "";
        //    Ativo = false;
        //}

        //public Servico(string nome, string descricao, bool ativo) : this()
        //{
        //    Nome = nome;
        //    Descricao = descricao;
        //    Ativo = ativo;
        //}
    }
}
