using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wcf.Entity.Member
{
    /// <summary>
    /// 登录密码实体 用户修改登录密码
    /// </summary>
    [DataContract]
    public class LoginPasswordEntity
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string email { get; set; }
        /// <summary>
        /// 找回密码 id
        /// </summary>
        [DataMember]
        public int emailid { get; set; }
        /// <summary>
        /// 找回密码 key
        /// </summary>
        [DataMember]
        public string emailkey { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [DataMember]
        public string password { get; set; }
    }
}
