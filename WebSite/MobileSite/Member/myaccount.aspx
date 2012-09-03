<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="myaccount.aspx.cs" Inherits="MobileSite.myaccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" data-theme="c" data-icon="star"
                >个人中心</a></li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" data-theme="c" data-icon="arrow-l"
                data-rel="back">返回</a> </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <ul data-role="listview" id="myAccount_content" data-divider-theme="d" data-inset="true">
        </ul>
        <ul data-role="listview" data-divider-theme="d" data-inset="true">
            <li data-role="list-divider">订单中心</li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.orderlist() %>" >我的订单</a></li>
        </ul>
        <a id="logout" data-role="button" data-inline="true" data-mini="true" data-theme="d">
            退出登录</a>
    </div>
    <script id="myAccount_Template" type="text/template">
    <li data-role="list-divider">个人信息</li>
            <li>用户名：<strong>{$T.email}</strong></li>
            <li>会员等级：<strong>{$T.userlevel}</strong></li>
            <li>已累积幸运星：<strong>{$T.locky}</strong></li>
            <li>累计消费：<strong>¥{$T.orderstotal}</strong></li>
    </script>
</asp:Content>
