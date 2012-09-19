using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wcf.ServiceLibrary
{
    //采用 add set get del 前缀

    /// <summary>
    /// 公用 Uri
    /// </summary>
    internal static class CommonUri
    {
        /// <summary>
        /// 传递回调函数的名称
        /// </summary>
        public const string JAVASCRIPT_CALLBACKNAME = "callback";

    }

    /*
    /// sid：系统ID 
    /// token：用户令牌
    /// uid:用户账号（一般为邮箱）
    */

    internal static class ManageUri
    {
        /// <summary>
        /// 刷新授权数据
        /// </summary>
        public const string REFRESHAUTHDATA = "/RefreshAuthData/{sid}/{token}/{guid}/{user_id}/{uid}/{privatekey}";
    }

    /// <summary>
    /// 基础Api Uri
    /// </summary>
    internal static class BaseDataUri
    {
        /// <summary>
        /// 获取地区列表
        /// sid：系统ID 
        /// token：用户令牌
        /// parentid：上级id
        /// </summary>
        public const string GETREGIONLIST = "/get_region_list/{sid}/{token}/{guid}/{user_id}/{uid}/{parentid}";
        /// <summary>
        /// 获取支付列表
        /// </summary>
        public const string GETPAYLIST = "/get_pay_list/{sid}/{token}/{guid}/{user_id}/{uid}/{paygroupid}";
        /// <summary>
        /// 获取全部地区列表
        /// sid：系统ID 
        /// token：用户令牌
        /// parentid：上级id
        /// </summary>
        public const string GETALLREGIONLIST = "/get_allregion_list/{sid}/{token}/{guid}/{user_id}/{uid}";
        /// <summary>
        /// 获取支付方式列表
        /// </summary>
        public const string GETPAYMENTLIST = "/get_payment_list/{sid}/{token}/{guid}/{user_id}/{uid}/{regionid}";
        /// <summary>
        /// 获取配送列表
        /// </summary>
        public const string GETLOGISTICSLIST = "/get_logistics_list/{sid}/{token}/{guid}/{user_id}/{uid}/{regionid}/{paygroupid}";
    }

    /// <summary>
    /// 会员Api Uri
    /// </summary>
    internal static class MemberUri
    {
        /// <summary>
        /// 会员注册
        /// </summary>
        public const string REGISTER = "/register/{sid}/{token}/{guid}/{user_id}/{uid}";
        /// <summary>
        /// 登录
        /// </summary>
        public const string LOGIN = "/login/{sid}/{token}/{guid}/{user_id}/{uid}";
        /// <summary>
        /// 注销
        /// </summary>
        public const string LOGOUT = "/logout/{sid}/{token}/{guid}/{user_id}/{uid}";
        /// <summary>
        /// 重新登录
        /// </summary>
        public const string RELOGIN = "/relogin/{sid}/{token}/{guid}/{user_id}/{uid}";
        /// <summary>
        /// 重置密码
        /// </summary>
        public const string RESETLOGINPASSWORD = "/reset_member_loginpassword/{sid}/{token}/{guid}/{user_id}/{uid}/{email}";
        /// <summary>
        /// 修改密码
        /// </summary>
        public const string CHANGELOGINPASSWORD = "/change_loginpassword/{sid}/{token}/{guid}/{user_id}/{uid}";
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public const string GETMEMBERINFO = "/get_member_info/{sid}/{token}/{guid}/{user_id}/{uid}";
        /// <summary>
        /// 设置用户收货地址
        /// </summary>
        public const string SETADDRESS = "/set_address_info/{sid}/{token}/{guid}/{user_id}/{uid}";
        /// <summary>
        /// 获取用户地址信息
        /// </summary>
        public const string GETADDRESSINFO = "/get_address_info/{sid}/{token}/{guid}/{user_id}/{uid}/{address_id}";
        /// <summary>
        /// 设置默认收货地址
        /// </summary>
        public const string SETDEFAULTADDRESS = "/set_defaultaddress/{sid}/{token}/{guid}/{user_id}/{uid}/{address_id}";
        /// <summary>
        /// 获取用户默认收货地址
        /// </summary>
        public const string GETDEFAULTADDRESS = "/get_defaultaddress_info/{sid}/{token}/{guid}/{user_id}/{uid}";
        /// <summary>
        /// 获取用户收货地址
        /// </summary>
        public const string GETADDRESSLIST = "/get_address_list/{sid}/{token}/{guid}/{user_id}/{uid}";
    }

    /// <summary>
    /// 订单Api Uri
    /// </summary>
    internal static class OrderUri
    {
        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        public const string ADDGOODSTOSHOPPINGCAR = "/add_goodstoshoppingcar/{sid}/{token}/{guid}/{user_id}/{uid}/{area_id}/{gid}/{num}";

        /// <summary>
        /// 删除购物车数据
        /// </summary>
        public const string DELSHOPPINGCARTBYSCID = "/del_shoppingcart/{sid}/{token}/{guid}/{user_id}/{uid}/{shoppingcarid}";

        /// <summary>
        /// 设置购物车商品数量
        /// </summary>
        public const string SETSHOPPINGCARTGOODSNUM = "/set_shoppingcartgoodsnum/{sid}/{token}/{guid}/{user_id}/{uid}/{shoppingcarid}/{gid}/{num}";

        /// <summary>
        /// 获取购物车商品列表
        /// </summary>
        public const string GETSHOPPINGCARTGOODSLIST = "/get_shoppingcartgoods_list/{sid}/{token}/{guid}/{user_id}/{uid}";

        /// <summary>
        /// 获取购物车商品数量
        /// </summary>
        public const string GETSHOPPINGCARTGOODSNUM = "/get_shoppingcartgoodsnum/{sid}/{token}/{guid}/{user_id}/{uid}";

        /// <summary>
        /// 创建订单
        /// </summary>
        public const string CREATEORDER = "/add_order_info/{sid}/{token}/{guid}/{user_id}/{uid}";

        /// <summary>
        /// 获取临时订单信息
        /// </summary>
        public const string GETTEMPORDERINFO = "/get_temporder_info/{sid}/{token}/{guid}/{user_id}/{uid}";

        /// <summary>
        /// 获取我的订单列表
        /// </summary>
        public const string GETORDERSLIST = "/get_order_list/{sid}/{token}/{guid}/{user_id}/{uid}/{begintime}/{endtime}";
        /// <summary>
        /// 获取订单信息
        /// </summary>
        public const string GETORDERSINFO = "/get_order_info/{sid}/{token}/{guid}/{user_id}/{uid}/{ordercode}";
        /// <summary>
        /// 获取订单详细
        /// </summary>
        public const string GETORDERGOODSLIST = "/get_ordergoods_list/{sid}/{token}/{guid}/{user_id}/{uid}/{ordercode}";
        /// <summary>
        /// 订单支付成功
        /// </summary>
        public const string ORDERPAYMENTSUCCESS = "/orderpayment_success/{sid}/{token}/{guid}/{user_id}/{uid}/{getdata}/{postdata}";
    }

    /// <summary>
    /// 商品Api Uri
    /// </summary>
    internal static class GoodsUri
    {
        /// <summary>
        /// 获取商品列表
        /// </summary>
        public const string GETGOODSLIST = "/get_goods_list/{sid}/{token}/{guid}/{user_id}/{uid}/{bid}/{cid}/{age}/{price}/{sort}/{page}/{size}";
        /// <summary>
        /// 获取商品详细
        /// </summary>
        public const string GETGOODSINFO = "/get_goods_info/{sid}/{token}/{guid}/{user_id}/{uid}/{gid}";
        /// <summary>
        /// 获取商品图片列表
        /// </summary>
        public const string GETGOODSPICLIST = "/get_goodspic_list/{sid}/{token}/{guid}/{user_id}/{uid}/{gid}";
        /// <summary>
        /// 获取商品评论列表
        /// </summary>
        public const string GETGOODSCOMMENT = "/get_goodscomment_list/{sid}/{token}/{guid}/{user_id}/{uid}/{gid}";
        /// <summary>
        /// 获取商品分类列表
        /// </summary>
        public const string GETGOODSCATEGORYLIST = "/get_goodscategory_list/{sid}/{token}/{guid}/{user_id}/{uid}";
    }

    /// <summary>
    /// 支付Api Uri
    /// </summary>
    internal static class PaymentUri
    {
        /// <summary>
        /// 订单支付
        /// </summary>
        public const string ORDERPAYMENT = "/order_payment/{sid}/{token}/{guid}/{user_id}/{uid}/{ocode}/{payid}";
    }

    /// <summary>
    /// 内容管理
    /// </summary>
    internal static class CmsUri
    {
        /// <summary>
        /// 获取栏位数据列表
        /// </summary>
        public const string GETCOLUMNDATALIST = "/get_columndata_list/{sid}/{token}/{guid}/{user_id}/{uid}/{columncode}/{page}/{size}";
        /// <summary>
        /// 获取公告列表
        /// </summary>
        public const string GETNOTICELIST = "/get_notice_list/{sid}/{token}/{guid}/{user_id}/{uid}/{page}/{size}";
        /// <summary>
        /// 获取公告详细
        /// </summary>
        public const string GETNOTICEINFO = "/get_notice_info/{sid}/{token}/{guid}/{user_id}/{uid}/{noticeid}";
    }

}
