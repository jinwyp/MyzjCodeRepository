<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="noticedetail.aspx.cs" Inherits="MobileSite.Product.noticedetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.index() %>" data-theme="c" data-icon="home" rel="external">首页</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.myaccount() %>" data-theme="c" data-icon="arrow-l"  data-rel="back">返回</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content" id="noticedetailContent">
        
    </div>
    <script id="notice_detail_jTemplate" type="text/template">
        <div class="content-primary">
            <h2>
                {$T.title}</h2>
            <p>
                {$T.created}</p>
        </div>
        <div>
            <p>
                {$T.content}</p>
        </div>
    </script>
</asp:Content>
