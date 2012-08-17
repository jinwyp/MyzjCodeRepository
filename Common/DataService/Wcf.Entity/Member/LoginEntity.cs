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
        [DataMember]
        public string uid { get; set; }
        [DataMember]
        public string pwd { get; set; }
    }
}
