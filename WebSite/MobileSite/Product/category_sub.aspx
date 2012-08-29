<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="category_sub.aspx.cs" Inherits="MobileSite.Product.category_sub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.category() %>" data-theme="c" data-icon="grid"
                >商品分类</a> </li>
            <li><a data-theme="c" data-icon="arrow-l" data-rel="back">返回</a> </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div class="content-primary">
            <ul data-role="listview" data-inset="true" data-theme="c" data-divider-theme="c">
                <li data-role="list-divider">妈妈食品</li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >孕妇奶粉</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >叶酸</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >孕妇奶粉</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >叶酸</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >孕妇奶粉</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >叶酸</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >孕妇奶粉</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >叶酸</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >孕妇奶粉</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >叶酸</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >孕妇奶粉</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>" >叶酸</a>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
</asp:Content>
