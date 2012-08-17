using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Wcf.Entity.Member;

namespace Wcf.Entity.Order
{
    /// <summary>
    /// 物流数据结构
    /// </summary>
    [DataContract]
    public class Shipping
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public int? oid { get; set; }

        /// <summary>
        /// 发货单ID
        /// </summary>
        [DataMember]
        public string doid { get; set; }

        /// <summary>
        /// 状态 CREATED(订单已创建) RECREATED(订单重新创建) CANCELLED(订单已取消) CLOSED(订单关闭) SENDING(等候发送给物流公司) ACCEPTING(已发送给物流公司,等待接单) ACCEPTED(物流公司已接单) REJECTED(物流公司不接单) PICK_UP(物流公司揽收成功) PICK_UP_FAILED(物流公司揽收失败) LOST(物流公司丢单) REJECTED_BY_RECEIVER(对方拒签) ACCEPTED_BY_RECEIVER(发货方式在线下单：对方已签收；自己联系：卖家已发货)
        /// </summary>
        [DataMember]
        public string status { get; set; }

        /// <summary>
        /// 买家昵称
        /// </summary>
        [DataMember]
        public string buyer_nick { get; set; }

        /// <summary>
        /// 买家字符串id
        /// </summary>
        [DataMember]
        public string buyer_uid { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        [DataMember]
        public string out_sid { get; set; }

        /// <summary>
        /// 收货地址信息
        /// </summary>
        [DataMember]
        public Location location { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        [DataMember]
        public string receiver_name { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        [DataMember]
        public string receiver_phone { get; set; }

        /// <summary>
        /// 收货人手机
        /// </summary>
        [DataMember]
        public string receiver_mobile { get; set; }

        /// <summary>
        /// 物流方式
        /// </summary>
        [DataMember]
        public string type { get; set; }

        /// <summary>
        /// 运费承担方式  buyer(买家承担),seller(卖家承担运费)
        /// </summary>
        [DataMember]
        public string freight_payer { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        [DataMember]
        public string company_name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime? created { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        public DateTime? modified { get; set; }

    }
}
