using System.Collections.Generic;
using System.Threading.Tasks;
using ComponentWebApi.Model.Base;
using EasyCaching.Core.Interceptor;

namespace ComponentWebApi.Services
{
    public interface IBaseService<T> : IDependency where T : class, new()
    {
        /// <summary>
        ///     根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [EasyCachingAble(Expiration = 60)]
        abstract Task<T> GetByIdAsync(int id);

        /// <summary>
        ///     根据ID查找
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [EasyCachingAble(Expiration = 60)]
        abstract Task<T[]> GetByIdAsync(params int[] ids);

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [EasyCachingPut]
        abstract Task<T> SaveAsync(T entity);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        [EasyCachingPut]
        Task<T[]> SaveAsync(List<T> entityList);

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [EasyCachingPut]
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [EasyCachingPut]
        Task<T[]> UpdateAsync(List<T> entity);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(params int[] ids);

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