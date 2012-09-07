<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="onlinepayment.aspx.cs" Inherits="MobileSite.CheckOut.onlinepayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" data-theme="c" data-icon="star" >购物车</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.orderconfirm() %>" data-theme="c" data-icon="arrow-l" data-rel="back">返回</a>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <ul data-role="listview" id="payList" data-divider-theme="d" data-inset="true">
            
        </ul>
        <script type="text/template" id="payList_template">
            {#foreach $T.list as pay}
                
                <li><a href="">
                    <img src="/images/bank01.jpg" alt="" />
                    <h3>
                        {$T.pay.payname}</h3>
                </a></li>
            {#/for}
        </script>
    </div>
</asp:Content>
