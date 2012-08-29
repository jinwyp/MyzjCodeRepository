﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="MobileSite.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.index() %>" data-theme="c" data-icon="home" >首页</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.Login() %>" data-theme="e" data-icon="grid" >登录</a> </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <form id="frmLogin" action="myaccount.aspx">
        <div data-role="fieldcontain">
            <fieldset data-role="controlgroup" id="test">
                <label for="email">
                    邮箱地址</label>
                <input type="text" id="email" name="email" class="required email" placeholder="请填写Email地址"
                    value="" />
                <span id="ErroMesg" class="error" generated="true" style="display: none"></span>
            </fieldset>
        </div>
        <div data-role="fieldcontain">
            <fieldset data-role="controlgroup">
                <label for="password">
                    密码</label>
                <input type="password" id="password" name="password" class="required" placeholder="请输入密码"
                    value="" />
            </fieldset>
        </div>
        <input data-theme="b" value="登录" type="submit" />
        <div class="ui-grid-a p10">
            <div class="ui-block-a">
                <a href="forgetpassword.aspx" >忘记密码</a>
            </div>
            <div class="ui-block-b">
                <a href="registration.aspx" >立即注册</a>
            </div>
        </div>
        <%--<ul data-role="listview" data-divider-theme="c" data-inset="true">
            <li data-role="list-divider" role="heading">使用其他合作网站登录</li>
            <li><a href="#page1">
                <img src="/images/16-sinaweibo.png" class="ui-li-icon" />新浪微博登录</a> </li>
            <li><a href="#page1" style="padding-left: 60px;">
                <img src="/images/alipay.png" class="ui-li-icon" />支付宝登录</a> </li>
            <li><a href="#page1">
                <img src="/images/qq.png" class="ui-li-icon" />QQ登录</a> </li>
        </ul>--%>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
    <script src="<%= MobileSite.BaseLib.WebUrls.JsRoot() %>bk/jquery.validate.min.js" type="text/javascript"></script>
</asp:Content>
