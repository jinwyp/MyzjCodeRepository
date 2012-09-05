<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="getpassword-success.aspx.cs" Inherits="MobileSite.getpassword_success" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.index() %>" data-theme="c" data-icon="home">
                首页</a></li>
            <li><a data-theme="c" data-icon="arrow-l" data-rel="back">返回</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content" id="getpassword_success" >
    </div>
    <script id="template_getpassword_success" type="text/template">
        <div class="p10">
            <p>
                找回密码成功<br />
                请访问邮箱 {$T.email} 查看密码
                <br />
                <a target="_blank" href="{$T.emaildomain}">点击进入@{$T.emailname}</a></p>
            <p>
                如果您长时间收不到邮件可以尝试：</p>
            <p>
                致电客服电话 400-820-1000</p>
            <p>
                或给母婴之家客服发送邮件 <a href="mailto:kf@muyingzhijia.com">kf@muyingzhijia.com</a></p>
        </div>
    </script>
</asp:Content>
