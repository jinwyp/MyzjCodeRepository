using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using Wcf.BLL.Member;
using Wcf.Entity.Goods;
using Wcf.Entity.Member;
using System;
using Core.DataType;
using System.Web;
using Core.Enums;
using Core.DataTypeUtility;

namespace Wcf.ServiceLibrary.Member
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = CommonUri.JAVASCRIPT_CALLBACKNAME)]
    public class MemberService : BaseWcfService, IMemberService
    {
        public MResult<int> RegisterMember(string sid, string token, string guid, string user_id, string uid, UserRegister user)
        {
            var result = new MResult<int>();

            try
            {
                result = MemberBLL.RegisterMember(guid,(int)SystemType, user);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult<int> LoginMember(string sid, string token, string guid, string user_id, string uid, LoginEntity loginEntity)
        {
            var result = new MResult<int>();

            try
            {
                result = MemberBLL.LoginMember(guid,(int)SystemType, loginEntity.uid, loginEntity.pwd);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult ReLoginMember(string sid, string token, string guid, string user_id, string uid)
        {
            var result = new MResult();

            try
            {
                result = MemberBLL.ReLoginMember(sid, uid, token);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult LogOut(string sid, string token, string guid, string user_id, string uid)
        {
            var result = new MResult();

            try
            {
                result = MemberBLL.LogOut(sid, uid, token);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult ResetLoginPassword(string sid, string token, string guid, string user_id, string uid)
        {
            var result = new MResult();

            try
            {
                result = MemberBLL.ResetLoginPassword(sid, uid);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult<UserEntity> GetMemberInfo(string sid, string token, string guid, string user_id, string uid)
        {
            var result = new MResult<UserEntity>();

            try
            {
                result = MemberBLL.GetMemberInfo(uid);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult<AddressEntity> GetDefaultAddress(string sid, string token, string guid, string user_id, string uid)
        {
            var result = new MResult<AddressEntity>();

            try
            {
                result = MemberBLL.GetDefaultAddress(UserId);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

        public MResult<AddressEntity> GetAddressInfo(string sid, string token, string guid, string user_id, string uid, string address_id)
        {
            var result = new MResult<AddressEntity>();

            try
            {
                var addressId = MCvHelper.To<int>(address_id);
                result = MemberBLL.GetAddressInfo(addressId);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

        public MResultList<AddressEntity> GetAddressList(string sid, string token, string guid, string user_id, string uid)
        {
            var result = new MResultList<AddressEntity>();

            try
            {
                result = MemberBLL.GetMemberAddressList(UserId);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

        public MResult<int> SetAddress(string sid, string token, string guid, string user_id, string uid, AddressEntity address)
        {
            var result = new MResult<int>();
            
            try
            {
                result = MemberBLL.SetAddress(sid, token, UserId, uid, address);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

        public MResult SetDefaultAddress(string sid, string token, string guid, string user_id, string uid, string address_id)
        {
            var result = new MResult();

            try
            {
                var addressId = MCvHelper.To<int>(address_id);
                result = MemberBLL.SetDefaultAddress(UserId, addressId);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

    }
}
