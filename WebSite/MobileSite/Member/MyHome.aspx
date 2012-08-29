<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="MyHome.aspx.cs" Inherits="MobileSite.Member.MyHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.index() %>" data-theme="c" data-icon="home">首页</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.Login() %>" data-theme="e" data-icon="grid">登录</a> </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    asdfasdf
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
    <script src="js/bk/jquery.validate.min.js" type="text/javascript"></script>
</asp:Content>
