using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.Member
{
    [DataContract]
    public class LoginEntity
    {
        /// <summary>
        /// 用户 名 字符串标识（email）
        /// </summary>
        [DataMember]
        public string uid { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string pwd { get; set; }
    }
}
