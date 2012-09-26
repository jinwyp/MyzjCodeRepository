using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wcf.Entity.Goods
{
    /// <summary>
    /// 产品结构
    /// </summary>
    [Serializable]
    [DataContract]
    public class Product
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [DataMember]
        public int? product_id { get; set; }

        /// <summary>
        /// 产品code
        /// </summary>
        [DataMember]
        public string product_code { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime? created { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        [DataMember]
        public int? cid { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [DataMember]
        public string cname { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        [DataMember]
        public int? bid { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        [DataMember]
        public string bname { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string name { get; set; }

        /// <summary>
        /// 销售价格
        /// </summary>
        [DataMember]
        public decimal? saleprice { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string desc { get; set; }

        /// <summary>
        /// 更改时间
        /// </summary>
        [DataMember]
        public DateTime? modified { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        [DataMember]
        public List<ProductImg> product_imgs { get; set; }

        /// <summary>
        /// 产品状态 0下架；1上架
        /// </summary>
        [DataMember]
        public int? status { get; set; }

        /// <summary>
        /// 产品主图
        /// </summary>
        [DataMember]
        public string pic_path { get; set; }

    }
}
