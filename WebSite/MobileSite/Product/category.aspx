<%@ Page Title="" Language="C#" MasterPageFile="~/MultiPage.Master" AutoEventWireup="true"
    CodeBehind="category.aspx.cs" Inherits="MobileSite.Product.category1" %>

<%@ Register Src="/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="page" id="Product_Category_Page" data-title="商品分类">
        <div data-role="header" data-theme="a">
            <uc1:Header ID="Header1" runat="server" />
            <div data-role="navbar" data-iconpos="left">
                <ul>
                    <li><a href="<%= MobileSite.BaseLib.WebUrls.category() %>" data-theme="c" data-icon="grid">商品分类</a> </li>
                    <li><a href="#Product_SubCategory_Page" data-theme="c" data-icon="arrow-l" data-rel="back">返回</a>
                    </li>
                </ul>
            </div>
        </div>
        <div data-role="content">
            <div id="categoryList" class="content-primary">
            </div>
        </div>
        <uc2:Footer ID="Footer1" runat="server" />
    </div>
    <div data-role="page" id="Product_SubCategory_Page" data-title="商品二级分类">
        <div data-role="header" data-theme="a">
            <uc1:Header ID="Header2" runat="server" />
            <div data-role="navbar" data-iconpos="left">
                <ul>
                    <li><a href="<%= MobileSite.BaseLib.WebUrls.category() %>" data-theme="c" data-icon="grid">商品分类</a> </li>
                    <li><a href="#Product_SubCategory_Page" data-theme="c" data-icon="arrow-l" data-rel="back">返回</a>
                    </li>
                </ul>
            </div>
        </div>
        <div data-role="content">
            <div class="content-primary">
                <ul id="sub_categoryList" data-role="listview" data-inset="true" data-theme="c" data-divider-theme="c">
                </ul>
            </div>
        </div>
        <uc2:Footer ID="Footer2" runat="server" />
    </div>
    <script id="categoryList_template" type="text/template">
        {#foreach $T as dr begin=0 count=1}
        <div data-role="collapsible" data-theme="e" data-collapsed="false">
                <h3>
                    {$T.dr.name}{$T.begin}</h3>
                <ul data-role="listview">
                    {#foreach $T.dr.child as drC}
                    <li><a class="SubZC" onclick="ShowDetails({#var $T.drC})" href="javascript:void(null);" >{$T.drC.name}</a></li>
                    {#/for}
                </ul>
            </div>
        {#/for}
        {#foreach $T as dr begin=1 to last}
        <div data-role="collapsible" data-theme="e" data-collapsed="true">
                <h3>
                    {$T.dr.name}{$T.begin}</h3>
                <ul data-role="listview">
                    {#foreach $T.dr.child as drC}
                    <li><a class="SubZC" onclick="ShowDetails({#var $T.drC})" href="javascript:void(null);" >{$T.drC.name}</a></li>
                    {#/for}
                </ul>
            </div>
        {#/for}
    </script>
    <script id="sub_category_template" type="text/template">
        <li data-role="list-divider">{$T.name}</li>
        {#foreach $T.child as drCC}
        <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >{$T.drCC.name}</a>
        </li>
        {#/for}
    </script>
</asp:Content>
