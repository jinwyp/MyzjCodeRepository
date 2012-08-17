using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Wcf.Entity.Order
{
    /// <summary>
    /// 购物车商品 简单 实体
    /// </summary>
    [JsonObject]
    public class ItemShoppingCartGoodsSmall
    {
        [JsonProperty]
        public int goods_count { get; set; }

        [JsonProperty]
        public decimal goods_total { get; set; }
    }
}
