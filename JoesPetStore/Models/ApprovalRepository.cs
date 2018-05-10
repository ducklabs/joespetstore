using System.Collections.Generic;
using System.Linq;
using JoesPetStore.ViewModels;
using NUnit.Framework;

namespace JoesPetStore.Models
{
    public class ApprovalRepository
    {
        public static void CreatePendingApproval(string customerEmail)
        {
            Approval approval = new Approval(){ CustomerEmail = customerEmail, ApprovalState = ApprovalState.Pending };
            TransactionManager.CreateEntity(approval);
        }

        public static List<Approval> GetApprovals(ApprovalState approvalState)
        {
            return TransactionManager.FindWhere<Approval>(app => app.ApprovalState == approvalState).ToList();
        }

        public static List<Approval> GetApprovals()
        {
            return TransactionManager.FindWhere<Approval>(app => true).ToList();
        }

        public static void Approve(ApprovalViewModel approvalViewModel)
        {
            var approval = TransactionManager.FindWhere<Approval>(app => app.CustomerEmail.Equals(approvalViewModel.CustomerEmail)).ToList().ElementAtOrDefault(0);
            if (approval == null) return;
            approval.ApprovalState = ApprovalState.Approved;
            TransactionManager.Commit();
        }
    }

}