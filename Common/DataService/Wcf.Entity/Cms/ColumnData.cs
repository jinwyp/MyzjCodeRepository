using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.Cms
{
    /// <summary>
    /// 栏位数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class ColumnData
    {
        /// <summary>
        /// 栏位数据id
        /// </summary>
        [DataMember]
        public int id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string title { get; set; }

        /// <summary>
        /// 图片url
        /// </summary>
        [DataMember]
        public string pic_url { get; set; }

        /// <summary>
        /// 栏位连接
        /// </summary>
        [DataMember]
        public string link { get; set; }

        /// <summary>
        /// 栏位内容
        /// </summary>
        [DataMember]
        public string content { get; set; }

        /// <summary>
        /// 资源id
        /// </summary>
        [DataMember]
        public int? resid { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        [DataMember]
        public int? restype { get; set; }

        /// <summary>
        /// 附加字段1
        /// </summary>
        [DataMember]
        public string f1 { get; set; }
        /// <summary>
        /// 附加字段2
        /// </summary>
        [DataMember]
        public string f2 { get; set; }
        /// <summary>
        /// 附加字段3
        /// </summary>
        [DataMember]
        public string f3 { get; set; }
        /// <summary>
        /// 附加字段4
        /// </summary>
        [DataMember]
        public string f4 { get; set; }
        /// <summary>
        /// 附加字段5
        /// </summary>
        [DataMember]
        public string f5 { get; set; }
        /// <summary>
        /// 附加字段6
        /// </summary>
        [DataMember]
        public string f6 { get; set; }
        /// <summary>
        /// 附加字段7
        /// </summary>
        [DataMember]
        public string f7 { get; set; }
        /// <summary>
        /// 附加字段8
        /// </summary>
        [DataMember]
        public string f8 { get; set; }
        /// <summary>
        /// 附加字段9
        /// </summary>
        [DataMember]
        public string f9 { get; set; }
    }
}
