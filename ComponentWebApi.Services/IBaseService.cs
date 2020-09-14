using System.Collections.Generic;
using System.Threading.Tasks;
using ComponentWebApi.Model.Base;
using EasyCaching.Core.Interceptor;

namespace ComponentWebApi.Services
{
    public interface IBaseService<T, in TKey> where T : class, IEntity<TKey>, new()
    {
        /// <summary>
        ///     根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [EasyCachingAble(Expiration = 60)]
        abstract Task<T> GetByIdAsync(TKey id);

        /// <summary>
        ///     根据ID查找
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [EasyCachingAble(Expiration = 60)]
        abstract Task<T[]> GetByIdAsync(params TKey[] ids);

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [EasyCachingPut(CacheKeyPrefix = "Entity")]
        abstract Task<bool> SaveAsync(T entity);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Task<bool> SaveAsync(List<T> entityList);

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(List<T> entity);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TKey id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(params TKey[] ids);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(List<T> entityList);
    }
}