<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="orderconfirm.aspx.cs" Inherits="MobileSite.orderconfirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.index() %>" data-theme="c" data-icon="home" rel="external">首页</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" data-theme="c" data-icon="check" rel="external">购物车</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.orderconfirm() %>" data-theme="e" data-icon="arrow-r" rel="external">提交订单</a> </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <ul id="address_message" data-role="listview" data-divider-theme="d" data-inset="true">
            <li data-role="list-divider" role="heading">收货人信息</li>
            <li><a id="address_default" class="ui-link-inherit" href="<%= MobileSite.BaseLib.WebUrls.addresslist() %>" rel="external"><p>正在加载中...</p></a></li>
                
                <script id="address_default_template" type="text/template">
                <p>
                    收货人：{$T.contact_name}<input type="hidden" id="text_user_id" value="{$T.id}" /></p>
                <p>
                    收货地址：{$T.addr}</p>
                <p>
                    邮编：{$T.zip}</p>
                <p>
                    电话：{$T.mobile}</p>
                    </script>
            
            <li data-role="list-divider" role="heading">支付方式</li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.paymentlist() %>" rel="external">
                <p id="paymentlist_p">正在加载中...
                    </p>
            </a></li>
            <li data-role="list-divider" role="heading">配送方式</li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.deliverylist() %>" rel="external">
                <p id="delivery_text_name_p">
                    正在加载中...</p>
                <p id="delivery_sh_time_text_p">
                    </p>
                <p id="delivery_fk_time_text_p">
                    </p>
            </a></li>
            <li data-role="list-divider" role="heading">发票信息</li>
            <li data-theme="c"><a href="<%= MobileSite.BaseLib.WebUrls.invoice() %>" rel="external">
                <p id="invoice_Type_Text">
                    正在加载中...</p>
                <p id="invoice_Theader_Text">
                    </p>
            </a></li>
            <li data-role="list-divider" role="heading">订单备注</li>
            <li>
                <textarea cols="40" rows="8" name="textarea" id="textarea"></textarea>
            </li>
        </ul>
        <div class="">
            <div data-role="fieldcontain">
                <label>
                    商品总金额：</label><span id="Total_Price">256</span>元
            </div>
            <div data-role="fieldcontain">
                <label>
                    +运费：</label><span id="Y_Price">5</span>元
            </div>
            <%--<div data-role="fieldcontain">
                <label>
                    -优惠金额：</label>0元
            </div>--%>
            <div data-role="fieldcontain" class="ui-font-bold">
                <label>
                    您需支付总金额：</label>
                <strong class="ui-font-red" id="Final_Price">0.00元</strong>
            </div>
        </div>
        <a href="<%= MobileSite.BaseLib.WebUrls.makeorder() %>" data-role="button" data-theme="a">提交订单</a>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
<script src="<%= MobileSite.BaseLib.WebUrls.JsRoot() %>mobile.checkout.js" type="text/javascript"></script>
</asp:Content>
