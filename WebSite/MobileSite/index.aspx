<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="MobileSite.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%= MobileSite.BaseLib.WebUrls.ThemesRoot() %>camera.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content" style="padding: 0">
        <form action="<%= MobileSite.BaseLib.WebUrls.productlist() %>">
        <fieldset data-role="controlgroup" data-mini="true">
            <label for="searchinput1">
            </label>
            <input id="searchinput1" placeholder="商品搜索" value="奶粉" type="search" />
        </fieldset>
        </form>
        <div class="content-primary">
            <div class="camera_wrap camera_azure_skin" id="foucsPic">
            </div>
            <div id="picList" style="display: none;">
                <%--<div data-src="/images/slides/Accessories0705_1000x400.jpg">
                </div>
                <div data-src="/images/slides/Diapers0710_1000x400.jpg">
                </div>
                <div data-src="/images/slides/Milk0710_1.jpg">
                </div>
                <div data-src="/images/slides/Pampers0711_1000x400.jpg">
                </div>
                <div data-src="/images/slides/wanju0705_1000x4000.jpg">
                </div>--%>
            </div>
            <!-- #foucs -->
            <!-- #camera_wrap_1 -->
            <ul data-role="listview" data-inset="true" data-theme="c" id="columnlistContent">
                <%--<li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>">限时抢购</a> </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>">促销快报</a> </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>">新品上架</a> </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>">热门单品</a> </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>">推荐品牌</a> </li>--%>
            </ul>
            <ul data-role="listview" data-inset="true" data-dividertheme="d" id="noticelistContent">
            </ul>
            <script id="columnlist_jTemplate" type="text/template">
                        {#foreach $T.list as col}
                        <li><a href="<%= MobileSite.BaseLib.WebUrls.GoodsTopic("{$T.col.id}") %>">{$T.col.title}</a></li>
                        {#/for}
            </script>
            <script id="notice_jTemplate" type="text/template">
            <li data-role="list-divider">公告</li>
            {#foreach $T.list as notice}
            <li><a href="<%= MobileSite.BaseLib.WebUrls.noticedetail("{$T.notice.id}") %>">{$T.notice.title}</a></li>
            {#/for}
            </script>
        </div>
    </div>
</asp:Content>
