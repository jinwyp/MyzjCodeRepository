using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wcf.Entity.Member
{
    /// <summary>
    /// 用户位置信息
    /// </summary>
    [DataContract]
    public class Location
    {
        /// <summary>
        /// 邮编
        /// </summary>
        [DataMember]
        public string zip { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [DataMember]
        public string address { get; set; }

        /// <summary>
        /// 所在城市
        /// </summary>
        [DataMember]
        public string city { get; set; }

        /// <summary>
        /// 所在省份
        /// </summary>
        [DataMember]
        public string state { get; set; }

        /// <summary>
        /// 国家名称
        /// </summary>
        [DataMember]
        public string country { get; set; }

        /// <summary>
        /// 区/县
        /// </summary>
        [DataMember]
        public string district { get; set; }

    }
}
