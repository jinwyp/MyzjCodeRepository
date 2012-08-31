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
                <div data-src="/images/slides/Accessories0705_1000x400.jpg">
                </div>
                <div data-src="/images/slides/Diapers0710_1000x400.jpg">
                </div>
                <div data-src="/images/slides/Milk0710_1.jpg">
                </div>
                <div data-src="/images/slides/Pampers0711_1000x400.jpg">
                </div>
                <div data-src="/images/slides/wanju0705_1000x4000.jpg">
                </div>
            </div>
            <!-- #foucs -->
            <!-- #camera_wrap_1 -->
            <ul data-role="listview" data-inset="true" data-theme="c">
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>"><span class="icon-custom icon-custom-01"></span>限时抢购</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>"><span class="icon-custom icon-custom-02"></span>促销快报</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>"><span class="icon-custom icon-custom-03"></span>新品上架</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>"><span class="icon-custom icon-custom-04"></span>热门单品</a>
                </li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.productlist() %>"><span class="icon-custom icon-custom-05"></span>推荐品牌</a>
                </li>
            </ul>
            <ul data-role="listview" data-inset="true" data-dividertheme="d">
                <li data-role="list-divider">公告11</li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.noticedetail() %>">在线客服试运行 </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.noticedetail() %>">在线客服试运行 </a></li>
                <li><a href="<%= MobileSite.BaseLib.WebUrls.noticedetail() %>">在线客服试运行 </a></li>
            </ul>
        </div>
    </div>
</asp:Content>
