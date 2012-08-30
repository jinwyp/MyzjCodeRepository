using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.BaseData
{
    /// <summary>
    /// 方法权限验证
    /// </summary>
    [DataContract]
    public class ItemMethodVerify
    {
        /// <summary>
        /// 方法名
        /// </summary>
        [DataMember]
        public string MethodName { get; set; }
        /// <summary>
        /// 是否验证系统ID
        /// </summary>
        [DataMember]
        public bool IsVerifySystemId { get; set; }
        /// <summary>
        /// 是否验证 Token
        /// </summary>
        [DataMember]
        public bool IsVerifyToken { get; set; }
        /// <summary>
        /// 是否验证数据一致性
        /// </summary>
        [DataMember]
        public bool IsVerifyData { get; set; }
        /// <summary>
        /// 是否验证权限
        /// </summary>
        [DataMember]
        public bool IsVerfiyPemissions { get; set; }
    }
}
