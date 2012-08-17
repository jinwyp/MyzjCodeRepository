using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Wcf.Entity.Member;


namespace Wcf.Entity.Order
{
    /// <summary>
    /// 购物车
    /// </summary>
    [DataContract]
    public class ShoppingCartEntity
    {

        [DataMember]
        public decimal sub_total
        {
            get;
            set;
        }

        /// <summary>
        /// /渠道id
        /// </summary>
        [DataMember]
        public int? channel_id
        {
            get;
            set;
        }

        /// <summary>
        /// 商品id
        /// </summary>
        [DataMember]
        public int? product_id
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为赠品
        /// </summary>
        [DataMember]
        public bool is_gift
        {
            get;
            set;
        }

        /// <summary>
        /// 原价
        /// </summary>
        [DataMember]
        public decimal org_price
        {
            get;
            set;
        }

        /// <summary>
        /// 销售价
        /// </summary>
        [DataMember]
        public decimal sale_price
        {
            get;
            set;
        }

        /// <summary>
        /// 所得积分
        /// </summary>
        [DataMember]
        public int? score
        {
            get;
            set;
        }

        /// <summary>
        /// 原可得积分
        /// </summary>
        [DataMember]
        public int? org_score
        {
            get;
            set;
        }

        /// <summary>
        /// 套装id
        /// </summary>
        [DataMember]
        public int? suit_id
        {
            get;
            set;
        }

        /// <summary>
        /// 套装名称
        /// </summary>
        [DataMember]
        public string suit_name
        {
            get;
            set;
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        [DataMember]
        public DateTime? add_time
        {
            get;
            set;
        }

        /// <summary>
        /// 分类id
        /// </summary>
        [DataMember]
        public int? category_id
        {
            get;
            set;
        }

        /// <summary>
        /// sessionid
        /// </summary>
        [DataMember]
        public string session_id
        {
            get;
            set;
        }

        /// <summary>
        /// 品牌id
        /// </summary>
        [DataMember]
        public int? brand_id
        {
            get;
            set;
        }

        /// <summary>
        /// 标签id
        /// </summary>
        [DataMember]
        public int? tag_id
        {
            get;
            set;
        }

        /// <summary>
        /// 会员等级id
        /// </summary>
        [DataMember]
        public int? cluster_id
        {
            get;
            set;
        }

        public string product_name
        {
            get;
            set;
        }

        /// <summary>
        /// 成本
        /// </summary>
        [DataMember]
        public decimal cost
        {
            get;
            set;
        }

        /// <summary>
        /// 去税成本
        /// </summary>
        [DataMember]
        public decimal clean_cost
        {
            get;
            set;
        }

        /// <summary>
        /// 购物车ID
        /// </summary>
        [DataMember]
        public int intShopCartID
        {
            get;
            set;
        }
        /// <summary>
        /// 渠道ID
        /// </summary>
        [DataMember]
        public int intChannelID
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public int intUserID
        {
            get;
            set;
        }
        /// <summary>
        /// 产品ID
        /// </summary>
        [DataMember]
        public int intProductID
        {
            get;
            set;
        }
        /// <summary>
        /// 购买数量
        /// </summary>
        [DataMember]
        public int intBuyCount
        {
            get;
            set;
        }
        /// <summary>
        /// 是否是礼物
        /// </summary>
        [DataMember]
        public byte chrIsGift
        {
            get;
            set;
        }
        /// <summary>
        /// 销售价
        /// </summary>
        [DataMember]
        public decimal numSalePrice
        {
            get;
            set;
        }
        /// <summary>
        /// 原始价
        /// </summary>
        [DataMember]
        public decimal numOrgPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 幸运星
        /// </summary>
        [DataMember]
        public int intScore
        {
            get;
            set;
        }
        /// <summary>
        /// 原始幸运星
        /// </summary>
        [DataMember]
        public int intOrgScore
        {
            get;
            set;
        }
        /// <summary>
        /// 套装ID
        /// </summary>
        [DataMember]
        public Nullable<int> intSuitID
        {
            get;
            set;
        }
        /// <summary>
        /// 套装名称
        /// </summary>
        [DataMember]
        public string nchSuitName
        {
            get;
            set;
        }
        /// <summary>
        /// 增加时间
        /// </summary>
        [DataMember]
        public System.DateTime dtAddTime
        {
            get;
            set;
        }
        /// <summary>
        /// 分类ID
        /// </summary>
        [DataMember]
        public int intCateId
        {
            get;
            set;
        }
        /// <summary>
        /// 品牌ID
        /// </summary>
        [DataMember]
        public int intBrandID
        {
            get;
            set;
        }
        /// <summary>
        /// 票识ID
        /// </summary>
        [DataMember]
        public Nullable<int> intTagID
        {
            get;
            set;
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        [DataMember]
        public string nchProductName
        {
            get;
            set;
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        public byte intIsDelete
        {
            get;
            set;
        }
        /// <summary>
        /// 促销数量
        /// </summary>
        [DataMember]
        public Nullable<int> intPromCount
        {
            get;
            set;
        }
        /// <summary>
        /// 成本价
        /// </summary>
        [DataMember]
        public decimal numCost
        {
            get;
            set;
        }
        /// <summary>
        /// 扣税成本
        /// </summary>
        [DataMember]
        public decimal numCleanCost
        {
            get;
            set;
        }
        /// <summary>
        /// 目录册ID
        /// </summary>
        [DataMember]
        public string vchProductPrinted
        {
            get;
            set;
        }
        /// <summary>
        /// 介格ID
        /// </summary>
        [DataMember]
        public int intPriceID
        {
            get;
            set;
        }
        /// <summary>
        /// 产品图片
        /// </summary>
        [DataMember]
        public string vchPicURL
        {
            get;
            set;
        }
        /// <summary>
        /// 总重量
        /// </summary>
        [DataMember]
        public Nullable<int> intWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 赠品类型
        /// </summary>
        [DataMember]
        public Nullable<int> intGiftType
        {
            get;
            set;
        }
        /// <summary>
        /// 促销ID
        /// </summary>
        [DataMember]
        public Nullable<int> intPromID
        {
            get;
            set;
        }
        /// <summary>
        /// 区域ID
        /// </summary>
        [DataMember]
        public Nullable<int> intAreaID
        {
            get;
            set;
        }
        /// <summary>
        /// 优惠码
        /// </summary>
        [DataMember]
        public string vchCoponCode
        {
            get;
            set;
        }
        /// <summary>
        /// Guid临时用户ID
        /// </summary>
        [DataMember]
        public string vchGuid
        {
            get;
            set;
        }
        /// <summary>
        /// 产品Code
        /// </summary>
        [DataMember]
        public string vchproductcode
        {
            get;
            set;
        }
        /// <summary>
        /// 商品类型
        /// </summary>
        [DataMember]
        public Nullable<int> intProductType
        {
            get;
            set;
        }
        /// <summary>
        /// 市场价
        /// </summary>
        [DataMember]
        public Nullable<decimal> numMarketPrice
        {
            get;
            set;
        }

    }
}
