using System.Collections.Generic;
using System.Threading.Tasks;
using ComponentWebApi.Model.Base;
using ComponentWebApi.Repository.Repositories;
using ComponentWebApi.Repository.UnitOfWorks;

namespace ComponentWebApi.Services
{
    public abstract class BaseService<T, TKey> : IBaseService<T, TKey> where T : class, IEntity<TKey>, new()
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<T, TKey> _repository;

        protected BaseService(IUnitOfWork unitOfWork, IRepository<T, TKey> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<T[]> GetByIdAsync(params TKey[] ids)
        {
            return await _repository.GetAsync(ids);
        }

        public async Task<bool> SaveAsync(T entity)
        {
            try
            {
                _repository.Insert(entity);
                var i = await _unitOfWork.SaveChangesAsync();
                return i == 1;
            }
            catch //(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> SaveAsync(List<T> entityList)
        {
            try
            {
                _repository.Insert(entityList.ToArray());
                var i = await _unitOfWork.SaveChangesAsync();
                return i == entityList.Count;
            }
            catch // (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _repository.Update(entity);
                var i = await _unitOfWork.SaveChangesAsync();
                return i == 1;
            }
            catch //(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(List<T> entity)
        {
            try
            {
                _repository.Update(entity.ToArray());
                var i = await _unitOfWork.SaveChangesAsync();
                return i >= 1;
            }
            catch //(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(TKey id)
        {
            try
            {
                await _repository.Delete(id);
                var i = await _unitOfWork.SaveChangesAsync();
                return i == 1;
            }
            catch //(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(params TKey[] ids)
        {
            try
            {
                await _repository.Delete(ids);
                var i = await _unitOfWork.SaveChangesAsync();
                return i >= 1;
            }
            catch //(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _repository.Delete(entity);
                var i = await _unitOfWork.SaveChangesAsync();
                return i == 1;
            }
            catch //(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(List<T> entityList)
        {
            try
            {
                _repository.Delete(entityList.ToArray());
                var i = await _unitOfWork.SaveChangesAsync();
                return i >= 1;
            }
            catch //(Exception e)
            {
                return false;
            }
        }
    }
}