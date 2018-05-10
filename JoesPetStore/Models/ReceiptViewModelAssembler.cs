using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoesPetStore.Models
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