namespace Pet.WebAPI.Domain.Entities
{
    public class ClientPet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Color { get; set; }
        public string Birthday { get; set; }
        public string Owner { get; set; }
        public string Breed { get; set; }

    }
}
