using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoesPetStore.Models
{
    internal class ReceiptRepository
    {
        public static void PurchasePet(int petId)
        {
            var reciept = new Receipt() { PetId = petId};
            TransactionManager.CreateEntity(reciept);
        }

        public static Receipt FindPurchaseReceipt()
        {
            return TransactionManager.FindEntity<Receipt>();
        }
    }
}