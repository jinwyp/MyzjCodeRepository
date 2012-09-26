using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wcf.Entity.Member
{
    /// <summary>
    /// 地址库返回信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class AddressEntity
    {
        /// <summary>
        /// 地址库ID
        /// </summary>
        [DataMember]
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string name { get; set; }

        /// <summary>
        /// 联系人ID
        /// </summary>
        [DataMember]
        public int contact_id { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        [DataMember]
        public string contact_name { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [DataMember]
        public string province { get; set; }

        /// <summary>
        /// 省份ID
        /// </summary>
        [DataMember]
        public int? province_id { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [DataMember]
        public string city { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        [DataMember]
        public int? city_id { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        [DataMember]
        public string county { get; set; }

        /// <summary>
        /// 国家ID
        /// </summary>
        [DataMember]
        public int? county_id { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [DataMember]
        public string country { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        [DataMember]
        public int? country_id { get; set; }

        /// <summary>
        /// 详细街道地址
        /// </summary>
        [DataMember]
        public string addr { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [DataMember]
        public string zip { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DataMember]
        public string phone { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember]
        public string mobile { get; set; }

        /// <summary>
        /// 地址类型
        /// </summary>
        [DataMember]
        public byte type{ get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [DataMember]
        public string seller_company { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string memo { get; set; }

        /// <summary>
        /// 区域id
        /// </summary>
        [DataMember]
        public int? area_id { get; set; }

        /// <summary>
        /// 配送id
        /// </summary>
        [DataMember]
        public int? deliver_id { get; set; }

        /// <summary>
        /// 支付id
        /// </summary>
        [DataMember]
        public int? pay_id { get; set; }


        /// <summary>
        /// 默认发货地址
        /// </summary>
        [DataMember]
        public bool send_def { get; set; }

        /// <summary>
        /// 默认收货地址
        /// </summary>
        [DataMember]
        public bool get_def { get; set; }

        /// <summary>
        /// 默认退货地址
        /// </summary>
        [DataMember]
        public bool cancel_def { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime created { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public DateTime modify_date { get; set; }

    }
}
