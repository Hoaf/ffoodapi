using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Models.Repositories.Basic
{
    public partial class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly BookingFoodContext context;
        private readonly DbSet<TEntity> dbSet;
        public BaseRepository(BookingFoodContext Context)
        {
            //context = new BookingFoodContext();
            context = Context;
            this.dbSet = context.Set<TEntity>();
        }

        public EntityEntry<TEntity> Create(TEntity entity)
        {

            return dbSet.Add(entity);
        }

        public TEntity FirstOrDefault()
        {
            return dbSet.FirstOrDefault();
        }

        public IQueryable<TEntity> Get()
        {
            return dbSet;
        }

        public TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public EntityEntry<TEntity> Remove(TEntity entity)
        {
            return dbSet.Remove(entity);
        }

        public EntityEntry<TEntity> Remove(int entity)
        {
            var key = dbSet.Find(entity);
            if (key != null)
            {
                return dbSet.Remove(key);
            }
            else
            {
                return null;
            }
        }



        public EntityEntry<TEntity> Update(TEntity entity)
        {
            var entry = context.Entry(entity);
            entry.State = EntityState.Modified;
            return entry;
        }
    }
}
