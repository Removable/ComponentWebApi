using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web;

namespace ComponentWebApi.Model.Healthink
{
    public class ApiMessage
    {
        public ApiMessage()
        {
        }

        /// <summary>
        /// //消息类型，暂未定义
        /// </summary>
        public int messageType { get; set; }

        //消息显示方式，1：弹出显示，自动消失 2：不弹窗，由APP确定在页面的显示位置  3: 提示信息，用户确认后关闭 4：弹出窗显示信息并提供“是”/“否”选择，根据用户选择，APP确定是否继续操作
        public int showType { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 消息正文
        /// </summary>
        public string content { get; set; }
    }

    public class ApiReturn
    {
        public ApiReturn(string langCode = "zh-cn")
        {
            errorCode = 0;
            success = true;
            message = new ApiMessage();
            message.messageType = 0;
            message.showType = 1;
            message.title = "";
            signType = "";
            signCode = "";
            language = langCode;
            encryptType = "";
        }

        public ApiReturn(bool success, object data, string langCode, string msg = "")
        {
            ErrorCode = enmErrorCode.NoError;
            this.success = success;
            message = new ApiMessage();
            message.messageType = 0;
            message.showType = 1;
            message.title = "";
            message.content = msg;
            signType = "";
            signCode = "";
            language = langCode;
            encryptType = "";

            this.data = data;
        }

        public ApiReturn(bool success, object data, enmErrorCode errorCode, string langCode, string msg = "",
            string title = "", int showType = 1, object data2 = null)
        {
            ErrorCode = errorCode;
            this.success = success;
            message = new ApiMessage();
            message.messageType = 0;
            message.showType = showType;
            message.title = title;
            message.content = msg;
            signType = "";
            signCode = "";
            encryptType = "";
            language = langCode;
            this.data = data;
            this.data2 = data2;
        }

        public string language { get; set; }
        public bool success { get; set; }
        public int errorCode { get; set; }

        public enmErrorCode ErrorCode
        {
            get { return (enmErrorCode) errorCode; }
            set { errorCode = (int) value; }
        }

        public string signType { get; set; } //校验方式 MD5 或者是空（不校验）
        public string signCode { get; set; } //校验码（针对业务数据校验）        public string language { get; set; }

        public string encryptType { get; set; } //加密方式DES（针对业务数据加密） 或者为空 （不加密）         public object data { get; set; }
        public ApiMessage message { get; set; }
        public object data { get; set; }
        public object data2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="errorCode"></param>
        /// <param name="msg"></param>
        /// <param name="msgTitle"></param>
        /// <param name="msgShowType"></param>
        /// <param name="objData"></param>
        /// <param name="encodeMsg">是否对msg编码</param>
        /// <returns></returns>
        public string ReturnJson(bool? success = null, int errorCode = (int) enmErrorCode.Undefined, string msg = "",
            string msgTitle = "", int msgShowType = -1, object objData = null, List<string> imgs = null,
            bool encodeMsg = false, string backMsg = "")
        {
            if (success != null)
                this.success = (bool) success;
            if (errorCode != (int) enmErrorCode.Undefined)
                this.errorCode = errorCode;
            if (this.message == null)
                this.message = new ApiMessage();
            //if(langCode.ToLower() == "en-us" && !string.IsNullOrEmpty(msg))
            //{
            //    //msg=TranslateHelper.GoogleTranslate(msg, "cn-zh", "en-us");
            //    //msgTitle= TranslateHelper.GoogleTranslate(msgTitle, "cn-zh", "en-us");
            //    string msggoogle = translate(msg);
            //    if (!string.IsNullOrEmpty(msggoogle))
            //    {
            //        msg = msggoogle;
            //    }
            //}
            //if (langCode.ToLower() == "en-us" && !string.IsNullOrEmpty(msgTitle))
            //{
            //    //msg=TranslateHelper.GoogleTranslate(msg, "cn-zh", "en-us");
            //    //msgTitle= TranslateHelper.GoogleTranslate(msgTitle, "cn-zh", "en-us");
            //    string msgTitlegoogle = translate(msgTitle);
            //    if (!string.IsNullOrEmpty(msgTitlegoogle))
            //    {
            //        msgTitle = msgTitlegoogle;
            //    }
            //}
            if (!string.IsNullOrEmpty(msg))
            {
                if (encodeMsg)
                    msg = HttpUtility.UrlEncode(msg);
                this.message.content = msg;
            }

            if (!string.IsNullOrEmpty(msgTitle))
            {
                if (encodeMsg)
                    msgTitle = HttpUtility.UrlEncode(msgTitle);
                this.message.title = msgTitle;
            }

            if (msgShowType != -1)
                this.message.showType = msgShowType;
            if (objData != null)
                this.data = objData;
            if (imgs != null)
            {
                this.data2 = imgs;
            }
            else
            {
                this.data2 = "";
            }

            #region 谷歌翻译统一处理2018-3-6 张兵

            this.message.title = translate(this.message.title, language);
            this.message.content = translate(this.message.content, language);
            if (!this.success && this.data != null) //在出错时，有部分消息可能存在此处2018-3-6张兵
                this.data = translate(this.data.ToString(), language);

            #endregion

            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public class transpara
        {
            public string q { get; set; }
            public string target { get; set; }
        }

        public string translate(string source, string langCode = "zh-cn")
        {
            try
            {
                if (string.IsNullOrEmpty(langCode) || string.IsNullOrEmpty(source) || langCode == "zh-cn")
                    return source;
                if (langCode != "en-us")
                    return source;
                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(2);
                transpara tp = new transpara();
                tp.q = get_uft8(source);
                tp.target = "en";
                string postUrl =
                    "https://translation.googleapis.com/language/translate/v2?key=AIzaSyDbOgKz0BfV52LtiR2jPMTk6LGFp2IKgks";
                // string postUrl = "https://translation.googleapis.com/language/translate/v2?key=AIzaSyB9TULXXahldQqYbVHeyFvg5BtQZP46tGQ";
                var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(tp);
                var httpContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                var data = httpClient.PostAsync(postUrl, httpContent).Result.Content.ReadAsStringAsync();
                if (data.Result.Contains("translatedText"))
                {
                    Newtonsoft.Json.Linq.JObject jo =
                        (Newtonsoft.Json.Linq.JObject) Newtonsoft.Json.JsonConvert.DeserializeObject(data.Result);
                    return jo["data"]["translations"][0]["translatedText"].ToString();
                }

                //翻译不成功，则返回原文
                return source;
            }
            catch (Exception)
            {
                return source;
            }
        }

        public static string get_uft8(string unicodeString)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes);
            return decodedString;
        }

        public string Json()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}