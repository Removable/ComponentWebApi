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

        public Repository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DbSet<TEntity> Table => _dbContext.Set<TEntity>();

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<TEntity[]> GetAsync(TKey[] ids)
        {
            return await Table.Where(i => ids.Contains(i.Id)).ToArrayAsync();
        }

        public TEntity Insert(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Added;

            return entity;
        }

        public TEntity[] Insert(TEntity[] entityArray)
        {
            AttachIfNot(entityArray);
            foreach (var entity in entityArray)
            {
                _dbContext.Entry(entity).State = EntityState.Added;
            }

            return entityArray;
        }

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public TEntity[] Update(TEntity[] entityArray)
        {
            AttachIfNot(entityArray);
            foreach (var entity in entityArray)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            return entityArray;
        }

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(TEntity[] entityArray)
        {
            AttachIfNot(entityArray);
            foreach (var entity in entityArray)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public async Task Delete(TKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = await GetAsync(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public async Task Delete(params TKey[] ids)
        {
            var entityArray = GetFromChangeTrackerOrNull(ids).ToArray();
            if (entityArray.Length > 0)
            {
                Delete(entityArray);
                return;
            }

            entityArray = await GetAsync(ids);
            if (entityArray.Length > 0)
            {
                Delete(entityArray);
            }
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

        protected virtual void AttachIfNot(TEntity[] entityArray)
        {
            var entryList = _dbContext.ChangeTracker.Entries().Where(ent => entityArray.Contains((TEntity)ent.Entity));
            if (!entryList.Any()) return;

            Table.AttachRange(entityArray);
        }

        private TEntity GetFromChangeTrackerOrNull(TKey id)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(ent =>
                ent.Entity is TEntity entity && EqualityComparer<TKey>.Default.Equals(id, entity.Id));

            return entry?.Entity as TEntity;
        }

        private IEnumerable<TEntity> GetFromChangeTrackerOrNull(TKey[] ids)
        {
            var entryEnumerable = _dbContext.ChangeTracker.Entries()
                .Where(ent => ent.Entity is TEntity entity && ids.Contains(entity.Id));

            foreach (var entityEntry in entryEnumerable)
            {
                yield return entityEntry?.Entity as TEntity;
            }
        }
    }
}