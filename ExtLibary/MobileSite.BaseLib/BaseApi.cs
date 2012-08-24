using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using MobileSite.BaseLib.MemberContent;

namespace MobileSite.BaseLib
{
    public class BaseApi : IRequiresSessionState
    {
        protected static Dictionary<string, string> ApiUrlDict = null;
        protected const string SystemId = "WebSite";

        public BaseApi()
        {
            if (ApiUrlDict == null)
            {
                var wcfHost = WebUrls.WcfHost();
                ApiUrlDict = new Dictionary<string, string>();
                if (string.IsNullOrEmpty(wcfHost)) return;

                ApiUrlDict.Add("Member.Get.Info", wcfHost + "Member.svc/get_info");
                ApiUrlDict.Add("Member.Login", wcfHost + "Member.svc/login");
                ApiUrlDict.Add("Member.logout", wcfHost + "Member.svc/logout");
                ApiUrlDict.Add("Member.ReLogin", wcfHost + "Member.svc/relogin");
                ApiUrlDict.Add("Member.Register", wcfHost + "Member.svc/register");
                ApiUrlDict.Add("Goods.goodList", wcfHost + "goods.svc/get_goods_list");
                ApiUrlDict.Add("Member.GetMember.Info", wcfHost + "Member.svc/get_member_info");
                ApiUrlDict.Add("Goods.GetProductDetail.Info", wcfHost + "goods.svc/get_goods_info");
                ApiUrlDict.Add("Goods.goodspic.Info", wcfHost + "goods.svc/get_goodspic_list");
                ApiUrlDict.Add("Goods.shoppingcartgoodsnum", wcfHost + "order.svc/get_shoppingcartgoodsnum");
                ApiUrlDict.Add("Order.get_payment_list", wcfHost + "basedata.svc/get_payment_list");
                ApiUrlDict.Add("Order.get_logistics_list", wcfHost + "basedata.svc/get_logistics_list");
                ApiUrlDict.Add("Member.get_defaultaddress_info", wcfHost + "member.svc/get_defaultaddress_info");
                ApiUrlDict.Add("Member.get_address_list", wcfHost + "member.svc/get_address_list");
                ApiUrlDict.Add("Order.get_allregion_list", wcfHost + "basedata.svc/get_allregion_list");
                ApiUrlDict.Add("Member.set_address_info", wcfHost + "member.svc/set_address_info");
                ApiUrlDict.Add("Member.get_address_info", wcfHost + "member.svc/get_address_info");
                ApiUrlDict.Add("Member.set_defaultaddress", wcfHost + "member.svc/set_defaultaddress");
                ApiUrlDict.Add("Order.get_shoppingcartgoods_list", wcfHost + "order.svc/get_shoppingcartgoods_list");
                ApiUrlDict.Add("Order.set_shoppingcartgoodsnum", wcfHost + "order.svc/set_shoppingcartgoodsnum");
                ApiUrlDict.Add("Order.add_goodstoshoppingcar", wcfHost + "order.svc/add_goodstoshoppingcar");
                ApiUrlDict.Add("Order.del_shoppingcart", wcfHost + "order.svc/del_shoppingcart");
                ApiUrlDict.Add("Order.get_temporder_info", wcfHost + "order.svc/get_temporder_info");
                ApiUrlDict.Add("Order.add_order_info", wcfHost + "order.svc/add_order_info");

            }
        }

    }
}