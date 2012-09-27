using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EF.Model.DataContext;

namespace EF.DAL
{
    public partial class Member
    {
        /// <summary>
        /// 获取会员收货地址列表
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<User_Consignee_Address> GetMemberAddressList(int user_id)
        {
            if (user_id > 0)
            {
                using (var holycaDb = new HolycaEntities())
                {
                    var queryTxt = from a in holycaDb.User_Consignee_Address
                                   where a.intUserID == user_id
                                   && a.intStateID > 0
                                   && a.intCityID > 0
                                   && a.intCountyID > 0
                                   && !string.IsNullOrEmpty(a.vchConsignee)
                                   && !string.IsNullOrEmpty(a.vchDetailAddr)
                                   orderby a.intIsDefaultAddr descending, a.dtLastModTime descending
                                   select a;
                    return queryTxt.Take(10).ToList();
                }
            } return new List<User_Consignee_Address>();
        }

        /// <summary>
        /// 添加会员收货地址
        /// </summary>
        /// <param name="addressEntity"></param>
        /// <returns></returns>
        public int AddMemberAddress(User_Consignee_Address addressEntity)
        {
            using (var holycaDb = new HolycaEntities())
            {
                holycaDb.AddToUser_Consignee_Address(addressEntity);
                holycaDb.SaveChanges();
                return addressEntity.intAddressID;
            }
        }

        /// <summary>
        /// 更新会员收货地址
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressEntity"></param>
        /// <returns></returns>
        public bool UpdateMemberAddress(int id, User_Consignee_Address addressEntity)
        {
            var result = false;
            using (var holycaDb = new HolycaEntities())
            {
                try
                {
                    var addressInfo = holycaDb.User_Consignee_Address.FirstOrDefault(c => c.intAddressID == id);
                    if (addressInfo != null && addressInfo.intAddressID > 0)
                    {
                        addressInfo.intAddrType = addressEntity.intAddrType;
                        addressInfo.intCityID = addressEntity.intCityID;
                        addressInfo.intCountyID = addressEntity.intCountyID;
                        addressInfo.intStateID = addressEntity.intStateID;
                        addressInfo.vchCAName = addressEntity.vchCAName;
                        addressInfo.vchCityName = addressEntity.vchCityName;
                        addressInfo.vchConsignee = addressEntity.vchConsignee;
                        addressInfo.vchCountyName = addressEntity.vchCountyName;
                        addressInfo.vchDetailAddr = addressEntity.vchDetailAddr;
                        addressInfo.vchMobile = addressEntity.vchMobile;
                        addressInfo.vchPhone = addressEntity.vchPhone;
                        addressInfo.vchPostCode = addressEntity.vchPostCode;
                        addressInfo.vchStateName = addressEntity.vchStateName;
                        addressInfo.intIsDefaultAddr = addressInfo.intIsDefaultAddr;
                        addressInfo.dtLastModTime = addressEntity.dtLastModTime;

                        //holycaDb.ApplyCurrentValues("User_Consignee_Address", addressInfo);
                        holycaDb.SaveChanges();
                        result = true;
                    }
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取用户收货地址信息
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public User_Consignee_Address GetMemberAddressInfo(int addressId)
        {
            using (var holycaDb = new HolycaEntities())
            {
                var queryTxt = from a in holycaDb.User_Consignee_Address
                               where a.intAddressID == addressId
                               select a;
                return queryTxt.FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取用户默认收货地址信息
        /// </summary>
        /// <returns></returns>
        public User_Consignee_Address GetMemberDefaultAddressInfo(int userId)
        {
            using (var holycaDb = new HolycaEntities())
            {
                var queryTxt = from a in holycaDb.User_Consignee_Address
                               where a.intUserID == userId
                               orderby a.intIsDefaultAddr descending
                               select a;
                return queryTxt.FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取用户收货地址总数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetMemberAddressCount(int userId)
        {
            using (var holycaDb = new HolycaEntities())
            {
                var queryTxt = from a in holycaDb.User_Consignee_Address
                               where a.intUserID == userId
                               select a;
                return queryTxt.Count();
            }
        }

        /// <summary>
        /// 检查收货地址信息是否存在
        /// </summary>
        /// <param name="addressEntity"></param>
        /// <returns></returns>
        public bool CheckAddresExists(User_Consignee_Address addressEntity)
        {
            using (var holycaDb = new HolycaEntities())
            {
                var queryTxt = from a in holycaDb.User_Consignee_Address
                               where a.intUserID == addressEntity.intUserID
                                     && a.intStateID == addressEntity.intStateID
                                     && a.intCityID == addressEntity.intCityID
                                     && a.intCountyID == addressEntity.intCountyID
                                     && a.vchDetailAddr == addressEntity.vchDetailAddr
                                     && a.vchPostCode == addressEntity.vchPostCode
                                     && a.vchConsignee == addressEntity.vchConsignee
                               select a;
                return queryTxt.Count() > 0;
            }
        }

        /// <summary>
        /// 设置用户默认收货地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public bool SetDefaultAddress(int userId, int addressId)
        {
            var result = false;
            using (var holycaDb = new HolycaEntities())
            {
                try
                {
                    ReSetAddressDefaultStatus(userId);
                    var address = holycaDb.User_Consignee_Address.FirstOrDefault(c => c.intAddressID == addressId);
                    if (address != null && address.intAddressID > 0)
                    {
                        address.intIsDefaultAddr = 1;
                        holycaDb.SaveChanges();
                        result = true;
                    }
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 重置用户全部地址默认状态为 0 （非默认地址）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ReSetAddressDefaultStatus(int userId)
        {
            var result = false;
            using (var holycaDb = new HolycaEntities())
            {
                try
                {
                    var defAddressList = holycaDb.User_Consignee_Address.Where(c => c.intUserID == userId && c.intIsDefaultAddr == 1).ToList();
                    if (defAddressList.Any())
                    {
                        defAddressList.ForEach(item => item.intIsDefaultAddr = 0);
                        holycaDb.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 检查用户是否存在黑名单里
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckUserIdInBackList(int userId)
        {
            var result = false;
            using (var db = new bbHomeEntities())
            {
                var queryTxt = from a in db.tb_Blacklist
                               where a.intUserID == userId
                               select a;
                result = queryTxt.Any();
            }
            return result;
        }

    }
}
