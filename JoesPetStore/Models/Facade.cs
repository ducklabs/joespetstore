using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JoesPetStore.Exceptions;
using JoesPetStore.ViewModels;
using static JoesPetStore.Models.PetRepository;

namespace JoesPetStore.Models
{
    public class Facade
    {
        public static PetDetailsViewModel FindPet()
        {
           var pet = PetRepository.FindPet();
           return PetDetailsViewModelAssembler.Assemble(pet);
        }

        public static void CreatePet(PetInputViewModel petInputViewModel)
        {
            PetRepository.CreatePet(petInputViewModel);
        }

        public static void PurchasePet(ApprovalViewModel approvalViewModel)
        {
            var approval = ApprovalRepository.FindApprovalByEmail(approvalViewModel.CustomerEmail);
            if (approvalViewModel == null) throw new PurchasePetException("No approval found");

            approval.PurchasePet();

            //TransactionManager.Commit();
        }

        public static ReceiptViewModel FindPurchaseReceipt()
        {
            var receipt = ReceiptRepository.FindPurchaseReceipt();
            return ReceiptViewModelAssembler.Assemble(receipt);
        }

        public static void RequestPetPurchase(string customerEmail)
        {

            ApprovalRepository.CreatePendingApproval(customerEmail);

        }

        public static List<ApprovalViewModel> GetApprovals(ApprovalState approvalState)
        {
            var approvals = ApprovalRepository.FindApprovalsByApprovalState(approvalState);
            var approvalViewModels = ApprovalViewModelAssembler.Assemble(approvals);
            return approvalViewModels;
        }

        public static void Approve(ApprovalViewModel approvalViewModel)
        {
            var approval = ApprovalRepository.FindApprovalByEmail(approvalViewModel.CustomerEmail);
            if (approval == null) return;
            
            approval.Approve();

            TransactionManager.Commit();
        }

        public static List<ApprovalViewModel> GetPendingApprovals()
        {
            var pendingApprovals = ApprovalRepository.FindPendingApprovalForPetsThatAreNotAlreadyApproved();
            var pendApprovalViewModels = ApprovalViewModelAssembler.Assemble(pendingApprovals);
            return pendApprovalViewModels;
        }

        public static void Reject(ApprovalViewModel pendingApproval)
        {
            var approval = ApprovalRepository.FindApprovalByEmail(pendingApproval.CustomerEmail);
            if (approval == null) return;

            approval.Reject();

            TransactionManager.Commit();
        }
    }
}