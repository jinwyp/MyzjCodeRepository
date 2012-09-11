using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Alipay.Class;
using Core.ConfigUtility;

namespace Core.Payment
{
    /// <summary>
    /// 支付宝 Wap 支付
    /// </summary>
    public class AlipayWapPayment : IPayment
    {
        #region 常量
        /// <summary>
        /// 
        /// </summary>
        public const string RequestUrl = "http://wappaygw.alipay.com/service/rest.htm";
        /// <summary>
        /// 
        /// </summary>
        public const string ServiceCreate = "alipay.wap.trade.create.direct";
        /// <summary>
        /// 
        /// </summary>
        public const string ServiceAuth = "alipay.wap.auth.authAndExecute";
        /// <summary>
        /// 
        /// </summary>
        public const string SecId = "MD5";
        /// <summary>
        /// 
        /// </summary>
        public const string Format = "xml";
        /// <summary>
        /// 
        /// </summary>
        public const string Version = "2.0";
        /// <summary>
        /// 
        /// </summary>
        public const string InputCharset = "utf-8";
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        private string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NotifyUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MerchantUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CallbackUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Partner { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SellerAccount { get; set; }

        /// <summary>
        /// 支付配置
        /// </summary>
        private PayConfigs PayConfig { get; set; }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="payConfig"></param>
        public AlipayWapPayment(PayConfigs payConfig)
            : this()
        {
            PayConfig = payConfig;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AlipayWapPayment()
        {
            NotifyUrl = MConfigManager.GetAppSettingsValue<string>(MConfigManager.FormatKey("Alipay_NotifyUrl", Enums.MConfigs.ConfigsCategory.Payment));
            MerchantUrl = MConfigManager.GetAppSettingsValue<string>(MConfigManager.FormatKey("Alipay_MerchantUrl", Enums.MConfigs.ConfigsCategory.Payment));
            CallbackUrl = MConfigManager.GetAppSettingsValue<string>(MConfigManager.FormatKey("Alipay_CallbackUrl", Enums.MConfigs.ConfigsCategory.Payment));

            Partner = MConfigManager.GetAppSettingsValue<string>(MConfigManager.FormatKey("Alipay_Partner", Enums.MConfigs.ConfigsCategory.Payment));// "2088201564809153";
            Key = MConfigManager.GetAppSettingsValue<string>(MConfigManager.FormatKey("Alipay_Key", Enums.MConfigs.ConfigsCategory.Payment));// "zpdjh9ywq433ejjnkrbc5pys7ipkosnz";
            SellerAccount = MConfigManager.GetAppSettingsValue<string>(MConfigManager.FormatKey("Alipay_SellerAccount", Enums.MConfigs.ConfigsCategory.Payment));// "alipay-test12@alipay.com";
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public IPayment Init()
        {
            var alipayService = new Alipay.Class.Service();
            Token = alipayService.alipay_wap_trade_create_direct(RequestUrl, PayConfig.Subject, PayConfig.OutTradeNo,
                                                         PayConfig.TotalFee.ToString(CultureInfo.InvariantCulture),
                                                         SellerAccount, NotifyUrl,
                                                         PayConfig.OutUser, MerchantUrl, CallbackUrl, ServiceCreate,
                                                         SecId, Partner, PayConfig.RequestIdentity, Format, Version, InputCharset, RequestUrl,
                                                         Key, SecId);
            return this;
        }

        /// <summary>
        /// 创建请求url
        /// </summary>
        /// <returns></returns>
        public string CreateRequestUrl()
        {
            var alipayService = new Alipay.Class.Service();
            var payUrl = alipayService.alipay_Wap_Auth_AuthAndExecute(RequestUrl, SecId, Partner,
                CallbackUrl, Format, Version, ServiceAuth, Token, InputCharset,
                RequestUrl, Key, SecId);
            return payUrl;
        }

        /// <summary>
        /// 验证 Sign 签名
        /// </summary>
        /// <param name="sortDict"></param>
        /// <returns></returns>
        public bool ValidationSign(SortedDictionary<string, string> sortDict)
        {
            var result = false;
            if (sortDict.Count > 0)
            {
                string sign;
                sortDict.TryGetValue("sign", out sign);
                if (!string.IsNullOrEmpty(sign))
                {
                    var strSign = Function.BuildMysign(sortDict, Key, SecId, InputCharset);
                    result = sign.Equals(strSign, StringComparison.InvariantCultureIgnoreCase);
                }
            }
            return result;
        }
    }
}
