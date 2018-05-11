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
            Approval approval = new Approval()
            {
                CustomerEmail = customerEmail,
                ApprovalState = ApprovalState.Pending
            };
            TransactionManager.CreateEntity(approval);
        }

        public static List<Approval> FindApprovalsByApprovalState(ApprovalState approvalState)
        {
            return TransactionManager.FindWhere<Approval>(app => app.ApprovalState == approvalState).ToList();
        }

        public static Approval FindApprovalByEmail(string customerEmail)
        {
            return TransactionManager.FindWhere<Approval>(app => app.CustomerEmail.Equals(customerEmail)).FirstOrDefault();
        }

        public static List<Approval> FindPendingApprovalForPetsThatAreNotAlreadyApproved()
        {
            var leosApproval = TransactionManager.FindWhere<Approval>(app => app.ApprovalState == ApprovalState.Approved).FirstOrDefault();
            if (leosApproval == null)
            {
                return FindApprovalsByApprovalState(ApprovalState.Pending);
            }
            return new List<Approval>();
        }
    }

}