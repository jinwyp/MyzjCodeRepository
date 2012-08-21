<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="shoppingcart.aspx.cs" Inherits="MobileSite.shoppingcart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" data-theme="c" data-icon="star" rel="external">购物车</a></li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" data-theme="c" data-icon="arrow-l"  data-rel="back">返回</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div class="content-primary">
            <div>
                您共有<strong id="good_totals">0</strong>件商品</div>
            <ul data-role="listview" data-inset="true" id="shoppingCart_list">
                
            </ul>
            <div class="ui-font-bold">
                商品总金额：<strong class="ui-font-red" id="goodsTotal">25145元</strong></div>
            <a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" data-role="button" data-icon="arrow-l" data-iconpos="left"
                data-theme="c" data-inline="true" data-mini="true" rel="external">继续购物</a> <a id="ShoppingCart_btn" href="javascript:void(0)"
                    data-role="button" data-icon="arrow-r" data-iconpos="right" data-theme="a" data-inline="true"
                    rel="external" id="balance">去结算</a><%--<a href="#"  data-role="button" data-icon="arrow-r"
                       data-iconpos="right" data-theme="a" data-inline="true" id="balance">去结算</a>--%>
            <ul id="tis_Tip" class="error">
            </ul>
        </div>
       
    </div>
    <script id="shoppingCart_list_template" type="text/template">
        {#foreach $T as Puct}
                <li>
                    <img src="{$T.Puct.vchPicURL}" alt="{$T.Puct.nchProductName}" />
                    <h3>
                        <a href="<%= MobileSite.BaseLib.WebUrls.productdetailinfo("{$T.Puct.intProductID}") %>" rel="external">{$T.Puct.nchProductName}</a></h3>
                    <div data-role="fieldcontain" data-theme="c">
                        <label>
                            单价:</label>
                        <strong class="ui-font-red">￥{$T.Puct.numSalePrice}</strong></div>
                    <div data-role="fieldcontain" data-theme="c">
                        <label>
                            数量：
                        </label>
                        <input type="range" name="slider" id="slider" value="{$T.Puct.intBuyCount}" onum="{$T.Puct.intBuyCount}" min="1" max="20" product_id="{$T.Puct.intProductID}" ShopCartID="{$T.Puct.intShopCartID}"/>
                    </div>
                    <div data-role="controlgroup" data-type="horizontal">
                        <a class="Del_id" href="#" data-role="button" data-icon="delete" data-iconpos="left" product_id="{$T.Puct.intProductID}" delete_id="{$T.Puct.intShopCartID}">删除</a></div>
                    <p class="error-text" id="error_{$T.Puct.intProductID}">
                        </p>
                </li>
                {#/for}
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
    <script src="<%= MobileSite.BaseLib.WebUrls.JsRoot() %>mobile.checkout.js" type="text/javascript"></script>
</asp:Content>
