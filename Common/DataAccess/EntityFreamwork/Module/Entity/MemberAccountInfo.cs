using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Model.Entity
{
    /// <summary>
    /// 会员订单统计信息
    /// </summary>
    public class MemberAccountInfo
    {
        /// <summary>
        /// 订单总额
        /// </summary>
        public decimal OrdersTotal { get; set; }

        /// <summary>
        /// 订单总数
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// 当前积分
        /// </summary>
        public int TotalIntegral { get; set; }

        /// <summary>
        /// 当前等级
        /// </summary>
        public int CurrentLevel { get; set; }

        /// <summary>
        /// 下个等级所需积分
        /// </summary>
        public int NextLevelIntegral { get; set; }

        /// <summary>
        /// 下个等级
        /// </summary>
        public int NextLevel { get; set; }

        /// <summary>
        /// 下个等级说明
        /// </summary>
        public string NextLevelRemark { get; set; }

        /// <summary>
        /// 下个等级名称
        /// </summary>
        public string NextLevelName { get; set; }
    }
}
