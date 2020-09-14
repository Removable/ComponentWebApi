using System.Linq;
using System.Threading.Tasks;
using ComponentWebApi.Model.Base;

namespace ComponentWebApi.Repository.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        Task<TEntity> GetAsync(TKey id);

        Task<TEntity> InsertAsync(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(TKey id);

        IQueryable<TEntity> GetAll();
    }
}