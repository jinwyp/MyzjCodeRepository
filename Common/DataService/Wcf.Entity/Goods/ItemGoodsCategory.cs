using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.Goods
{
    /// <summary>
    /// 商品分类
    /// </summary>
    [Serializable]
    [DataContract]
    public class ItemGoodsCategory
    {
        /// <summary>
        /// 分类id
        /// </summary>
        [DataMember]
        public int id { get; set; }
        /// <summary>
        /// 分类 名称
        /// </summary>
        [DataMember]
        public string name { get; set; }
        /// <summary>
        /// 分类 父级 id
        /// </summary>
        [DataMember]
        public int pid { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        [DataMember]
        public List<ItemGoodsCategory> child { get; set; }
    }
}
