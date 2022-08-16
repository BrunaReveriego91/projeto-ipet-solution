namespace Pet.WebAPI.Domain.Model
{
    public class NovoUsuario
    {
        public NovoUsuario(
            string nome,
            string senha,
            string eMail)
        {
            Nome = nome;
            Senha = senha;
            EMail = eMail;
        }

        public string Nome { get; }
        public string Senha { get; set; }
        public string EMail { get; set; }
    }
}
