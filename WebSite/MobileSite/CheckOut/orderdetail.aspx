<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="orderdetail.aspx.cs" Inherits="MobileSite.CheckOut.orderdetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.myaccount() %>" data-theme="c" data-icon="star" >个人中心</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.myaccount() %>" data-theme="c" data-icon="arrow-l"  data-rel="back">返回</a> </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div class="content-primary">
            <ul id="orderdetail_template_container" data-role="listview" data-inset="true" data-divider-theme="d">
                
            </ul>
            <script id="orderdetail_template" type="text/template">
                <li data-role="list-divider">订单{$T.ocode}信息</li>
                <li>
                    <p>
                        收货人：{$T.contact_name}</p>
                    <p>
                        收货地址：{$T.addr}</p>
                    <p>
                        邮编：{$T.zip}</p>
                    <p>
                        电话：{$T.mobile}</p>
                </li>
                <li>支付方式：{$T.paytype}</li>
                <li>送货方式：<strong>{$T.deliverytype}</strong></li>
                <li>
                    {#if $T.titletype==1}
                    <p>
                        发票类型：{#if $T.invoicecategory===1}用品发票{#elseif $T.invoicecategory===2}食品发票{#/if}</p>
                    {#elseif $T.titletype==2}
                    <p>
                        发票类型：{#if $T.invoicecategory===1}用品发票{#elseif $T.invoicecategory===2}食品发票{#/if}</p>
                    <p>
                        发票抬头：{$T.invoicetitle}</p>
                    {#/if}
                </li>
            </script>
            <div data-role="">
                <a href="<%= MobileSite.BaseLib.WebUrls.onlinepayment() %>" data-role="button" data-inline="true" data-theme="e" class="zaixian">在线支付</a><a
                    href="#" data-role="button" data-inline="true" data-mini="true" class="cancels_btn">取消订单</a></div>
            <ul id="ordergoods_list_template_container" data-role="listview" data-inset="true" data-divider-theme="d">
                
            </ul>
            <script id="ordergoods_list_template" type="text/template">
            <li data-role="list-divider">订单商品列表</li>
                {#foreach $T as dr}
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productdetailinfo("{$T.dr.gid}") %>">
                    <img src="{$T.dr.pic_url}" />
                    <p>
                        商品编号：<strong>{$T.dr.gid}</strong></p>
                    <p>
                        名称：<strong>{$T.dr.title}</strong></p>
                    <p>
                        单价：<strong>￥{$T.dr.price}</strong></p>
                    <p>
                        数量：<strong>{$T.dr.num}</strong></p>
                    <p>
                        小计：<strong class="ui-font-red">￥{$T.dr.total}</strong></p>
                </a></li>
                {#/for}
                <li data-icon="arrow-l" data-iconpos="left"><a href="<%= MobileSite.BaseLib.WebUrls.orderlist() %>">返回</a> </li>
            </script>
            <div data-role="">
                <a href="<%= MobileSite.BaseLib.WebUrls.onlinepayment() %>" data-role="button" data-inline="true" data-theme="e" class="zaixian">在线支付</a><a
                    href="#" data-role="button" data-inline="true" data-mini="true" class="cancels_btn">取消订单</a></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
<script src="<%= MobileSite.BaseLib.WebUrls.JsRoot() %>mobile.muzj.js" type="text/javascript"></script>
</asp:Content>
