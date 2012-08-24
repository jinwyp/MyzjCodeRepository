
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using EF.Model.DataContext;
using Core.DataType;
using Core.Enums;

namespace EF.DAL
{
    public partial class Member
    {

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public base_t_member GetMemberInfo(string uid)
        {
            var bbhome = new bbHomeEntities();
            var querylinq = from a in bbhome.base_t_member
                            where a.email.Equals(uid)
                            select a;
            return querylinq.FirstOrDefault();
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public base_t_member GetMemberInfo(int userId)
        {
            var bbhome = new bbHomeEntities();
            var querylinq = from a in bbhome.base_t_member
                            where a.membNo == userId
                            select a;
            return querylinq.FirstOrDefault();
        }

    }
}
