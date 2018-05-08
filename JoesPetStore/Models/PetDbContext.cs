using System.Data.Entity;

namespace JoesPetStore.Models
{
    public class PetDbContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
    }
}