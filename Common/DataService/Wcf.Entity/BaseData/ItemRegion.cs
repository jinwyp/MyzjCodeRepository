using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.BaseData
{
    /// <summary>
    /// 地区行数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class ItemRegion
    {
        /// <summary>
        /// 地区id
        /// </summary>
        [DataMember]
        public int id { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        [DataMember]
        public int pid { get; set; }
        /// <summary>
        /// 地区名称
        /// </summary>
        [DataMember]
        public string name { get; set; }
        /// <summary>
        /// 区域类型
        /// </summary>
        [DataMember]
        public string type { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        [DataMember]
        public string zip { get; set; }
        /// <summary>
        /// 简码
        /// </summary>
        [DataMember]
        public string code { get; set; }
        /// <summary>
        /// 子节点总数
        /// </summary>
        [DataMember]
        public int child_total { get; set; }
    }
}
