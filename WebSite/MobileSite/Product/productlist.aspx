<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="productlist.aspx.cs" Inherits="MobileSite.productlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.category() %>" data-theme="c" data-icon="grid" >商品分类</a> </li>
            <li><a data-theme="c" data-icon="grid" data-rel="back">返回</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div class="content-primary">
            <div data-role="controlgroup" data-type="horizontal" class="groupbuttonfullwidth">
                <a id="sales" data-theme="e" data-role="button" data-icon="arrow-d" data-mini="true">
                    销量</a> <a id="price" data-theme="e" data-role="button" data-icon="arrow-d" data-mini="true">
                        价格</a> <a id="upTime" data-theme="e" data-role="button" data-icon="arrow-d" data-mini="true">
                            上架时间</a>
            </div>
            <div class="p10">
            </div>
            <ul id="productlistContent" data-role="listview" data-filter="true" data-filter-placeholder="快速筛选">
            </ul>
            <div class="p10">
            </div>
            <div data-role="controlgroup" data-type="horizontal" data-mini="true" data-theme="c"
                class="page">
                <%--<a href="#prev" data-role="button" data-icon="arrow-l" data-iconpos="left" class="paging"
                    id="PrevPage">上一页</a>&nbsp;&nbsp;<span id="CurrentPages">1/3</span>&nbsp;&nbsp;<a
                        href="#next" data-role="button" data-icon="arrow-r" data-iconpos="right" id="NextPage"
                        class="paging">下一页</a>--%>
                        <div id="loading_list"></div>
                       <a href="#" data-role="button" data-icon="arrow-d" data-iconpos="left" id="morePage">再显示10条&nbsp;共<label id="totalCount"></label>件商品</a>
            </div>
        </div>
    </div>
    <script id="jTemplate" type="text/template">
    {#foreach $T.list as Puct}
        <li><a href='<%= MobileSite.BaseLib.WebUrls.productdetailinfo("{$T.Puct.gid}") %>' >
                    <img src='{$T.Puct.pic_url}' onerror='this.src="/images/errorImg_small.jpg"' />
                    <h3>
                        {$T.Puct.title}</h3>
                    <p>
                        价格： <strong class="ui-font-red">￥{$T.Puct.price}</strong></p>
                </a></li>
    {#/for}
    </script>
</asp:Content>
