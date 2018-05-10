namespace JoesPetStore.Models
{
    public class Approval : IEntity
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; }
        public ApprovalState ApprovalState { get; set; }
    }
}