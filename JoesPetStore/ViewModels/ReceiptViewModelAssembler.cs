using JoesPetStore.Models;

namespace JoesPetStore.ViewModels
{
    internal class ReceiptViewModelAssembler
    {
        public static ReceiptViewModel Assemble(Receipt receipt)
        {
            if (receipt == null) return null;
            ReceiptViewModel receiptViewModel = new ReceiptViewModel { PetId = receipt.PetId };
            return receiptViewModel;
        }
    }
}