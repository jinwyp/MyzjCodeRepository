using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.Cms
{
    /// <summary>
    /// 公告类
    /// </summary>
    [DataContract]
    public class ItemNotice
    {
        /// <summary>
        /// id
        /// </summary>
        [DataMember]
        public int id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        public string content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime? created { get; set; }
    }
}
