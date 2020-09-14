using System.Threading.Tasks;

namespace ComponentsBlog.Repository.UnitOfWorks
{
    public interface IUnitOfWork
    {
        /// <summary>
        ///     异步保存
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}