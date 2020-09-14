using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponentWebApi.Model.Base;
using Microsoft.EntityFrameworkCore;

namespace ComponentWebApi.Repository.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly MyDbContext _dbContext;

        public Repository(MyDbContext dbDbContext)
        {
            _dbContext = dbDbContext;
        }

        protected DbSet<TEntity> Table => _dbContext.Set<TEntity>();

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var result = await Table.AddAsync(entity);
            return result.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public async void Delete(TKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = await GetAsync(id);
            if (entity != null) Delete(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null) return;

            Table.Attach(entity);
        }

        private TEntity GetFromChangeTrackerOrNull(TKey id)
        {
            var entry = _dbContext.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TKey>.Default.Equals(id, ((TEntity) ent.Entity).Id)
                );

            return entry?.Entity as TEntity;
        }
    }
}