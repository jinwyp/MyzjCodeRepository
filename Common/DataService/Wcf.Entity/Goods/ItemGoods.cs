using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;


namespace Wcf.Entity.Goods
{
    /// <summary>
    /// 商品结构
    /// </summary>
    [JsonObject]
    public class ItemGoods
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [JsonProperty]
        public int gid { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        [JsonProperty]
        public string title { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        [JsonProperty]
        public string pic_url { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [JsonProperty]
        public decimal price { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        [JsonProperty]
        public decimal marketprice { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        [JsonProperty]
        public string productcode { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        [JsonProperty]
        public int score { get; set; }
    }
}
