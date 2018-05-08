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
    }
    
}