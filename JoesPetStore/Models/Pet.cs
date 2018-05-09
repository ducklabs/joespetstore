namespace JoesPetStore.Models
{
    public class Pet : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Kind { get; set; }
        //public string Breed { get; set; }
    }
}