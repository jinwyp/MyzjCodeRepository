using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.BaseData
{
    /// <summary>
    /// 物流项目
    /// </summary>
    [Serializable]
    [DataContract]
    public class ItemLogistics
    {
        /// <summary>
        /// 配送 id
        /// </summary>
        [DataMember]
        public int id { get; set; }

        /// <summary>
        /// 配送 名称
        /// </summary>
        [DataMember]
        public string name { get; set; }

        /// <summary>
        /// 配送 备注
        /// </summary>
        [DataMember]
        public string remark { get; set; }
    }
}
