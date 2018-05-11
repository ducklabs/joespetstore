using JoesPetStore.Exceptions;
using JoesPetStore.Mail;

namespace JoesPetStore.Models
{
    public class Approval : IEntity
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; }
        public ApprovalState ApprovalState { get; set; }

        public void Approve()
        {

            if (ApprovalRepository.FindApprovalsByApprovalState(ApprovalState.Approved).Count != 0)
            {
                throw new ApprovalException("Another Approval has already been Approved");
            }

            if (ApprovalState == ApprovalState.Pending)
            {
                ApprovalState = ApprovalState.Approved;
                EmailServerServiceFactory.EmailServer.SendEmail(CustomerEmail,FormatApprovedEmail());
            }
            else
            {
                throw new ApprovalException("Approval not in Pending state");
            }
        }

        public string FormatApprovedEmail()
        {
            return
                "Congratulations, you've been approved and you can purchase Leo <a href='localhost:50112/Pet/PurchasePage?customerEmail=\"" +
                CustomerEmail + "\"' >Purchase Pet</a>";
        }

        public void Reject()
        {
            if (ApprovalState == ApprovalState.Pending)
            {
                ApprovalState = ApprovalState.Denied;
                EmailServerServiceFactory.EmailServer.SendEmail(CustomerEmail, FormatRejectedEmail());
            }
            else
            {
                throw new ApprovalException("Approval not in Pending state");
            }
        }

        private string FormatRejectedEmail()
        {
            return
                "Sorry, you have been denied.";
        }

        public void PurchasePet()
        {
            if (ReceiptRepository.FindPurchaseReceipt() == null &&
                ApprovalState == ApprovalState.Approved)
            {
                ReceiptRepository.CreateReceipt(CustomerEmail);
            }
            else
            {
                throw new PurchasePetException("PetId Already Bought Or Not Approved");
            }
        }
    }
}