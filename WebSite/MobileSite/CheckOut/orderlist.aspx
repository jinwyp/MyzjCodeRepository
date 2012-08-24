<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="orderlist.aspx.cs" Inherits="MobileSite.CheckOut.orderlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.myaccount() %>" data-theme="c" data-icon="star" rel="external">个人中心</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.myaccount() %>" data-theme="c" data-icon="arrow-l"  data-rel="back">返回</a> </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div data-role="controlgroup" data-type="horizontal">
            <a id="oneMonth" data-theme="e" data-role="button" data-icon="arrow-d" data-mini="true">
                近一个月订单</a> <a id="twoMonth" data-theme="e" data-role="button" data-icon="arrow-d" data-mini="true">
                    近两个月订单</a>
        </div>
        <div class="content-primary">
            <ul id="orderlist_template_container" data-role="listview" data-inset="true" data-divider-theme="d">
                
            </ul>
            <script id="orderlist_template" type="text/template">
            {#foreach $T as dr}
            <li><a href="<%= MobileSite.BaseLib.WebUrls.orderdetail("{$T.dr.ocode}") %>">
                    <h3>
                        订单号：{$T.dr.ocode}</h3>
                    <p>
                        订单状态：<strong class="ui-font-red">{$T.dr.status}</strong></p>
                    <p>
                        订单金额：<strong class="ui-font-red">￥{$T.dr.total_order}</strong></p>
                    <p>
                        付款方式：<strong>{$T.dr.paytype}</strong></p>
                    <p>
                        订单日期：<strong>{$T.dr.created}</strong></p>
                </a></li>
                {#/for}
            </script>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
<script src="<%= MobileSite.BaseLib.WebUrls.JsRoot() %>mobile.checkout.js"></script>
</asp:Content>
