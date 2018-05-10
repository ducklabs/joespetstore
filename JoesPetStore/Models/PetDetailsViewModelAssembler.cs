namespace JoesPetStore.Models
{
    internal class PetDetailsViewModelAssembler
    {
        public static PetDetailsViewModel Assemble(Pet pet)
        {
            if (pet == null) return null;
            PetDetailsViewModel petViewModel = new PetDetailsViewModel {Name = pet.Name, Id = pet.Id};
            return petViewModel;
        }
    }
}