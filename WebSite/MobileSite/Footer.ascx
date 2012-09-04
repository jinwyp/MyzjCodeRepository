<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="MobileSite.Footer" %>
<div data-role="footer" data-theme="c" class="p10">
    <div class="ui-grid-b" style="padding-bottom: 5px;">
        <div class="ui-block-a">
            <a href="<%= MobileSite.BaseLib.WebUrls.Login() %>">登录</a>
        </div>
        <div class="ui-block-b">
            <a href="<%= MobileSite.BaseLib.WebUrls.registration() %>">注册</a>
        </div>
        <div class="ui-block-c">
            <a href="http://www.muyingzhijia.com/index.aspx?refer=mobile">电脑版</a>
        </div>
    </div>
    <div>
        <span data-role="button" data-icon="arrow-u" data-iconpos="left" class="gotop">回顶部</span>
    </div>
    <div>
        Copyright 2004 - 2012 muyingzhijia.com All Rights Reserved</div>
</div>
