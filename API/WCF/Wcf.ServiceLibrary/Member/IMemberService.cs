using System.ServiceModel;
using System.ServiceModel.Web;
using Wcf.Entity.Member;
using Core.DataType;

namespace Wcf.ServiceLibrary.Member
{
    [ServiceContract]
    public interface IMemberService
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="sid"> </param>
        /// <param name="uid"> </param>
        /// <param name="user"></param>
        /// <param name="token"> </param>
        /// <param name="guid"> </param>
        /// <param name="user_id"> </param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = MemberUri.REGISTER)]
        MResult<int> RegisterMember(string sid, string token, string guid, string user_id, string uid, UserRegister user);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="sid"> </param>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="token"> </param>
        /// <param name="guid"> </param>
        /// <param name="loginEntity"> </param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = MemberUri.LOGIN)]
        MResult<int> LoginMember(string sid, string token, string guid, string user_id, string uid, LoginEntity loginEntity);

        /// <summary>
        /// 重新登录
        /// </summary>
        /// <param name="sid"> </param>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = MemberUri.RELOGIN)]
        MResult ReLoginMember(string sid, string token, string guid, string user_id, string uid);

        /// <summary>
        /// 注销 登出
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate=MemberUri.LOGOUT)]
        MResult LogOut(string sid, string token, string guid, string user_id, string uid);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"> </param>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = MemberUri.RESETLOGINPASSWORD)]
        MResult ResetLoginPassword(string sid, string token, string guid, string user_id, string uid);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="sid"> </param>
        /// <param name="token"> </param>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = MemberUri.GETMEMBERINFO)]
        MResult<UserEntity> GetMemberInfo(string sid, string token, string guid, string user_id, string uid);

        /// <summary>
        /// 获取默认收货地址
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = MemberUri.GETDEFAULTADDRESS)]
        MResult<AddressEntity> GetDefaultAddress(string sid, string token, string guid, string user_id, string uid);

        /// <summary>
        /// 获取用户地址信息
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="guid"> </param>
        /// <param name="address_id"> </param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = MemberUri.GETADDRESSINFO)]
        MResult<AddressEntity> GetAddressInfo(string sid, string token, string guid, string user_id, string uid, string address_id);

        /// <summary>
        /// 获取用户收货地址列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = MemberUri.GETADDRESSLIST)]
        MResultList<AddressEntity> GetAddressList(string sid, string token, string guid, string user_id, string uid);

        /// <summary>
        /// 设置用户收货地址
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="address"></param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method="POST", UriTemplate=MemberUri.SETADDRESS)]
        MResult<int> SetAddress(string sid, string token, string guid, string user_id, string uid, AddressEntity address);

        /// <summary>
        /// 设置用户默认收货地址
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="address_id"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = MemberUri.SETDEFAULTADDRESS)]
        MResult SetDefaultAddress(string sid, string token, string guid, string user_id, string uid, string address_id);

    }

}
