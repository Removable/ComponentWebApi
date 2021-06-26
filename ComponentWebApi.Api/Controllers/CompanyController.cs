using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponentUtil.Webs.Controllers;
using ComponentWebApi.Model.Healthink;
using ComponentWebApi.Services.Healthink;
using iHealthinkCore.Models;
using Newtonsoft.Json;

namespace ComponentWebApi.Api.Controllers
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class CompanyController : WebApiControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            this._companyService = companyService;
        }

        /// <summary>
        /// 获取单位颜色配置和首页菜单【前端使用】
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [HttpPost("GetCompanyColorsAndIndexMenus")]
        public async Task<string> GetCompanyColorsAndIndexMenus(int companyId, string session = "")
        {
            try
            {
                //获取配色
                var company = await _companyService.GetByIdAsync(companyId).ConfigureAwait(false);
                if (company == null)
                    return JsonConvert.SerializeObject(new ApiReturn(false, null, enmErrorCode.invalidParam, "",
                        "找不到单位"));

                if (string.IsNullOrWhiteSpace(company.Colors))
                {
                    company.Colors = JsonConvert.SerializeObject(new CompanyColors());
                }

                //获取菜单
                //排序规则：1.按sorting字段正序；2.按id倒序
                var menus = await _companyService.GetCompanyIndexMenusWithChildren(companyId);

                var menuTypes =
                    await _companyService.GetCompanyIndexMenuTypeArrayByIdsAsync(companyId,
                        menus.Select(i => i.Type).ToArray());
                foreach (var m in menus)
                {
                    m.MenuTypeName = menuTypes.FirstOrDefault(i => i.Id == m.Type)?.ShowName ?? string.Empty;
                    m.MenuTypeNameEng = menuTypes.FirstOrDefault(i => i.Id == m.Type)?.ShowNameEng ?? string.Empty;
                }

                var data = JsonConvert.SerializeObject(new ApiReturn(true,
                    new
                    {
                        Colors = JsonConvert.DeserializeObject<CompanyColors>(company.Colors),
                        menus
                    }, enmErrorCode.NoError, "", ""));
                return data;
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new ApiReturn(false, null, enmErrorCode.invalidParam, "",
                    "查询失败"));
            }
        }
    }
}