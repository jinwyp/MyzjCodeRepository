using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ConfigUtility;
using Wcf.Entity.Enum;
using System.Web;
using Core.DataTypeUtility;
using Wcf.SpringDotNetAdvice.Validate;
using Core.Caching;
using Wcf.Entity.BaseData;
using Core.LogUtility;
using Core.Enums;

namespace Wcf.ServiceLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseWcfService
    {
        protected string Uid { get; set; }
        protected string Token { get; set; }
        protected string Guid { get; set; }
        protected int UserId { get; set; }
        protected SystemType SystemType { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseWcfService()
        {
            var httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                var args = httpContext.Request.Url.Segments;
                for (var i = 0; i < args.Length; i++)
                    args[i] = ValidateUtility.CheckNull(args[i].Trim(new char[] { '/', '\\', ' ' })).ToString();
                if (args.Length >= 8)
                {
                    SystemType = MCvHelper.To<SystemType>(args[3]);
                    Token = MCvHelper.To<string>(args[4]);
                    Guid = MCvHelper.To<string>(args[5]);
                    UserId = MCvHelper.To<int>(args[6]);
                    Uid = MCvHelper.To<string>(args[7]);

                    if (!string.IsNullOrWhiteSpace(Token))
                        SecureAuth.RefreshToken(Uid, Token);
                }
                else
                {
                    MLogManager.Error(MLogGroup.WcfService.构造函数, "","", "wcf 服务基类 构造函数初始化 ，uri参数错误");
                }
            }
        }
    }
}
