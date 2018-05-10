using System.Collections.Generic;
using System.Linq;
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

        public static List<Approval> GetPendingApprovals()
        {
            return TransactionManager.FindWhere<Approval>(app => app.ApprovalState == ApprovalState.Pending).ToList();
        }
    }

}