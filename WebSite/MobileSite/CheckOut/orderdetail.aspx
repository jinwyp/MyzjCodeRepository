<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="orderdetail.aspx.cs" Inherits="MobileSite.CheckOut.orderdetail" %>

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
        <div class="content-primary">
            <ul data-role="listview" data-inset="true" data-divider-theme="d">
                <li data-role="list-divider">订单5373753信息</li>
                <li>
                    <p>
                        收货人：王宇鹏</p>
                    <p>
                        收货地址：上海市普陀区， 澳门路519弄华生大厦1号楼7楼</p>
                    <p>
                        邮编：200060</p>
                    <p>
                        电话：13564568304</p>
                </li>
                <li>支付方式：在线支付</li>
                <li>送货方式：<strong>母婴之家快递</strong></li>
                <li>
                    <p>
                        发票类型：食品发票</p>
                    <p>
                        发票抬头：公司 宜家中国</p>
                </li>
            </ul>
            <div data-role="">
                <a href="<%= MobileSite.BaseLib.WebUrls.onlinepayment() %>" data-role="button" data-inline="true" data-theme="e">在线支付</a><a
                    href="#" data-role="button" data-inline="true" data-mini="true">取消订单</a></div>
            <ul data-role="listview" data-inset="true" data-divider-theme="d">
                <li data-role="list-divider">订单商品列表</li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productdetailinfo() %>">
                    <img src="http://img.muyingzhijia.com/product/normal/66158_01_01.jpg" />
                    <p>
                        商品编号：<strong>119902</strong></p>
                    <p>
                        名称：<strong>商品名称商品名称商品名称22</strong></p>
                    <p>
                        单价：<strong>￥212.00</strong></p>
                    <p>
                        数量：<strong>1</strong></p>
                    <p>
                        小计：<strong class="ui-font-red">￥212.00</strong></p>
                </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productdetailinfo() %>">
                    <img src="http://img.muyingzhijia.com/product/normal/66158_01_01.jpg" />
                    <p>
                        商品编号：<strong>119902</strong></p>
                    <p>
                        名称：<strong>商品名称商品名称商品名称22</strong></p>
                    <p>
                        单价：<strong>￥212.00</strong></p>
                    <p>
                        数量：<strong>1</strong></p>
                    <p>
                        小计：<strong class="ui-font-red">￥212.00</strong></p>
                </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productdetailinfo() %>">
                    <img src="http://img.muyingzhijia.com/product/normal/66158_01_01.jpg" />
                    <p>
                        商品编号：<strong>119902</strong></p>
                    <p>
                        名称：<strong>商品名称商品名称商品名称22</strong></p>
                    <p>
                        单价：<strong>￥212.00</strong></p>
                    <p>
                        数量：<strong>1</strong></p>
                    <p>
                        小计：<strong class="ui-font-red">￥212.00</strong></p>
                </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productdetailinfo() %>">
                    <img src="http://img.muyingzhijia.com/product/normal/66158_01_01.jpg" />
                    <p>
                        商品编号：<strong>119902</strong></p>
                    <p>
                        名称：<strong>商品名称商品名称商品名称22</strong></p>
                    <p>
                        单价：<strong>￥212.00</strong></p>
                    <p>
                        数量：<strong>1</strong></p>
                    <p>
                        小计：<strong class="ui-font-red">￥212.00</strong></p>
                </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productdetailinfo() %>">
                    <img src="http://img.muyingzhijia.com/product/normal/66158_01_01.jpg" />
                    <p>
                        商品编号：<strong>119902</strong></p>
                    <p>
                        名称：<strong>商品名称商品名称商品名称22</strong></p>
                    <p>
                        单价：<strong>￥212.00</strong></p>
                    <p>
                        数量：<strong>1</strong></p>
                    <p>
                        小计：<strong class="ui-font-red">￥212.00</strong></p>
                </a></li>
                <li data-icon="arrow-l" data-iconpos="left"><a href="<%= MobileSite.BaseLib.WebUrls.orderlist() %>">返回</a> </li>
            </ul>
            <div data-role="">
                <a href="<%= MobileSite.BaseLib.WebUrls.onlinepayment() %>" data-role="button" data-inline="true" data-theme="e">在线支付</a><a
                    href="#" data-role="button" data-inline="true" data-mini="true">取消订单</a></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
</asp:Content>
