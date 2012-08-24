﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ServiceModel;
using Wcf.Entity.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wcf.Entity.Member
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [JsonObject]
    public class UserEntity
    {
        /// <summary>
        /// 用户数字ID
        /// </summary>
        [JsonProperty]
        public int user_id { get; set; }

        /// <summary>
        /// 用户字符串ID
        /// </summary>
        [JsonProperty]
        public string uid { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [JsonProperty]
        public string nick { get; set; }

        /// <summary>
        /// 用户性别 可选值:m(男),f(女)
        /// </summary>
        [JsonProperty]
        public string sex { get; set; }

        /// <summary>
        /// 用户当前居住地公开信息。如：location.city获取其中的city数据
        /// </summary>
        [JsonProperty]
        public Location location { get; set; }

        /// <summary>
        /// 用户注册时间
        /// </summary>
        [JsonProperty]
        public DateTime? created { get; set; }

        /// <summary>
        /// 最近登陆时间
        /// </summary>
        [JsonProperty]
        public DateTime? last_login { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [JsonProperty]
        public DateTime? birthday { get; set; }

        /// <summary>
        /// 宝宝生日
        /// </summary>
        [JsonProperty]
        public DateTime? babybirthday { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        [JsonProperty]
        public string type { get; set; }

        /// <summary>
        /// 状态。可选值:normal(正常),inactive(未激活),delete(删除),reeze(冻结),supervise(监管)
        /// </summary>
        [JsonProperty]
        public string status { get; set; }

        /// <summary>
        /// 用户头像地址
        /// </summary>
        [JsonProperty]
        public string avatar { get; set; }

        /// <summary>
        /// 联系人email
        /// </summary>
        [JsonProperty]
        public string email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [JsonProperty]
        public string phone { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [JsonProperty]
        public string mobile { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        [JsonProperty]
        public string userlevel { get; set; }

        /// <summary>
        /// 注册类型
        /// </summary>
        [JsonProperty]
        public string registertype { get; set; }

        /// <summary>
        /// 幸运星
        /// </summary>
        [JsonProperty]
        public string locky { get; set; }

        /// <summary>
        /// 累计订单总数
        /// </summary>
        [JsonProperty]
        public int orderstotal { get; set; }

        /// <summary>
        /// 消费总额
        /// </summary>
        [JsonProperty]
        public decimal consumetotal { get; set; }

    }
}
