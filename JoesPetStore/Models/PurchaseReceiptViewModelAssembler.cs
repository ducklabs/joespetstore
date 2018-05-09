using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoesPetStore.Models
{
    internal class PurchaseReceiptViewModelAssembler
    {
        public static PurchaseReceiptViewModel Assemble(Receipt receipt)
        {
            if (receipt == null) return null;
            PurchaseReceiptViewModel receiptViewModel = new PurchaseReceiptViewModel { PetId = receipt.PetId };
            return receiptViewModel;
        }
    }
}