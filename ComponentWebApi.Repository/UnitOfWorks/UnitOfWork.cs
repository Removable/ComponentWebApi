using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ComponentWebApi.Repository.UnitOfWorks
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public UnitOfWork(TDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}