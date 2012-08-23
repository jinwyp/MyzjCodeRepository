using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Wcf.Entity.Goods;

namespace Wcf.Entity.Order
{
    /// <summary>
    /// 订单商品信息
    /// </summary>
    [DataContract]
    public class ItemOrderGoods : ItemGoods
    {
        /// <summary>
        /// 套餐ID
        /// </summary>
        [DataMember]
        public int meal_id { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        [DataMember]
        public string meal_name { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        [DataMember]
        public int num { get; set; }

        /// <summary>
        /// 商品总额
        /// </summary>
        [DataMember]
        public decimal total { get; set; }
    }
}
