using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Model.Entity
{
    public class Customer
    {
        #region 私有成员
        private int membNo;
        private string userCode = string.Empty;
        private string userName = string.Empty;
        private DateTime? regTime;
        private string createBy = string.Empty;
        private DateTime? updateTime;
        private string updateBy = string.Empty;
        private string cardNO = string.Empty;
        private int? noteFlag;
        private int? userGroupId;
        private int? policymaker;
        private int? referrerMembNo;
        private int? province;
        private int? city;
        private int? district;
        private int? scores;
        private int? valid;
        private string mobileTel = string.Empty;
        private string tel = string.Empty;
        private string email = string.Empty;
        private int? userLevel;
        private int? areaNo;
        private int? regType;
        private int? clusterId;
        private string passward = string.Empty;
        private string nickName = string.Empty;
        //private List<Child> children;
        //private List<FamilyMember> families;
        //private List<Address> addresses;
        private int? sex;
        private string question;
        private string answer;
        private string qq;
        private string msn;
        private DateTime birthday;
        private DateTime firstBuyTime;

        private int orderTotals;

        #endregion


        #region 属性
        /// <summary>
        /// 首购时间
        /// </summary>
        public System.DateTime FirstBuyTime
        {
            get { return firstBuyTime; }
            set { firstBuyTime = value; }
        }

        /// <summary>
        /// 订单时间
        /// </summary>
        public int OrderTotals
        {
            get { return orderTotals; }
            set { orderTotals = value; }
        }

        /// <summary>
        /// 宝宝生日或者预产期
        /// </summary>
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }
        /// <summary>
        /// 属性: 会员内码
        /// </summary>
        public int MembNo
        {
            get
            {
                return this.membNo;
            }
            set
            {
                this.membNo = value;
            }
        }

        /// <summary>
        /// 属性: 会员编号
        /// </summary>
        public string UserCode
        {
            get
            {
                return this.userCode == null ? string.Empty : this.userCode.Trim();
            }
            set
            {
                this.userCode = value;
            }
        }

        /// <summary>
        /// 属性: 会员名称
        /// </summary>
        public string UserName
        {
            get
            {
                return this.userName == null ? string.Empty : this.userName.Trim();
            }
            set
            {
                this.userName = value;
            }
        }

        /// <summary>
        /// 属性: 注册时间
        /// </summary>
        public DateTime? RegTime
        {
            get
            {
                return this.regTime;
            }
            set
            {
                this.regTime = value;
            }
        }

        /// <summary>
        /// 属性: 创建人
        /// </summary>
        public string CreateBy
        {
            get
            {
                return this.createBy == null ? string.Empty : this.createBy.Trim();
            }
            set
            {
                this.createBy = value;
            }
        }

        /// <summary>
        /// 属性: 修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            get
            {
                return this.updateTime;
            }
            set
            {
                this.updateTime = value;
            }
        }

        /// <summary>
        /// 属性: 修改人
        /// </summary>
        public string UpdateBy
        {
            get
            {
                return this.updateBy == null ? string.Empty : this.updateBy.Trim();
            }
            set
            {
                this.updateBy = value;
            }
        }

        /// <summary>
        /// 属性: 卡号
        /// </summary>
        public string CardNO
        {
            get
            {
                return this.cardNO == null ? string.Empty : this.cardNO.Trim();
            }
            set
            {
                this.cardNO = value;
            }
        }

        /// <summary>
        /// 属性: 用户注意标示，【1：极易流失用户，服务重点注意】
        /// </summary>
        public int? NoteFlag
        {
            get
            {
                return this.noteFlag;
            }
            set
            {
                this.noteFlag = value;
            }
        }

        /// <summary>
        /// 属性: 会员级别（1普通，2VIP3000，3VIP5000，4VIP10000）
        /// </summary>
        public int? UserGroupId
        {
            get
            {
                return this.userGroupId;
            }
            set
            {
                this.userGroupId = value;
            }
        }

        /// <summary>
        /// 属性: 首购角色
        /// </summary>
        public int? Policymaker
        {
            get
            {
                return this.policymaker;
            }
            set
            {
                this.policymaker = value;
            }
        }

        /// <summary>
        /// 属性: 推荐人编号
        /// </summary>
        public int? ReferrerMembNo
        {
            get
            {
                return this.referrerMembNo;
            }
            set
            {
                this.referrerMembNo = value;
            }
        }

        /// <summary>
        /// 属性: 省
        /// </summary>
        public int? Province
        {
            get
            {
                return this.province;
            }
            set
            {
                this.province = value;
            }
        }

        /// <summary>
        /// 属性: 城市编号
        /// </summary>
        public int? City
        {
            get
            {
                return this.city;
            }
            set
            {
                this.city = value;
            }
        }

        /// <summary>
        /// 属性: 区/县
        /// </summary>
        public int? District
        {
            get
            {
                return this.district;
            }
            set
            {
                this.district = value;
            }
        }

        /// <summary>
        /// 属性: 积分
        /// </summary>
        public int? Scores
        {
            get
            {
                return this.scores;
            }
            set
            {
                this.scores = value;
            }
        }

        /// <summary>
        /// 属性: 是否有效
        /// </summary>
        public int? Valid
        {
            get
            {
                return this.valid;
            }
            set
            {
                this.valid = value;
            }
        }

        /// <summary>
        /// 属性: 默认手机
        /// </summary>
        public string MobileTel
        {
            get
            {
                return this.mobileTel == null ? string.Empty : this.mobileTel.Trim();
            }
            set
            {
                this.mobileTel = value;
            }
        }

        /// <summary>
        /// 属性: 默认电话
        /// </summary>
        public string Tel
        {
            get
            {
                return this.tel == null ? string.Empty : this.tel.Trim();
            }
            set
            {
                this.tel = value;
            }
        }

        /// <summary>
        /// 属性: 默认邮箱
        /// </summary>
        public string Email
        {
            get
            {
                return this.email == null ? string.Empty : this.email.Trim();
            }
            set
            {
                this.email = value;
            }
        }

        /// <summary>
        /// 属性: 1普通，2星星，3月亮，4太阳
        /// </summary>
        public int? UserLevel
        {
            get
            {
                return this.userLevel;
            }
            set
            {
                this.userLevel = value;
            }
        }

        /// <summary>
        /// 属性: 地域编号
        /// </summary>
        public int? AreaNo
        {
            get
            {
                return this.areaNo;
            }
            set
            {
                this.areaNo = value;
            }
        }

        /// <summary>
        /// 属性: 客户来源（0为ERP电话注册，1为网站注册,2徐汇店注册,3代理商发展客户注册,4慧氏医务）
        /// </summary>
        public int? RegType
        {
            get
            {
                return this.regType;
            }
            set
            {
                this.regType = value;
            }
        }

        /// <summary>
        /// 属性: 
        /// </summary>
        public int? ClusterId
        {
            get
            {
                return this.clusterId;
            }
            set
            {
                this.clusterId = value;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Passward
        {
            get { return passward; }
            set { passward = value; }
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }

        ///// <summary>
        ///// 宝宝集合
        ///// </summary>
        //public List<Child> Children
        //{
        //    get { return children; }
        //    set { children = value; }
        //}

        ///// <summary>
        ///// 家庭成员集合
        ///// </summary>
        //public List<FamilyMember> Families
        //{
        //    get { return families; }
        //    set { families = value; }
        //}

        ///// <summary>
        ///// 地址集合
        ///// </summary>
        //public List<Address> Addresses
        //{
        //    get { return addresses; }
        //    set { addresses = value; }
        //}

        /// <summary>
        /// 性别:0,女;1,男
        /// </summary>
        public int? Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        /// <summary>
        /// 安全问题
        /// </summary>
        public string Question
        {
            get { return question; }
            set { question = value; }
        }

        /// <summary>
        /// 安全答案
        /// </summary>
        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            get { return qq; }
            set { qq = value; }
        }

        /// <summary>
        /// MSN
        /// </summary>
        public string MSN
        {
            get { return msn; }
            set { msn = value; }
        }
        #endregion

    }
}
