using System.Collections.Generic;

namespace JoesPetStore.Models
{
    internal class PetRepository
    {
        public static void CreatePet(PetInputViewModel petInputViewModel)
        {
            var pet = new Pet {Name = petInputViewModel.Name};
            TransactionManager.CreateEntity(pet);
        }

        public static Pet FindPet()
        {
            return TransactionManager.FindEntity<Pet>();
        }
        
    }
}