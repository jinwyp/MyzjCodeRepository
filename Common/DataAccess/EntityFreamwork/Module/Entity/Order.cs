using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Model.Entity
{
    public class Order
    {
        #region 私有成员
        //在这里设置字段的默认值
        private int orderNo;
        private string orderCode;
        private DateTime createDate;
        private int userID;
        private string userCode;
        private int orderType;
        private DateTime sendDate;
        private string sendTime;
        private decimal couponAmount;
        private decimal addAmount;
        private decimal carriage;
        private decimal goodsAmount;
        private decimal receAmount;
        private decimal change;
        private decimal weight;
        private int payID;
        private int deliverID;
        private int channel;
        private int totalStars;
        private int orderState;
        private int payState;
        private int createrID;
        private int logisticsID;
        private int stockID;
        private string memo;
        private int spID;

        private List<OrderItem> items;
        #endregion



        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        /// <summary>
        /// 属性: 订单标识
        /// </summary>
        public int OrderNo
        {
            get
            {
                return this.orderNo;
            }
            set
            {
                this.orderNo = value;
            }
        }

        /// <summary>
        /// 属性: 订单编号
        /// </summary>
        public string OrderCode
        {
            get
            {
                return this.orderCode == null ? string.Empty : this.orderCode.Trim();
            }
            set
            {
                this.orderCode = value;
            }
        }

        /// <summary>
        /// 属性: 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return this.createDate;
            }
            set
            {
                this.createDate = value;
            }
        }

        /// <summary>
        /// 属性: 用户编号
        /// </summary>
        public string UserCode
        {
            get
            {
                return this.userCode == null ? string.Empty : this.userCode.Trim();
            }
            set
            {
                this.userCode = value;
            }
        }

        /// <summary>
        /// 属性: 业务类型（1销售订单，5奥优订单，6代理商订单，11客服批发订单，12特殊）
        /// </summary>
        public int OrderType
        {
            get
            {
                return this.orderType;
            }
            set
            {
                this.orderType = value;
            }
        }

        /// <summary>
        /// 属性: 送货日期
        /// </summary>
        public DateTime SendDate
        {
            get
            {
                return this.sendDate;
            }
            set
            {
                this.sendDate = value;
            }
        }

        /// <summary>
        /// 属性: 送货时间
        /// </summary>
        public string SendTime
        {
            get
            {
                return this.sendTime == null ? string.Empty : this.sendTime.Trim();
            }
            set
            {
                this.sendTime = value;
            }
        }

        /// <summary>
        /// 属性: 整单优惠金额
        /// </summary>
        public decimal CouponAmount
        {
            get
            {
                return this.couponAmount;
            }
            set
            {
                this.couponAmount = value;
            }
        }

        /// <summary>
        /// 属性: 附加金额
        /// </summary>
        public decimal AddAmount
        {
            get
            {
                return this.addAmount;
            }
            set
            {
                this.addAmount = value;
            }
        }

        /// <summary>
        /// 属性: 附加金额（运费、保险。。。）
        /// </summary>
        public decimal Carriage
        {
            get
            {
                return this.carriage;
            }
            set
            {
                this.carriage = value;
            }
        }

        /// <summary>
        /// 总货款
        /// </summary>
        public decimal GoodsAmount
        {
            get
            {
                return this.goodsAmount;
            }
            set
            {
                this.goodsAmount = value;
            }
        }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal ReceAmount
        {
            get
            {
                return this.receAmount;
            }
            set
            {
                this.receAmount = value;
            }
        }


        /// <summary>
        /// 属性: 零头
        /// </summary>
        public decimal Change
        {
            get
            {
                return this.change;
            }
            set
            {
                this.change = value;
            }
        }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
            }
        }

        /// <summary>
        /// 付款方式标识
        /// </summary>
        public int PayID
        {
            get
            {
                return this.payID;
            }
            set
            {
                this.payID = value;
            }
        }

        /// <summary>
        /// 送货方式标识
        /// </summary>
        public int DeliverID
        {
            get
            {
                return this.deliverID;
            }
            set
            {
                this.deliverID = value;
            }
        }

        /// <summary>
        /// 渠道编号
        /// </summary>
        public int Channel
        {
            get
            {
                return this.channel;
            }
            set
            {
                this.channel = value;
            }
        }

        /// <summary>
        /// 总幸运星数
        /// </summary>
        public int TotalStars
        {
            get
            {
                return this.totalStars;
            }
            set
            {
                this.totalStars = value;
            }
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderState
        {
            get
            {
                return this.orderState;
            }
            set
            {
                this.orderState = value;
            }
        }

        /// <summary>
        /// 支付状态
        /// </summary>
        public int PayState
        {
            get
            {
                return this.payState;
            }
            set
            {
                this.payState = value;
            }
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreaterID
        {
            get
            {
                return this.createrID;
            }
            set
            {
                this.createrID = value;
            }
        }

        /// <summary>
        /// 物流中心标识
        /// </summary>
        public int LogisticsID
        {
            get
            {
                return this.logisticsID;
            }
            set
            {
                this.logisticsID = value;
            }
        }

        /// <summary>
        /// 仓库标识
        /// </summary>
        public int StockID
        {
            get
            {
                return this.stockID;
            }
            set
            {
                this.stockID = value;
            }
        }

        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary>
        /// 订单备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// SpID价格ID
        /// </summary>
        public int SpID
        {
            get { return spID; }
            set { spID = value; }
        }
    }
}
