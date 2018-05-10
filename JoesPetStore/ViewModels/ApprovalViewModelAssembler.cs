using JoesPetStore.Models;

namespace JoesPetStore.ViewModels
{
    public class ApprovalViewModelAssembler
    {
        public static ApprovalViewModel Assemble(Approval pendingApproval)
        {
            return new ApprovalViewModel(){CustomerEmail = pendingApproval.CustomerEmail};
        }
    }
}