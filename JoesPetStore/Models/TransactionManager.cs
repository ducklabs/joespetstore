using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;

namespace JoesPetStore.Models
{
    internal class TransactionManager
    {
        public static void CreateEntity<T>(T entity) where T : class, IEntity
        {
            var petDbContext = new PetDbContext();
            petDbContext.Set<T>().Add(entity);
            petDbContext.SaveChanges();
        }

        public static T FindEntity<T>() where T : class, IEntity
        {
            var db = new PetDbContext();
            return db.Set<T>().FirstOrDefault();
        }
    }
}