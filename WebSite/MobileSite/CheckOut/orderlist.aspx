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
            <a href="#onemonth" data-theme="e" data-role="button" data-icon="arrow-d" data-mini="true">
                近一个月订单</a> <a href="#twomonths" data-theme="c" data-role="button" data-icon="arrow-d"
                    data-mini="true">近两个月订单</a>
        </div>
        <div class="content-primary">
            <ul data-role="listview" data-inset="true" data-divider-theme="d">
                <li><a href="<%= MobileSite.BaseLib.WebUrls.orderdetail() %>">
                    <h3>
                        订单号：5373753</h3>
                    <p>
                        订单状态：<strong class="ui-font-red">等待付款</strong></p>
                    <p>
                        订单金额：<strong class="ui-font-red">￥212.00</strong></p>
                    <p>
                        付款方式：<strong>在线支付</strong></p>
                    <p>
                        订单日期：<strong>2012/6/26 13:50:27</strong></p>
                </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.orderdetail() %>">
                    <h3>
                        订单号：5373753</h3>
                    <p>
                        订单状态：<strong class="ui-font-red">等待付款</strong></p>
                    <p>
                        订单金额：<strong class="ui-font-red">￥212.00</strong></p>
                    <p>
                        付款方式：<strong>在线支付</strong></p>
                    <p>
                        订单日期：<strong>2012/6/26 13:50:27</strong></p>
                </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.orderdetail() %>">
                    <h3>
                        订单号：5373753</h3>
                    <p>
                        订单状态：<strong class="ui-font-red">等待付款</strong></p>
                    <p>
                        订单金额：<strong class="ui-font-red">￥212.00</strong></p>
                    <p>
                        付款方式：<strong>在线支付</strong></p>
                    <p>
                        订单日期：<strong>2012/6/26 13:50:27</strong></p>
                </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.orderdetail() %>">
                    <h3>
                        订单号：5373753</h3>
                    <p>
                        订单状态：<strong class="ui-font-red">等待付款</strong></p>
                    <p>
                        订单金额：<strong class="ui-font-red">￥212.00</strong></p>
                    <p>
                        付款方式：<strong>在线支付</strong></p>
                    <p>
                        订单日期：<strong>2012/6/26 13:50:27</strong></p>
                </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.orderdetail() %>">
                    <h3>
                        订单号：5373753</h3>
                    <p>
                        订单状态：<strong class="ui-font-red">等待付款</strong></p>
                    <p>
                        订单金额：<strong class="ui-font-red">￥212.00</strong></p>
                    <p>
                        付款方式：<strong>在线支付</strong></p>
                    <p>
                        订单日期：<strong>2012/6/26 13:50:27</strong></p>
                </a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
</asp:Content>
