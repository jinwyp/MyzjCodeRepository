using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.BaseData
{
    [DataContract]
    public class LogisticsResult
    {
        /// <summary>
        /// 物流信息
        /// </summary>
        [DataMember]
        public ItemLogistics logistics_info { get; set; }

        /// <summary>
        /// 总运费
        /// </summary>
        [DataMember]
        public decimal total_fee { get; set; }

    }
}
