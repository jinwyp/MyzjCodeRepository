<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="MobileSite.Header" %>
<div class="header">
    <div class="logo">
        <a href="<%= MobileSite.BaseLib.WebUrls.index() %>" rel="external">
            <img src="/images/logo.png" />
        </a>
    </div>
    <div class="nav">
        <a href="<%= MobileSite.BaseLib.WebUrls.category() %>" class="menu" rel="external"></a><a href="<%= MobileSite.BaseLib.WebUrls.myaccount() %>"
            class="user" rel="external"></a><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" class="cart" rel="external">
            </a>
        <input type="hidden" class="Hid_Good_Total_price" /><span class="Good_Total_Count cart-count ui-btn-up-c ui-btn-corner-all"
            style="display: none"></span>
    </div>
</div>
