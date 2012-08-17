using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.Order
{
    [DataContract]
    public class ShoppingCartResult
    {
        /// <summary>
        /// 购物车商品列表
        /// </summary>
        [DataMember]
        public List<ShoppingCartEntity> shoppingcart_list { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        [DataMember]
        public decimal shoppingcart_total { get; set; }
    }
}
