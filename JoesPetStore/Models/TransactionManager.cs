using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;

namespace JoesPetStore.Models
{
    public class TransactionManager
    {

        static PetDbContext dbContext = new PetDbContext();

        public static void CreateEntity<T>(T entity) where T : class, IEntity
        {            
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
        }

        public static T FindEntity<T>() where T : class, IEntity
        {
            return dbContext.Set<T>().FirstOrDefault();
        }

        public static IQueryable<T> FindWhere<T>(Expression<Func<T, bool>> expression) where T : class, IEntity
        {
            return dbContext.Set<T>().Where(expression);
        }

        public static void DeleteEntities<T>() where T : class, IEntity
        {
            var dbSet = dbContext.Set<T>();
            var entities = dbSet.ToList();
            dbSet.RemoveRange(entities);
            dbContext.SaveChanges();
        }

        public static void Commit()
        {
            dbContext.SaveChanges();
        }
    }
}