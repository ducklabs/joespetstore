namespace JoesPetStore.Models
{
    public class Pet : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}