using System;
using System.Data.Entity;

namespace JoesPetStore.Models
{
    public partial class PetDbContext : DbContext
    {
        public PetDbContext()
            : base("name=PetStoreDatabase")
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Approval> Approvals { get; set; }

    }
}