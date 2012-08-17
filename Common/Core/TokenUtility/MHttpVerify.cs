using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Core.ExtMethod;

namespace Core.TokenUtility
{
    public class MHttpVerify
    {
        private readonly string _privateKey = string.Empty;
        private readonly string _publicKey = string.Empty;

        public MHttpVerify(string peKey, string pcKey)
        {
            _privateKey = peKey;
            _publicKey = pcKey;
        }

        /// <summary>
        /// 创建验证 url
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paramNvc"></param>
        /// <returns></returns>
        public string Create(Uri uri, NameValueCollection paramNvc)
        {
            //设置时间刻度
            paramNvc.Add("_mark", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //对url 进行编码
            var url = string.Format("{0}?{1}", uri.ToString(), string.Join("&", paramNvc.AllKeys.Select(key => string.Format("{0}={1}", key, paramNvc[key])).ToArray()));

            //对url进行加密
            var urlToken = FormsAuthentication.HashPasswordForStoringInConfigFile(string.Format("{0}{1}{2}", url, _publicKey, _privateKey), "MD5");

            return string.Format("{0}&_token={1}", url, urlToken);
        }

        /// <summary>
        /// Url 验证
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="timeSpan"></param>
        /// <param name="paramNvc"></param>
        /// <returns></returns>
        public bool Verify(Uri uri, TimeSpan timeSpan, out NameValueCollection paramNvc)
        {
            var result = false;

            NameValueCollection _params = paramNvc = HttpUtility.ParseQueryString(uri.Query.TrimStart('?'));

            var token = _params["_token"] ?? "";
            if (string.IsNullOrEmpty(token)) return false;

            var url = uri.ToString().Substring(0, uri.ToString().LastIndexOf("_token"));
            var mark = _params["_mark"].ToString().MConvertTo<DateTime>(DateTime.MinValue);
            var mToken = FormsAuthentication.HashPasswordForStoringInConfigFile(string.Format("{0}{1}{2}", url, _publicKey, _privateKey), "MD5");
            if (token.Equals(mToken))
            {
                var beginTimeSpan = new TimeSpan(mark.Ticks);
                var endTimeSpan = new TimeSpan(DateTime.Now.Ticks);
                if (endTimeSpan.Subtract(beginTimeSpan) <= timeSpan)
                {
                    result = true;
                }
            }

            return result;
        }

    }
}
