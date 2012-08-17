using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Wcf.BLL;
using Wcf.BLL.Member;
using Wcf.Entity.Enum;
using Core.DataType;
using Core.Enums;
using Core.Caching;
using Core.NetUtility;
using Core.ConfigUtility;
using Wcf.Entity.Enum;

namespace Wcf.SpringDotNetAdvice.Validate
{
    /// <summary>
    /// 安全验证
    /// </summary>
    public class SecureAuth
    {
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
        private SystemType _systemType;

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
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
        /// 验证数据一致性
        /// </summary>
        /// <returns></returns>
        public MResult VerifyData()
        {
            var result = new MResult();

            var httpContext = HttpContext.Current;
            var md5 = MHttpHelper.GetParam<string>("md5");
            var requestTime = MHttpHelper.GetParam<DateTime>("time", DateTime.MaxValue);
            var systemKey = MConfigManager.GetAppSettingsValue<string>("SystemKey");
            if (!string.IsNullOrEmpty(md5) && !string.IsNullOrEmpty(systemKey) && requestTime != DateTime.MaxValue)
            {
                var newMd5 = string.Empty;
                var uri = httpContext.Request.Url;

                //var url = string.Format("{0}://{1}{2}", uri.Scheme, uri.Host, uri.AbsolutePath);

                var url = uri.ToString().Split('?')[0];

                var getData = new StringBuilder(1000);
                var getParams = httpContext.Request.QueryString;
                foreach (var p in getParams)
                {
                    var key = p.ToString();
                    if (!key.Equals("md5", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var values = getParams.GetValues(key);
                        if (values != null)
                            foreach (var v in values)
                            {
                                getData.AppendFormat("{0}={1}", p, v);
                            }
                    }
                }

                var postData = new StringBuilder(1000);
                var postParams = httpContext.Request.Form;
                foreach (var p in postParams)
                {
                    var key = p.ToString();
                    if (!key.Equals("md5", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var values = postParams.GetValues(key);
                        if (values != null)
                            foreach (var v in values)
                            {
                                postData.AppendFormat("{0}={1}", p, v);
                            }
                    }
                }

                newMd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(string.Format("{0}{1}{2}{3}", url, requestTime, postData, systemKey).ToUpper(), "MD5");

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
        /// 验证系统id
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
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
        /// 验证token
        /// </summary>
        /// <param name="uid"> </param>
        /// <param name="token"></param>
        /// <returns></returns>
        public MResult VerifyToKen(string uid, string token)
        {
            var result = new MResult();
            var cache = MCacheManager.GetCacheObj(MCaching.Provider.Redis);
            if (cache.Contains(token, MCaching.CacheGroup.Member))
            {
                MemberBLL.UpdateUserCache(uid, token);
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
