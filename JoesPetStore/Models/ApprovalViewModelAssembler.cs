namespace JoesPetStore.Models
{
    public class ApprovalViewModelAssembler
    {
        public static ApprovalViewModel Assemble(Approval pendingApproval)
        {
            return new ApprovalViewModel(){CustomerEmail = pendingApproval.CustomerEmail};
        }
    }
}