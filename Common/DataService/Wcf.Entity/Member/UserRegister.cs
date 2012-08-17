using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Wcf.Entity.Enum;

namespace Wcf.Entity.Member
{
    /// <summary>
    /// 用户注册
    /// </summary>
    [DataContract]
    public class UserRegister
    {
        /// <summary>
        /// 联系人email
        /// </summary>
        [DataMember]
        public string email { get; set; }

        /// <summary>
        /// 用户字符串ID
        /// </summary>
        [DataMember]
        public string uid { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [DataMember]
        public string pwd { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember]
        public string mobile { get; set; }

        /// <summary>
        /// 宝宝生日
        /// </summary>
        [DataMember]
        public DateTime babybirthday { get; set; }
        
        /// <summary>
        /// 注册类型（来源）
        /// </summary>
        [DataMember]
        public int registertype { get; set; }
    }
}
