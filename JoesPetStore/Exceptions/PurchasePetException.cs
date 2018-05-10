using System;

namespace JoesPetStore.Exceptions
{
    public class PurchasePetException : Exception
    {
        public PurchasePetException(string petAlreadyBought)
        {
        }
    }
}