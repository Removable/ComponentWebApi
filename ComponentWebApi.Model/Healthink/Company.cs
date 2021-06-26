using System;
using System.Collections.Generic;
using ComponentWebApi.Model.Base;
using Newtonsoft.Json;

namespace iHealthinkCore.Models
{
    public class Company : BaseEntity
    {
        public string name { get; set; }

        /// <summary>
        /// 公司别名，用于与部分供应商对接
        /// </summary>
        public string ReportName { get; set; }

        public string code { get; set; }
        public string name_en { get; set; }
        public bool MulLanguage { get; set; }
        public bool? Active { get; set; }
        public bool show_in_page { get; set; }
        public Nullable<int> show_priority { get; set; }
        public int city_id { get; set; }
        public string contact { get; set; }
        public string tel { get; set; }
        public string addr { get; set; }
        public string zip { get; set; }
        public string email { get; set; }
        public Nullable<bool> vip { get; set; }
        public string logo_file { get; set; }
        public string logo_file_s { get; set; }
        public string logo_file_l { get; set; }
        public string welcome_word { get; set; }
        public bool? UseHealthInsurance { get; set; }
        public bool? UseHealthAsset { get; set; }
        public bool? UseExpressDoctor { get; set; }
        public bool? UseHealthStore { get; set; }
        public bool? UseHealthCard { get; set; }
        public bool? UseHealthExam { get; set; }
        public bool? UseHealthInfo { get; set; }
        public int? CustomerIdentifierTypeId { get; set; }
        public virtual string ServiceExecutive { get; set; }
        public virtual string ServiceMobilePhone { get; set; }

        public virtual string ServiceEmail { get; set; }

        //机构对接人
        public int? PickPeopleId { get; set; }
        public bool? CardLoginAllowed { get; set; }

        public string AccessToken { get; set; }

        //public int? delaydays { get; set; }
        //public string RelativeLimit { get; set; }
        public bool? ActiveRelativeUnreadable { get; set; }
        public bool? ElecReportNeedAuth { get; set; }
        public bool AllowEditorRelation { get; set; }

        // public virtual bool IsActive
        // {
        //     get { return Active != false; }
        //     set { Active = value; }
        // }

        public bool? iOSForbidden { get; set; }
        public bool? AndroidForbidden { get; set; }
        public string iOSForbiddenTips { get; set; }
        public string AndroidForbiddenTips { get; set; }
        public string JiangtaiPolicyId { get; set; }
        public Nullable<DateTime> PolicyStartDate { get; set; }
        public Nullable<DateTime> PolicyEndDate { get; set; }
        public int? BackPassState { get; set; } //是否同步订单 0不同步，1同步，后续状态自定义
        public int? OpenReimState { get; set; } //是否开启在线报销，0=不开启，1=开启
        public bool? ChangFlg { get; set; } //改约功能是否在已安排中可用， 0不可用，1可用
        public bool? ApplyForRefund { get; set; } //是否允许用户取消订单自动提交退款申请， 0不允许，1允许

        /// <summary>
        /// 是否开启实时退款 0=不开启，1=开启
        /// </summary>
        public bool IsRefundNow { get; set; }

        /// <summary>
        /// 预导加项不可选
        /// </summary>
        public bool PreExtraUnchangeable { get; set; } = false;

        // public virtual bool IsCardLoginAllowed
        // {
        //     get
        //     {
        //         if (CardLoginAllowed == null) return false;
        //         return (bool) CardLoginAllowed;
        //     }
        //     set { CardLoginAllowed = value; }
        // }

        /// <summary>
        /// 爱康编码，用于接口对接
        /// </summary>
        public string AiKangCode { get; set; }

        /// <summary>
        /// 密码错误锁定的最大连续次数，为0则不锁定
        /// </summary>
        public int PwdErrMaxCount { get; set; } = 0;

        /// <summary>
        /// 颜色配置
        /// </summary>
        public string Colors { get; set; } = JsonConvert.SerializeObject(new CompanyColors());

        /// <summary>
        /// 移动端每行菜单最大显示个数
        /// </summary>
        public int MobileMenuColumnCount { get; set; } = 3;

        // [JsonIgnore]
        // public CompanyColors ColorsClass
        // {
        //     get => string.IsNullOrWhiteSpace(Colors)
        //         ? new CompanyColors()
        //         : JsonConvert.DeserializeObject<CompanyColors>(Colors);
        //     set => Colors = JsonConvert.SerializeObject(value);
        // }
    }
    
    public class CompanyIndexMenu : BaseEntity
    {
        /// <summary>
        /// 所属单位ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 显示的名称
        /// </summary>
        public string ShowName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string ShowNameEng { get; set; } = string.Empty;

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 点击方法名，无则为空
        /// </summary>
        public string ClickMethod { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 父菜单ID，为0则为一级菜单
        /// </summary>
        public int ParentMenuId { get; set; }

        /// <summary>
        /// 是否有下级菜单
        /// </summary>
        public bool HasChildren { get; set; }

        /// <summary>
        /// 是否有下级菜单
        /// </summary>
        public IList<CompanyIndexMenu> Children { get; set; } = new List<CompanyIndexMenu>();

        /// <summary>
        /// 菜单类型
        /// </summary>
        public int Type { get; set; } = 1;
        
        /// <summary>
        /// 菜单类型名称（不入库）
        /// </summary>
        public string MenuTypeName { get; set; }
        
        /// <summary>
        /// 菜单类型名称（不入库）
        /// </summary>
        public string MenuTypeNameEng { get; set; }
    }
    
    public class CompanyIndexMenuType : BaseEntity
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string ShowName { get; set; }

        /// <summary>
        /// 显示名称（英文）
        /// </summary>
        public string ShowNameEng { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }

    /// <summary>
    /// 单位自定义名称的菜单类型
    /// </summary>
    public class CompanyIndexMenuTypeCompanyMapping : BaseEntity
    {
        /// <summary>
        /// 菜单类型ID
        /// </summary>
        public int MenuTypeId { get; set; }
        
        /// <summary>
        /// 单位ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string ShowName { get; set; } = string.Empty;

        /// <summary>
        /// 显示名称（英文）
        /// </summary>
        public string ShowNameEng { get; set; } = string.Empty;
    }

    /// <summary>
    /// 自定义类型的相关信息
    /// </summary>
    public class CustomCompanyIndexMenuTypeInfo
    {
        /// <summary>
        /// 自定义名称表ID
        /// </summary>
        public int MappingId { get; set; }
        /// <summary>
        /// 原菜单类型ID
        /// </summary>
        public int MenuTypeId { get; set; }
        
        /// <summary>
        /// 单位ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// 显示名称
        /// </summary>
        public string CustomShowName { get; set; }
        
        /// <summary>
        /// 原名
        /// </summary>
        public string OriginShowName { get; set; }

        /// <summary>
        /// 显示名称（英文）
        /// </summary>
        public string CustomShowNameEng { get; set; }
        
        /// <summary>
        /// 原英文名
        /// </summary>
        public string OriginShowNameEng { get; set; }
    }

    /// <summary>
    /// 单位各类颜色配置
    /// </summary>
    public class CompanyColors
    {
        /// <summary>
        /// 主题色
        /// </summary>
        public string ThemeColor { get; set; } = string.Empty;

        /// <summary>
        /// SVG颜色
        /// </summary>
        public string SvgColor { get; set; } = string.Empty;

        /// <summary>
        /// SVG颜色
        /// </summary>
        public string SvgColorDark { get; set; } = string.Empty;

        /// <summary>
        /// 按钮颜色
        /// </summary>
        public string ButtonColor { get; set; } = string.Empty;

        /// <summary>
        /// 顶部浮在Banner上方的文字颜色
        /// </summary>
        public string TopColor { get; set; } = string.Empty;
    }
}