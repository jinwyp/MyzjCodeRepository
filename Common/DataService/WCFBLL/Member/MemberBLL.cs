using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using EF.Model.Entity;
using Factory;
using WCFService;
using Wcf.BLL.ServiceReference.External;
using Wcf.Entity.Member;
using EF.Model.DataContext;
using Core.Caching;
using Core.DataType;
using Core.Enums;
using System.Security.Cryptography;
using Core.LogUtility;
using Core.DataTypeUtility;
using Core.Mail;
using Core;

using Wcf.BLL.ShoppingCart;
using Wcf.Entity.Enum;
using System.Xml.Linq;

namespace Wcf.BLL.Member
{
    public static partial class MemberBLL
    {

        /// <summary>
        /// 创建用户令牌
        /// </summary>
        /// <returns></returns>
        public static string CreateUserToKen()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 更新用户 Session
        /// </summary>
        /// <param name="uid">用户 字符串id（此处邮箱） 如果是更新用户状态可以为空，如果是登录则必填</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static MResult<int> RefreshUserToken(string uid, string token)
        {
            var result = new MResult<int>();

            try
            {
                var cache = MCacheManager.GetCacheObj();
                if (cache.Contains(token, MCaching.CacheGroup.Member))
                {
                    uid = cache.GetValByKey<string>(token, MCaching.CacheGroup.Member);
                    cache.RemoveByKey(token, MCaching.CacheGroup.Member);
                }

                if (!string.IsNullOrEmpty(uid))
                {
                    if (cache.Set<string>(token, MCaching.CacheGroup.Member, uid, DateTime.Now.AddMinutes(30)))
                    {
                        result.status = MResultStatus.Success;
                        result.data = token;
                    }
                    else
                    {
                        result.status = MResultStatus.ExecutionError;
                        result.msg = "[" + uid + "]缓存用户状态失败！";
                    }
                }
                else
                {
                    result.status = MResultStatus.ExecutionError;
                    result.msg = "缓存用户状态 uid 不能为空！";
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Member.登陆, null, null, "更新用户状态缓存失败！", ex);
            }
            return result;
        }

        /// <summary>
        /// 移除用户 Session
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static MResult RemoveUserCache(string uid, string token)
        {
            var result = new MResult();

            var memberObj = Factory.DALFactory.Member();
            if (memberObj != null)
            {
                var userInfo = new UserEntity();

                if (MCacheManager.GetCacheObj().RemoveByKey(token, MCaching.CacheGroup.Member))
                    result.status = MResultStatus.Success;
                else
                {
                    result.status = MResultStatus.ExecutionError;
                    result.msg = "[" + uid + "]移除用户缓存失败！";
                }
            }
            else
            {
                result.status = MResultStatus.ParamsError;
                result.msg = "[" + uid + "]用户不存在！";
            }

            return result;
        }

        /// <summary>
        /// 检查用户登录是否过期
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool CheckUserState(string token)
        {
            var cache = MCacheManager.GetCacheObj();
            return cache.Contains(token, MCaching.CacheGroup.Member);
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static MResult ResetLoginPassword(SystemType sType, string uid)
        {

            var result = new MResult();

            try
            {
                var memberDal = DALFactory.Member();
                var memberInfo = memberDal.GetMemberInfo(uid);
                if (memberInfo != null && memberInfo.membNo > 0)
                {
                    var templatePath = HttpContext.Current.Server.MapPath("/Template/" + sType + "/ForgotPassword.xml");
                    var templateInit = false;
                    if (File.Exists(templatePath))
                    {
                        var xdom = XElement.Load(templatePath);
                        var titleElement = xdom.Element("Title");
                        var bodyElement = xdom.Element("Body");

                        if (titleElement != null && bodyElement != null)
                        {
                            var emailTitle = titleElement.Value.Replace("\r", "").Replace("\n", "").Trim();
                            var emailBody = bodyElement.Value.Replace("\r", "").Replace("\n", "").Trim();

                            if (!string.IsNullOrEmpty(emailTitle) && !string.IsNullOrEmpty(emailBody))
                            {
                                templateInit = true;
                                string mailKey;
                                int mailId;

                                #region 写入 找回密码 记录到数据库

                                using (var bbHome = new BbHomeServiceClient())
                                {
                                    bbHome.Open();
                                    string message;
                                    if (!bbHome.CheckPswKeyStatusByEmail(out mailId, out mailKey, out message, uid))
                                    {
                                        bbHome.InsertPswKey(out mailId, out mailKey, out message, uid);
                                    }
                                    bbHome.Close();
                                }

                                #endregion

                                var mailBody =
                                    emailBody.Replace("{%MAILID%}", mailId.ToString()).Replace("{%MAIL%}", uid).Replace(
                                        "{%MAILKEY%}", HttpUtility.UrlEncode(mailKey));

                                var wcfmail = new WcfMail
                                                  {
                                                      MailTo = uid.Trim().Split(new[] { ',', ';' }),
                                                      IsHtml = true,
                                                      Subject = emailTitle,
                                                      Body = mailBody
                                                  };

                                using (var wcfClient = new EmailServiceClient())
                                {
                                    wcfClient.Open();
                                    wcfClient.SendCmail(wcfmail);
                                    wcfClient.Close();
                                }
                                result.status = MResultStatus.Success;
                            }
                        }
                    }
                    if (!templateInit)
                    {
                        result.status = MResultStatus.LogicError;
                        result.msg = "模板不存在";
                    }
                }
                else
                {
                    result.status = MResultStatus.LogicError;
                    result.msg = "邮箱不存在！";
                }
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                result.msg = "重置登录密码 出错！";
            }

            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sType"> </param>
        /// <param name="loginPassword"> </param>
        /// <returns></returns>
        public static MResult ChangeLoginPassword(SystemType sType, LoginPasswordEntity loginPassword)
        {
            var result = new MResult();

            if (loginPassword != null &&
                !string.IsNullOrEmpty(loginPassword.email) &&
                !string.IsNullOrEmpty(loginPassword.emailkey) &&
                loginPassword.emailid > 0)
            {
                var changeSuccess = false;
                string message;
                using (var bbHome = new BbHomeServiceClient())
                {
                    bbHome.Open();
                    int mailId;
                    string mailKey;
                    if (bbHome.CheckPswKeyStatusByEmail(out mailId, out mailKey, out message, loginPassword.email))
                    {
                        changeSuccess = bbHome.ChangePassWordByEmail(out message, loginPassword.email, loginPassword.password);
                    }
                    bbHome.Close();
                }
                if (changeSuccess)
                {
                    result.status = MResultStatus.Success;
                }
                else
                {
                    result.status = MResultStatus.LogicError;
                    result.msg = message;
                }
            }

            return result;
        }

        /// <summary>
        /// 注册会员
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="user"></param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        public static MResult<int> RegisterMember(string guid, int channelId, UserRegister user)
        {
            var result = new MResult<int>();
            var member = new ServiceReference.External.base_t_member();
            using (var se = new ServiceReference.External.BbHomeServiceClient())
            {
                try
                {
                    user.registertype = channelId;

                    se.Open();
                    string msg;
                    member.email = user.email;
                    member.passward = user.pwd;
                    member.clusterId = 1; //会员等级:普通
                    member.userLevel = 1;
                    member.regType = user.registertype; //注册类型:网站
                    member.scores = 0; //幸运星
                    member.createBy = "555";
                    member.mobileTel = user.mobile;
                    if (se.InsertCustomer(out msg, member, user.babybirthday))
                    {
                        //注册成功后直接登陆
                        result = LoginMember(guid, channelId, member.email, member.passward);
                        result.status = MResultStatus.Success;
                        result.msg = "注册成功";
                    }
                    else
                    {
                        result.status = MResultStatus.ExecutionError;
                        result.msg = msg;
                    }

                    se.Close();
                }
                catch (Exception ex)
                {
                    result.status = MResultStatus.LogicError;
                }
            }
            return result;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="channelId"> </param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        public static MResult<int> LoginMember(string guid, int channelId, string uid, string pwd)
        {
            var result = new MResult<int>();
            try
            {
                var token = CreateUserToKen();
                var memberObj = Factory.DALFactory.Member();
                var passWord = MEncryptUtility.MD5Encrypt(pwd, 16);
                var memberInfo = memberObj.GetMemberInfo(uid);
                if (memberInfo != null)
                {
                    if (memberInfo.passward.Equals(passWord, StringComparison.OrdinalIgnoreCase) && memberInfo.valid == 1)
                    {
                        result = RefreshUserToken(uid, token);
                        if (result.status == MResultStatus.Success)
                        {
                            result.msg = "成功登录 ！";
                        }
                        else
                        {
                            result.data = token;
                            result.status = MResultStatus.Success;
                            result.msg = "成功登录，缓存失败" + result.msg;
                        }
                        ShoppingCartBll.MergeShoppingCartGoods(guid, channelId, uid, memberInfo.membNo);
                    }
                    else
                    {
                        result.status = MResultStatus.ExecutionError;
                        result.msg = "用户名或密码错误！";
                    }
                    result.info = memberInfo.membNo;
                    result.data = token;
                }
                else
                {
                    result.status = MResultStatus.ExecutionError;
                    result.msg = "用户不存在！";
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Member.登陆, channelId + "", null, "获取数据错误");
            }
            return result;
        }

        /// <summary>
        /// 重新登录
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static MResult ReLoginMember(string sid, string uid, string token)
        {
            var result = new MResult();
            try
            {
                var memberObj = Factory.DALFactory.Member();
                var memberInfo = memberObj.GetMemberInfo(uid);
                var cache = MCacheManager.GetCacheObj();
                if (memberInfo != null && memberInfo.valid == 1)
                {
                    var uId = cache.GetValByKey<string>(token, MCaching.CacheGroup.Member);
                    if (uId != null && !string.IsNullOrEmpty(uId))
                    {
                        result = RefreshUserToken(uid, token);
                        if (result.status == MResultStatus.Success)
                        {
                            result.msg = "登录成功！";
                        }
                        else
                        {
                            result.data = token;
                            result.status = MResultStatus.Success;
                            result.msg = "用户名密码正确，缓存失败" + result.msg;
                        }
                    }
                    else
                        result.msg = "token 不存在，请重新登录";
                }
                else
                {
                    result.status = MResultStatus.ExecutionError;
                    result.msg = "用户不存在！";
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Member.登陆, sid, uid, "获取数据错误");
            }
            return result;
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static MResult LogOut(string sid, string uid, string token)
        {
            var result = new MResult();
            try
            {
                result = RemoveUserCache(uid, token);
                if (result.status == MResultStatus.Success)
                {
                    result.msg = "注销成功！";
                }
                else
                {
                    result.status = MResultStatus.Success;
                    result.msg = "注销成功，清除缓存失败" + result.msg;
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Member.登陆, sid,uid, "获取数据错误");
            }
            return result;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static MResult<UserEntity> GetMemberInfo(string uid)
        {
            var result = new MResult<UserEntity>();
            var memberObj = Factory.DALFactory.Member();
            var orderDal = DALFactory.Order();

            var userInfo = new UserEntity();
            var memberEntity = memberObj.GetMemberInfo(uid);

            if (memberEntity != null)
            {
                var orderInfo = orderDal.GetMemberOrderStatistics(memberEntity.membNo);
                var memberLevelInfo = memberObj.GetMemberLevelInfo(memberEntity, orderInfo);

                userInfo.user_id = memberEntity.membNo;
                userInfo.uid = memberEntity.email;
                userInfo.sex = memberEntity.sex.ToString();
                userInfo.nick = memberEntity.userName;
                userInfo.location = new Location { zip = "200000" };
                userInfo.created = memberEntity.regTime;
                userInfo.birthday = DateTime.Now;
                userInfo.babybirthday = DateTime.Now;
                userInfo.type = "网站用户";
                userInfo.status = "正常";
                userInfo.avatar = "";
                userInfo.email = memberEntity.email;
                userInfo.mobile = memberEntity.mobileTel;
                userInfo.registertype = memberEntity.regType.ToString();

                if (memberLevelInfo != null)
                {
                    userInfo.userlevel = memberLevelInfo.NextLevelName;
                    userInfo.locky = memberLevelInfo.NextLevelRemark;
                    userInfo.consumetotal = memberLevelInfo.OrdersTotal;
                }
                else
                {
                    userInfo.userlevel = memberEntity.userLevel.ToString();
                }

                result.info = userInfo;
                result.status = MResultStatus.Success;
            }
            else
            {
                result.status = MResultStatus.LogicError;
                result.msg = "该用户不存在！";
            }
            return result;
        }

        /// <summary>
        /// 获取用户收货地址里列表
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static MResultList<AddressEntity> GetMemberAddressList(int user_id)
        {
            var result = new MResultList<AddressEntity>(true);
            try
            {
                var member = DALFactory.Member();
                var list = member.GetMemberAddressList(user_id);
                list.ForEach(item =>
                                 {
                                     try
                                     {
                                         result.list.Add(new AddressEntity
                                                             {
                                                                 id = item.intAddressID,
                                                                 contact_id = item.intUserID,
                                                                 contact_name = item.vchConsignee,
                                                                 name = item.vchCAName,
                                                                 phone = item.vchPhone,
                                                                 mobile = item.vchMobile,
                                                                 type = item.intAddrType,
                                                                 province_id = item.intStateID,
                                                                 province = item.vchStateName,
                                                                 city_id = item.intCityID,
                                                                 city = item.vchCityName,
                                                                 county_id = item.intCountyID,
                                                                 county = item.vchCountyName,
                                                                 addr = item.vchDetailAddr,
                                                                 zip = item.vchPostCode,
                                                                 get_def = item.intIsDefaultAddr == 1 ? true : false,
                                                                 created = item.dtAddTime,
                                                                 modify_date = item.dtLastModTime,
                                                                 deliver_id = item.intDeliverID,
                                                                 pay_id = item.intPayID
                                                             });
                                     }
                                     catch
                                     {
                                     }
                                 });
                result.status = MResultStatus.Success;
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "获取用户收货地址里列表 出错！";
            }
            return result;
        }

        /// <summary>
        /// 获取用户默认收货地址
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static MResult<AddressEntity> GetDefaultAddress(int user_id)
        {
            var result = new MResult<AddressEntity>(true);
            try
            {
                var member = DALFactory.Member();
                var item = member.GetMemberDefaultAddressInfo(user_id);
                if (item == null || item.intAddressID <= 0)
                {
                    result.status = MResultStatus.Undefined;
                    result.msg = "没有数据！";
                }
                result.info = new AddressEntity
                {
                    id = item.intAddressID,
                    contact_id = item.intUserID,
                    contact_name = item.vchConsignee,
                    name = item.vchCAName,
                    phone = item.vchPhone,
                    mobile = item.vchMobile,
                    type = item.intAddrType,
                    province_id = item.intStateID,
                    province = item.vchStateName,
                    city_id = item.intCityID,
                    city = item.vchCityName,
                    county_id = item.intCountyID,
                    county = item.vchCountyName,
                    addr = item.vchDetailAddr,
                    zip = item.vchPostCode,
                    get_def = item.intIsDefaultAddr == 1 ? true : false,
                    created = item.dtAddTime,
                    modify_date = item.dtLastModTime,
                    deliver_id = item.intDeliverID,
                    pay_id = item.intPayID
                };
                result.status = MResultStatus.Success;
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "获取用户默认收货地址 出错！";
            }
            return result;
        }

        /// <summary>
        /// 设置用户收货地址信息
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <param name="uid"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static MResult<int> SetAddress(string sid, string token, int userId, string uid, AddressEntity address)
        {
            var result = new MResult<int>();
            try
            {
                if (address != null && userId > 0)
                {
                    var member = DALFactory.Member();

                    #region 地址实体类转换
                    var userAddress = new User_Consignee_Address
                                          {
                                              intAddressID = address.id,
                                              intUserID = userId,
                                              vchConsignee = address.contact_name,
                                              vchCAName = address.name,
                                              vchPhone = address.phone,
                                              vchMobile = address.mobile,
                                              intAddrType = address.type,
                                              intStateID = address.province_id,
                                              vchStateName = address.province,
                                              intCityID = address.city_id,
                                              vchCityName = address.city,
                                              intCountyID = address.county_id,
                                              vchCountyName = address.county,
                                              vchDetailAddr = address.addr,
                                              vchPostCode = address.zip,
                                              intIsDefaultAddr = (byte)(address.get_def ? 1 : 0),
                                              dtLastModTime = address.modify_date,
                                              intDeliverID = address.deliver_id,
                                              intPayID = address.pay_id,
                                          };
                    #endregion

                    //查询用户地址库数量
                    var addressCount = member.GetMemberAddressCount(userId);

                    #region 如果用户第一次添加地址，则设为默认地址
                    if (addressCount == 0)
                    {
                        userAddress.intIsDefaultAddr = 1;
                    }
                    #endregion

                    #region 如果设置了 默认收货地址，则需要清空之前所有地址的默认状态
                    if (userAddress.intIsDefaultAddr == 1)
                    { member.ReSetAddressDefaultStatus(userId); }
                    #endregion

                    #region 如果 地址id > 0 则需要更新
                    if (address.id > 0)
                    {
                        userAddress.dtLastModTime = DateTime.Now;
                        if (member.UpdateMemberAddress(address.id, userAddress))
                            result.status = MResultStatus.Success;
                        else
                        {
                            result.status = MResultStatus.ExecutionError;
                            result.msg = "更新收货地址失败！";
                        }
                    }
                    #endregion

                    #region 否则 添加操作
                    else
                    {
                        #region 判断用户地址是否已存在
                        var addresExists = member.CheckAddresExists(userAddress);
                        if (addresExists)
                        {
                            result.status = MResultStatus.ExecutionError;
                            result.msg = "该收货地址已存在!";
                            return result;
                        }
                        #endregion

                        #region 判断用户地址数量
                        if (addressCount >= 10)
                        {
                            result.status = MResultStatus.ExecutionError;
                            result.msg = "您最多只能保存10个收货地址!";
                            return result;
                        }
                        #endregion

                        userAddress.dtAddTime = DateTime.Now;
                        userAddress.dtLastModTime = DateTime.Now;
                        result.info = member.AddMemberAddress(userAddress);
                        if (result.info > 0)
                            result.status = MResultStatus.Success;
                        else
                        {
                            result.status = MResultStatus.ExecutionError;
                            result.msg = "添加收货地址失败！";
                        }
                    }
                    #endregion

                }
                else
                {
                    result.status = MResultStatus.ParamsError;
                    result.msg = "参数错误！";
                }
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "设置用户收货地址信息 异常！";
            }
            return result;
        }

        /// <summary>
        /// 获取用户地址信息
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public static MResult<AddressEntity> GetAddressInfo(int addressId)
        {
            var result = new MResult<AddressEntity>(true);
            var member = DALFactory.Member();
            var item = member.GetMemberAddressInfo(addressId);

            try
            {
                result.info = new AddressEntity
                {
                    id = item.intAddressID,
                    contact_id = item.intUserID,
                    contact_name = item.vchConsignee,
                    name = item.vchCAName,
                    phone = item.vchPhone,
                    mobile = item.vchMobile,
                    type = item.intAddrType,
                    province_id = item.intStateID,
                    province = item.vchStateName,
                    city_id = item.intCityID,
                    city = item.vchCityName,
                    county_id = item.intCountyID,
                    county = item.vchCountyName,
                    addr = item.vchDetailAddr,
                    zip = item.vchPostCode,
                    get_def = item.intIsDefaultAddr == 1 ? true : false,
                    created = item.dtAddTime,
                    modify_date = item.dtLastModTime,
                    deliver_id = item.intDeliverID,
                    pay_id = item.intPayID
                };
                result.status = MResultStatus.Success;
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "获取用户收货地址里列表 出错！";
            }

            return result;
        }

        /// <summary>
        /// 设置用户默认收货地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public static MResult SetDefaultAddress(int userId, int addressId)
        {
            var result = new MResult();
            try
            {
                var member = DALFactory.Member();
                var success = member.SetDefaultAddress(userId, addressId);
                if (success)
                {
                    result.status = MResultStatus.Success;
                }
                else
                {
                    result.status = MResultStatus.ExecutionError;
                    result.msg = "设置默认收货地址失败！";
                }
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "设置用户默认收货地址 出错！";
            }
            return result;
        }
    }
}
