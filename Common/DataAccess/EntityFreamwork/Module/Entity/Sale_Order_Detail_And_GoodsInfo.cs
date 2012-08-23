using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EF.Model.DataContext;

namespace EF.Model.Entity
{
    /// <summary>
    /// 订单详细列表
    /// </summary>
    public class Sale_Order_Detail_And_GoodsInfo : Sale_Order_Detail
    {
        /// <summary>
        /// 商品图片url
        /// </summary>
        public string PicUrl { get; set; }
    }
}
