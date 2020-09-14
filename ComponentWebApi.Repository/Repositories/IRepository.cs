using System.Linq;
using System.Threading.Tasks;
using ComponentWebApi.Model.Base;

namespace ComponentWebApi.Repository.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : class, IEntity<TKey>
    {
        Task<TEntity> GetAsync(TKey id);
        
        Task<TEntity[]> GetAsync(TKey[] ids);

        TEntity Insert(TEntity entity);

        TEntity[] Insert(TEntity[] entityArray);

        TEntity Update(TEntity entity);

        TEntity[] Update(TEntity[] entityArray);

        void Delete(TEntity entity);

        void Delete(TEntity[] entityArray);

        Task Delete(TKey id);

        Task Delete(params TKey[] ids);

        IQueryable<TEntity> GetAll();
    }
}