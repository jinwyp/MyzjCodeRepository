
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using EF.Model.DataContext;
using Core.DataType;
using Core.Enums;
using EF.Model.Entity;

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

        /// <summary>
        /// 获取会员账户信息
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public MemberAccountInfo GetMemberLevelInfo(base_t_member memberInfo, MemberAccountInfo orderInfo)
        {
            if (memberInfo != null && memberInfo.membNo > 0 && orderInfo != null)
            {
                var levelList = new List<KeyValuePair<int, string>>();
                levelList.Add(new KeyValuePair<int, string>(5000, "普通会员"));
                levelList.Add(new KeyValuePair<int, string>(15000, "星星宝宝"));
                levelList.Add(new KeyValuePair<int, string>(30000, "月亮宝宝"));
                levelList.Add(new KeyValuePair<int, string>(99999, "太阳宝宝"));

                var currentLevel = (memberInfo.userLevel ?? 0);
                var currentLevelName = levelList[(memberInfo.userLevel ?? 0) - 1].Value;
                var currentIntegral = (memberInfo.scores ?? 0);

                if (currentLevel < 4)
                {
                    var nextLevel = currentLevel + 1;
                    var nextLevelName = levelList[nextLevel - 1].Value;
                    var nextLevelIntegral = levelList[nextLevel - 1].Key - currentIntegral;
                    return new MemberAccountInfo
                    {
                        CurrentLevel = currentLevel,
                        NextLevel = nextLevel,
                        NextLevelRemark = string.Format("{0}颗（再累积{1}颗幸运星，就能成为{2}享受更多优惠！）", currentIntegral, nextLevelIntegral, nextLevelName),
                        NextLevelIntegral = nextLevelIntegral,
                        NextLevelName = currentLevelName
                    };
                }
                else
                {
                    return new MemberAccountInfo
                    {
                        CurrentLevel = currentLevel,
                        NextLevel = 0,
                        NextLevelRemark = "",
                        NextLevelIntegral = 0,
                        NextLevelName = currentLevelName
                    };
                }


            }
            return null;
        }

    }
}
