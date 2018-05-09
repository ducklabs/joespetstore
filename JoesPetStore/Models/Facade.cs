using System;
using System.Data.Entity;
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

        public static void PurchasePet(int petId)
        {
            if (ReceiptRepository.FindPurchaseReceipt() == null && PetRepository.FindPet() != null)
            {
                ReceiptRepository.PurchasePet(petId);
            }
        }

        public static PurchaseReceiptViewModel FindPurchaseReceipt(int petId)
        {
            var receipt = ReceiptRepository.FindPurchaseReceipt();
            return PurchaseReceiptViewModelAssembler.Assemble(receipt);
        }
    }
    
}