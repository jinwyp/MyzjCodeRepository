using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Security;
using Core.Caching;
using Core.ConfigUtility;
using Core.DataType;
using Core.Enums;
using Core.NetUtility;
using Wcf.BLL.Member;
using Wcf.Entity.Enum;

namespace Wcf.SpringDotNetAdvice.Validate
{
    /// <summary>
    ///   安全验证
    /// </summary>
    public class SecureAuth
    {
        private SystemType _systemType;
        public bool IsVerifyData { get; set; }
        public bool IsVerifySystemId { get; set; }
        public bool IsVerifyToKen { get; set; }
        public bool IsVerifyPermissions { get; set; }

        public string Sid { get; set; }
        public string Token { get; set; }
        public string Uid { get; set; }
        public int UserId { get; set; }

        public SystemType SystemType
        {
            get { return _systemType; }
        }

        /// <summary>
        ///   验证
        /// </summary>
        /// <returns> </returns>
        public MResult Verify()
        {
            var result = new MResult();

            MResult dataSuccess = null,
                    toKenSuccess = null,
                    systemIdSuccess = null;

            dataSuccess = VerifyData();

            if (IsVerifyToKen)
                toKenSuccess = VerifyToKen(Uid, Token);
            if (IsVerifySystemId)
                systemIdSuccess = VerifySystemId(Sid);

            if ((!IsVerifyData || (dataSuccess != null && dataSuccess.status == MResultStatus.Success)) &&
                (!IsVerifyToKen || (toKenSuccess != null && toKenSuccess.status == MResultStatus.Success)) &&
                (!IsVerifySystemId || (systemIdSuccess != null && systemIdSuccess.status == MResultStatus.Success)))
            {
                result.status = MResultStatus.Success;
            }
            else
            {
                result.status = MResultStatus.LogicError;
            }

            return result;
        }

        /// <summary>
        ///   验证数据一致性
        /// </summary>
        /// <returns> </returns>
        public MResult VerifyData()
        {
            var result = new MResult();

            HttpContext httpContext = HttpContext.Current;
            var md5 = MHttpHelper.GetParam<string>("md5");
            DateTime requestTime = MHttpHelper.GetParam("time", DateTime.MaxValue);
            var systemKey = MConfigManager.GetAppSettingsValue<string>("SystemKey");
            if (!string.IsNullOrEmpty(md5) && !string.IsNullOrEmpty(systemKey) && requestTime != DateTime.MaxValue)
            {
                string newMd5 = string.Empty;
                Uri uri = httpContext.Request.Url;

                //var url = string.Format("{0}://{1}{2}", uri.Scheme, uri.Host, uri.AbsolutePath);

                string url = uri.ToString().Split('?')[0];

                var getData = new StringBuilder(1000);
                NameValueCollection getParams = httpContext.Request.QueryString;
                foreach (object p in getParams)
                {
                    string key = p.ToString();
                    if (!key.Equals("md5", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string[] values = getParams.GetValues(key);
                        if (values != null)
                            foreach (string v in values)
                            {
                                getData.AppendFormat("{0}={1}", p, v);
                            }
                    }
                }

                var postData = new StringBuilder(1000);
                NameValueCollection postParams = httpContext.Request.Form;
                foreach (object p in postParams)
                {
                    string key = p.ToString();
                    if (!key.Equals("md5", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string[] values = postParams.GetValues(key);
                        if (values != null)
                            foreach (string v in values)
                            {
                                postData.AppendFormat("{0}={1}", p, v);
                            }
                    }
                }

                newMd5 =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(
                        string.Format("{0}{1}{2}{3}", url, requestTime, postData, systemKey).ToUpper(), "MD5");

                if (md5.Equals(newMd5, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.status = MResultStatus.Success;
                }
                else
                {
                    result.msg += "提交的数据验证未通过，是否修改了参数！";
                    result.status = MResultStatus.ParamsError;
                }
            }
            else
            {
                result.msg += "提交的数据验证未通过，没有验证代码 md5！";
                result.status = MResultStatus.ParamsError;
            }
            return result;
        }

        /// <summary>
        ///   验证系统id
        /// </summary>
        /// <param name="sid"> </param>
        /// <returns> </returns>
        public MResult VerifySystemId(string sid)
        {
            SystemType systemType;
            var result = new MResult();
            if (Enum.TryParse(sid, out systemType))
            {
                _systemType = systemType;
                result.status = MResultStatus.Success;
            }
            else
            {
                result.msg += "系统代码不正确！";
                result.status = MResultStatus.ParamsError;
            }
            return result;
        }

        /// <summary>
        ///   验证token
        /// </summary>
        /// <param name="uid"> </param>
        /// <param name="token"> </param>
        /// <returns> </returns>
        public static MResult VerifyToKen(string uid, string token)
        {
            var result = new MResult();
            ICache cache = MCacheManager.GetCacheObj();
            if (cache.Contains(token, MCaching.CacheGroup.Member))
            {
                result.status = MResultStatus.Success;
            }
            else
            {
                result.msg += "请登录系统！";
                result.status = MResultStatus.LogicError;
            }
            return result;
        }

        /// <summary>
        ///   刷新 token
        /// </summary>
        /// <param name="uid"> </param>
        /// <param name="token"> </param>
        /// <returns> </returns>
        public static MResult RefreshToken(string uid, string token)
        {
            var result = new MResult();
            MResult isExists = VerifyToKen(uid, token);
            if (isExists.status == MResultStatus.Success)
            {
                MemberBLL.RefreshUserToken(uid, token);
                result.status = MResultStatus.Success;
            }
            else
            {
                result.msg += "请登录系统！";
                result.status = MResultStatus.LogicError;
            }
            return result;
        }
    }
}