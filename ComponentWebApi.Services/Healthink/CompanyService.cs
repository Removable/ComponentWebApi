using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComponentWebApi.Repository.Repositories;
using ComponentWebApi.Repository.UnitOfWorks;
using EasyCaching.Core;
using iHealthinkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ComponentWebApi.Services.Healthink
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        private readonly IEasyCachingProvider _easyCaching;
        private readonly IMapper _mapper;
        private readonly IRepository<CompanyIndexMenu> _companyIndexMenuRepository;
        private readonly IRepository<CompanyIndexMenuType> _companyIndexMenuTypeRepository;
        private IRepository<CompanyIndexMenuTypeCompanyMapping> _companyIndexMenuTypeCompanyMappingRepository;

        public CompanyService(IUnitOfWork unitOfWork, IRepository<Company> repository,
            IRepository<CompanyIndexMenu> companyIndexMenuRepository,
            IRepository<CompanyIndexMenuType> companyIndexMenuTypeRepository,
            IRepository<CompanyIndexMenuTypeCompanyMapping> companyIndexMenuTypeCompanyMappingRepository,
            IEasyCachingProvider easyCaching, IMapper mapper) : base(unitOfWork, repository)
        {
            _easyCaching = easyCaching;
            _mapper = mapper;
            _companyIndexMenuRepository = companyIndexMenuRepository;
            _companyIndexMenuTypeRepository = companyIndexMenuTypeRepository;
            _companyIndexMenuTypeCompanyMappingRepository = companyIndexMenuTypeCompanyMappingRepository;
        }

        public async Task<List<CompanyIndexMenu>> GetCompanyIndexMenusWithChildren(int companyId)
        {
            var allMenus = await (await GetCompanyIndexMenusBase(companyId, true, 1, null, true)).ToListAsync();
            var parents = allMenus.Where(i => i.ParentMenuId == 0).ToList();
            var children = allMenus.Where(i => i.ParentMenuId > 0).ToList();

            foreach (var parent in parents)
            {
                foreach (var child in children.Where(i => i.ParentMenuId == parent.Id))
                {
                    parent.Children.Add(child);
                }
            }

            return parents;
        }

        /// <summary>
        /// 获取单位首页菜单
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="isActive"></param>
        /// <param name="orderBy"></param>
        /// <param name="parentMenuId">父菜单ID</param>
        /// <param name="getDefaultMenus">是否包含默认菜单</param>
        /// <returns></returns>
        private async Task<IQueryable<CompanyIndexMenu>> GetCompanyIndexMenusBase(int companyId, bool? isActive,
            int orderBy, int? parentMenuId, bool getDefaultMenus)
        {
            var queryable = _companyIndexMenuRepository.GetAll().AsNoTracking().Where(i => i.CompanyId == companyId);

            if (getDefaultMenus && !await queryable.AnyAsync())
            {
                queryable = _companyIndexMenuRepository.GetAll().AsNoTracking().AsNoTracking().Where(i => i.CompanyId == 0);
            }

            if (isActive.HasValue)
                queryable = queryable.Where(i => i.IsActive == isActive.Value);
            if (parentMenuId.HasValue)
                queryable = queryable.Where(i => i.ParentMenuId == parentMenuId);
            //排序
            if (orderBy == 1)
                queryable = queryable.OrderBy(i => i.Sorting);
            else if (orderBy == 2)
                queryable = queryable.OrderByDescending(i => i.Id);

            return queryable;
        }
        
        public async Task<CompanyIndexMenuType[]> GetCompanyIndexMenuTypeArrayByIdsAsync(int companyId, int[] ids)
        {
            if (ids == null) throw new ArgumentNullException();
            if (ids.Length == 0) return new CompanyIndexMenuType[0];

            var originTypes = await _companyIndexMenuTypeRepository.GetAll().AsNoTracking().Where(i => ids.Contains(i.Id))
                .ToArrayAsync();
            //自定义的类型
            var customTypes = await GetCompanyCustomIndexMenuType(companyId);

            foreach (var type in originTypes)
            {
                var customType = customTypes.FirstOrDefault(i => i.MenuTypeId == type.Id);
                if (customType == null) continue;

                type.ShowName = customType.ShowName;
                type.ShowNameEng = customType.ShowNameEng;
            }

            return originTypes;
        }
        
        public async Task<CompanyIndexMenuTypeCompanyMapping[]> GetCompanyCustomIndexMenuType(int companyId)
        {
            return await _companyIndexMenuTypeCompanyMappingRepository.GetAll().AsNoTracking()
                .Where(i => i.CompanyId == companyId).ToArrayAsync();
        }
    }
}