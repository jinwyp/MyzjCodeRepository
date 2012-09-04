<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="forgetpassword.aspx.cs" Inherits="MobileSite.forgetpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.index() %>" data-theme="c" data-icon="home" >首页</a> </li>
            <li><a data-theme="c" data-icon="arrow-l" data-rel="back">返回</a> </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <form id="frmgetpassword" action="getpassword-success.aspx">
        <div data-role="fieldcontain" class="ui-hide-label">
            <fieldset data-role="controlgroup">
                <label for="email">
                    邮箱地址</label>
                <input type="text" id="email" name="email" class="required email" placeholder="请填写您注册时的Email地址"
                    value="" />
            </fieldset>
        </div>
        <input id="forGet_btn" data-theme="b" value="找回密码" type="submit" />
        </form>
    </div>
</asp:Content>
