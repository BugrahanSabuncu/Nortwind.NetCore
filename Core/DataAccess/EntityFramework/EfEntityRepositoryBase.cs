using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, Context> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where Context : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //IDisposible patern
            using (Context context = new Context())
            {
                var addedEntity = context.Entry(entity); //veri kaynağında gönderdiğim değerle eşleşme yap fakat bu ekleme olduğu için durum referans yakalama
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (Context context = new Context())
            {
                var deleteEntity = context.Entry(entity); //veri kaynağında gönderdiğim değerle eşleşme yap fakat bu ekleme olduğu için durum referans yakalama
                deleteEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (Context context = new Context())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }

        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (Context context = new Context())
            {
                return filter == null ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (Context context = new Context())
            {
                var updateEntity = context.Entry(entity); //veri kaynağında gönderdiğim değerle eşleşme yap fakat bu ekleme olduğu için durum referans yakalama
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
