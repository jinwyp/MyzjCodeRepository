<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="userpolicy.aspx.cs" Inherits="MobileSite.userpolicy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="index.aspx" data-theme="c" data-icon="home">首页</a> </li>
            <li><a data-theme="e" data-icon="grid" data-rel="back">返回</a> </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <h1>
            母婴之家服务协议</h1>
        <ul>
            <li>（一）只有注册会员才可以在母婴之家网上进行购物、订单查询、参加各项优惠活动、发表产品评论等。请记住您的会员号。它是您在母婴之家的唯一识别，您的任何投诉、问题、购买记录，均采用这个会员号处理。而且，您再次光临本站购物会有很大的方便，许多信息不必重新输入。</li>
            <li>（二）会员名可以是您便于记忆的任何代号比如网名。不过，在注册用户的时候，我们建议您务必输入您的真实信息，我们会为您绝对保密。这样有利于准确发货、提供各项服务、必要时迅速与您联系。</li>
            <li>（三）母婴之家首页中上方有"购物车"的链接，当您忘记密码时，请点击"取回密码" ，输入您注册的用户名，系统会自动发送您的密码到您的注册邮箱，您可以收到您的密码，下次登陆母婴之家时，您可以在母婴之家中可以重新设置一个您能记住的密码。</li>
            <li>（四）您随时可以登录"购物车"，进入"个人资料"查看并修改您的注册信息。</li>
            <li>（五）如果在使用中遇到程序问题或BUG，请给我们留言,我们会及时修正这些问题！谢谢支持！</li>
        </ul>
        <a href="registration.aspx?checked_user=true"  data-role="button" data-theme="b">同意该协议</a>
    </div>
</asp:Content>
