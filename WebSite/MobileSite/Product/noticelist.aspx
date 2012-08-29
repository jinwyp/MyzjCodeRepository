<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="noticelist.aspx.cs" Inherits="MobileSite.Product.noticelist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.index() %>" data-theme="c" data-icon="home" >首页</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.myaccount() %>" data-theme="c" data-icon="arrow-l"  data-rel="back">返回</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div class="content-primary">
            <ul data-role="listview" data-inset="true" data-dividertheme="d">
                <li data-role="list-divider">公告</li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.noticedetail() %>">在线客服试运行 </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.noticedetail() %>">在线客服试运行 </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.noticedetail() %>">在线客服试运行 </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.noticedetail() %>">在线客服试运行 </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.noticedetail() %>">在线客服试运行 </a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
</asp:Content>
