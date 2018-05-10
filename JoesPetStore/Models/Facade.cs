using System.Collections.Generic;
using System.Data.Entity;
using JoesPetStore.Exceptions;
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

        public static void PurchasePet()
        {
            if (ReceiptRepository.FindPurchaseReceipt() == null && PetRepository.FindPet() != null)
            {
                ReceiptRepository.PurchasePet();
            }
            else
            {
                throw new PurchasePetException("PetId Already Bought");
            }
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

        public static List<ApprovalViewModel> GetPendingApprovals()
        {
            var pendingApprovalViewModels = new List<ApprovalViewModel>();
            foreach (var pendingApproval in ApprovalRepository.GetPendingApprovals())
            {
                pendingApprovalViewModels.Add(ApprovalViewModelAssembler.Assemble(pendingApproval));
            }
            return pendingApprovalViewModels;
        }
    }
}