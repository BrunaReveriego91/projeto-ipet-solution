namespace Pet.WebAPI.Domain.Model
{
    public class NovoServicoPrestador
    {
        public int Prestador_Id { get; set; }
        public int Servico_Id { get; set; }
        public bool Ativo { get; set; }
        public float Valor { get; set; }
    }
}
