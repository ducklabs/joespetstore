using JoesPetStore.Models;

namespace JoesPetStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JoesPetStore.Models.PetDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JoesPetStore.Models.PetDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Pets.AddOrUpdate(x => x.Id,
                new Pet() { Id = 1, Name = "Leo" }
            );
            context.Receipts.AddOrUpdate(x => x.Id,
                new Receipt() { Id = 1, PetId = 0 }
            );
        }
    }
}
