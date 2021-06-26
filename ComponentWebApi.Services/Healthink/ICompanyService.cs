using System.Collections.Generic;
using System.Threading.Tasks;
using iHealthinkCore.Models;

namespace ComponentWebApi.Services.Healthink
{
    public interface ICompanyService : IBaseService<Company>
    {
        Task<List<CompanyIndexMenu>> GetCompanyIndexMenusWithChildren(int companyId);

        Task<CompanyIndexMenuType[]> GetCompanyIndexMenuTypeArrayByIdsAsync(int companyId, int[] ids);
    }
}