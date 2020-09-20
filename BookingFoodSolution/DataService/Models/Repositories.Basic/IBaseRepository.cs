using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Models.Repositories.Basic
{
    public partial interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IQueryable<TEntity> Get();
        EntityEntry<TEntity> Create(TEntity entity);
        EntityEntry<TEntity> Update(TEntity entity);
        EntityEntry<TEntity> Remove(TEntity entity);
        EntityEntry<TEntity> Remove(int entity);
        TEntity FirstOrDefault();
    }
}
