namespace ComponentWebApi.Model.Healthink
{
    public enum enmErrorCode
    {
        NoError = 0, //正常
        Undefined = -9999999, //未定义

        /// <summary>
        /// 错误代码
        /// </summary>
        dbError = -1, //数据库读写错误
        invalidSession = -2, //用户验证字错误
        invalidParam = -3, //参数错误
        invalidRecord = -4, //无效记录
        smsRequestFail = -5, //短信发送错误
        invalidOpr = -6, //操作错误(如短信重发时间没过60秒)
        invalidVerifyCode = -7, //短信验证码错误
        cannotAddExamOrder = -8, //不能添加订单
        cannotCancelExamOrder = -49, //不能取消订单
        relativeNotAllowed = -9, //不允许家属预约
        cpyuserAccountExist = -10, //单位用户账号已存在
        cpyuserNotExist = -11, //不在单位名单中，不能注册为单位用户
        cpyNotExist = -12, //单位不存在
        cityNotExist = -13, //城市错误（一般不会用到）
        mobileRegistered = -14, //手机号已被注册
        invalidArea = -15, //区域不存在（国家、省、市、区等）
        invalidCountry = -16, //国家不存在
        reBookNotAllowed = -17, //不允许改约
        reBookNotChanged = -18, //未存在任何变动项的改约
        orderNotExist = -19, //预约订单不存在
        invalidUsernameOrPass = -20, //用户名或密码错误
        UserDeleted = -21, //用户已删除
        UserNotActive = -22, //用户未激活
        UserNotRegistered = -23,
        UserWrongPassword = -24,
        UserNotExsit = -25,
        invalidMobilePhone = -26,
        invalidIdCardNumber = -27,
        IdCardDuplicated = -28, //身份证号重复
        IdCardRegistered = -29,
        InvalidCard = -30, //卡账户错误
        CardBound = -31, //卡绑定错误
        CustomerDuplicated = -32, //用户信息重复
        NotAuthenticated = -33, //未授权认证
        MobilePhoneNotBinding = -34, //手机号未绑定账号
        CustomerInfoMatchError = -35, //用户信息不匹配，比如姓名与身份证信息不匹配
        accessTimeout = -36, //访问超时
        accessProhibit = -37, //不允许访问
        accessWithPasswordNotAllow = -38, //未修改缺省密码访问
        CustomerInfoLacking = -39,
        PaymodeNotDefined = -40,
        ReportOnlyTheDayUpload = -41, //相同用户报告只有在当天可以多次上传，次日无法上传
        WxUserNotExist = -50,
        SystemMaitenance = -98, //系统维护
        unkownErr = -99, //未知错误
        PhysiqueReportRepeat = -100, //报告重复
        DecryptErr = -51, //解密失败

        /// <summary>
        /// 命令代码
        /// </summary>
        conformLoginOrRegister = 10, //注册时如果手机号已经存在，需要用户确认
        approveCustomer = 11, //第三方登录用户验证（账号存在）
        cardRegister = 100, //卡注册命令

        //交易相关错误 -200 ~ -300
        noPostError = 0,
        transPostError = -200, // 提交交易post错误，具体错误原因待定义（如延时错误，则订单应该生成）
        reserved201 = -201, // 保留，待后续定义使用
        reserved202 = -202, // 保留，待后续定义使用
        reserved203 = -203, // 保留，待后续定义使用
        reserved204 = -204, // 保留，待后续定义使用
        reserved205 = -205, // 保留，待后续定义使用
        reserved206 = -206, // 保留，待后续定义使用
        transInterrupt = -207, //交易在途中被终止（如怡安优选多个订单项提交中出现错误,需要对交易继续后续处理）
        partnerInfoError = -208, //渠道信息错误
        VendorInfoError = -209, //供应商信息错误
        VendorReturnFail = -210, // 保留，待后续定义使用

        //oss相关错误
        TradeNotMessage = -60, //oss文件回调，交易信息缺失
        BucketNotMessage = -61, //oss文件回调，系统变量缺失
        FileNameRepeat = -62, //oss文件回调，文件重名
        DataRecordFail = -63, //oss文件回调，回调记录未生成
        NetWorkInterrupt = -64, //oss文件回调，回调过程中网络中断
    }
}