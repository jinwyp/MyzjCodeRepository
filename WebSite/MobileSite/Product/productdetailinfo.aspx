<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="productdetailinfo.aspx.cs" Inherits="MobileSite.productdetailinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.category() %>" data-theme="c" data-icon="grid">
                商品分类</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>" data-theme="c" data-icon="arrow-l"
                data-rel="back">返回</a> </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div data-role="controlgroup" data-type="horizontal" class="groupbuttonfullwidth">
            <a id="intro_a" data-theme="e" data-role="button" data-icon="arrow-d" data-mini="true">
                简介</a> <a id="detail_a" data-theme="e" data-role="button" data-icon="arrow-d" data-mini="true">
                    详情</a><%-- <a id="comments_a" data-theme="c" data-role="button" data-icon="arrow-d" data-mini="true">
                        评论</a>--%>
        </div>
        <div class="content-primary">
            <h2 id="h2_title">
            </h2>
            <div id="intro" title="商品简介">
                <div class="slides" id="imggallery">
                    <ul id="imggallery_iteminfo">
                    </ul>
                </div>
                <div class="p10">
                </div>
                <div id="imgNex_Pre" data-role="controlgroup" data-type="horizontal" data-theme="c"
                    class="page">
                    <a href="#prev" data-role="button" data-icon="arrow-l" data-iconpos="left" data-mini="true"
                        id="imggalleryprev">上一页</a> &nbsp;&nbsp;<span id="imggallerynum">1/1</span>&nbsp;&nbsp;
                    <a href="#next" data-role="button" data-icon="arrow-r" data-iconpos="right" data-mini="true"
                        id="imggallerynext">下一页</a>
                </div>
                <div data-role="">
                </div>
                <div class="iteminfo" id="iteminfo">
                </div>
            </div>
            <div id="detail" title="商品详情">
                <div id="P_attrs" class="ui-grid-a" style="font-weight: normal; font-size: 12px;
                    line-height: 20px;">
                </div>
                <div id="P_desc" style="display: none">
                </div>
                <script id="P_attrs_Template" type="text/template">
                {#foreach $T.attrs as attr_p}
                <div class="ui-block-b ui-li-desc" style="margin: 0;">
                    <label class="ui-font-width">{$T.attr_p.key}</label>：{$T.attr_p.value}</div>
                {#/for}
                </script>
            </div>
            <%-- <a id="AddShop_btn" href="javascript:void(0)" data-role="button" data-icon="add"
                data-iconpos="left" data-theme="a" rel="external">加入购物车</a>--%>
            <input id="AddShop_btn" type="button" data-icon="add" data-iconpos="left" data-theme="a"
                value="加入购物车" />
            <script id="productDetailContent" type="text/template">
                <fieldset>
                    <label>
                        市场价：</label>
                    <del>￥{$T.marketprice}</del>
                </fieldset>
                <fieldset>
                    <label>
                        会员价：</label>
                    <strong class="ui-font-red">￥{$T.price}</strong>
                </fieldset>
                <fieldset>
                    <label>
                        库存：</label>{#if $T.stock == 0} 无货 {#else} 有货 {#/if}
                </fieldset>
                <fieldset>
                    <label>
                        商品编号：</label>{$T.productcode}
                </fieldset>
                <fieldset>
                    <label>
                        幸运星：</label>{$T.score}
                </fieldset>
            </script>
            <script id="picList" type="text/template">
                {#foreach $T.list as Puct}
                    <li>
                        <img src="{$T.Puct.url}" style="background:url(/images/errorImg_big.jpg) no-repeat center;" onerror='this.src="/images/errorImg_big.jpg"' width="300"
                            height="300" /></li>
                {#/for} 
            </script>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
    <script src="<%= MobileSite.BaseLib.WebUrls.JsRoot() %>bk/jquery.mobile.swipegallery.js"></script>
    <script src="<%= MobileSite.BaseLib.WebUrls.JsRoot() %>mobile.member.js"></script>
</asp:Content>
