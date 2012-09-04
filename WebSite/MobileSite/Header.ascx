<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="MobileSite.Header" %>
<div class="header">
    <div class="logo">
        <a href="<%= MobileSite.BaseLib.WebUrls.index() %>">
            <img src="/images/logo.png" />
        </a>
    </div>
    <div class="nav">
        <a href="<%= MobileSite.BaseLib.WebUrls.category() %>" class="menu"></a><a href="<%= MobileSite.BaseLib.WebUrls.myaccount() %>"
            class="user"></a><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" class="cart">
            </a>
        <input type="hidden" class="Hid_Good_Total_price" /><span class="Good_Total_Count cart-count ui-btn-up-c ui-btn-corner-all"
            style="display: none"></span>
    </div>
</div>
