using System.Linq;
using System.Threading.Tasks;
using ComponentWebApi.Model.Base;

namespace ComponentWebApi.Repository.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetAsync(int id);
        
        Task<TEntity[]> GetAsync(int[] ids);

        TEntity Insert(TEntity entity);

        TEntity[] Insert(TEntity[] entityArray);

        TEntity Update(TEntity entity);

        TEntity[] Update(TEntity[] entityArray);

        void Delete(TEntity entity);

        void Delete(TEntity[] entityArray);

        Task Delete(int id);

        Task Delete(params int[] ids);

        IQueryable<TEntity> GetAll();
    }
}