using ComponentUtil.Common.Crypto;

namespace ComponentWebApi.Common.Helper
{
    public static class PasswordHelper
    {
        /// <summary>
        ///     用户密码加密
        /// </summary>
        /// <param name="psw"></param>
        /// <param name="preHash">字符串是否已md5加密过一次</param>
        /// <returns></returns>
        public static string UserPswEncrypt(string psw, bool preHash = false)
        {
            //这里最好能写到配置文件，方便修改
            var salt = "zR^c5rvW5W%wGc%5pDaK";
            //双重加密，其中一层加盐
            var md5Str = psw;
            if (!preHash)
                md5Str = EncryptionHelper.Md5(md5Str);
            md5Str = EncryptionHelper.Md5(md5Str, salt);

            return md5Str;
        }
    }
}