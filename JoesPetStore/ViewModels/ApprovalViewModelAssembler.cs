using System.Collections.Generic;
using JoesPetStore.Models;

namespace JoesPetStore.ViewModels
{
    public class ApprovalViewModelAssembler
    {
        public static ApprovalViewModel Assemble(Approval approval)
        {
            return new ApprovalViewModel()
            {
                CustomerEmail = approval.CustomerEmail,
                ApprovalState = approval.ApprovalState
            };
        }

        public static List<ApprovalViewModel> Assemble(List<Approval> approvals)
        {
            var approvalViewModels = new List<ApprovalViewModel>();

            foreach (var approval in approvals)
            {
                approvalViewModels.Add(Assemble(approval));
            }

            return approvalViewModels;
        }
    }
}