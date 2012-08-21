<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="deliverylist.aspx.cs" Inherits="MobileSite.deliverylist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.index() %>" data-theme="c" data-icon="home" rel="external">首页</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" data-theme="c" data-icon="check" rel="external">购物车</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.orderconfirm() %>" data-theme="e" data-icon="arrow-r" rel="external">提交订单</a> </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div data-role="fieldcontain" id="deliverylist_cont">
            
        </div>
        <div id="sh_time" data-role="fieldcontain" style="display:none;">
            <fieldset data-role="controlgroup" data-type="">
                <legend>选择送货时间</legend>
                <input type="radio" name="radio-date-1" id="radio-date-1" value="1" checked="checked" />
                <label for="radio-date-1">
                    只工作日送货（双休日、假日不用送）</label>
                <input type="radio" name="radio-date-1" id="radio-date-2" value="2" />
                <label for="radio-date-2">
                    工作日、双休日与假日均可送货</label>
                <input type="radio" name="radio-date-1" id="radio-date-3" value="3" />
                <label for="radio-date-3">
                    只双休日、假日送货（工作日不用送）</label>
            </fieldset>
        </div>
        <div id="fk_type" data-role="fieldcontain" style="display:none;">
            <legend>付款方式</legend>
            <fieldset data-role="controlgroup">
                <legend></legend>
                <input type="radio" name="radio-pay-1" id="radio-choice-1" value="1" checked="checked" />
                <label for="radio-choice-1">
                    现金</label>
                <input type="radio" name="radio-pay-1" id="radio-choice-2" value="3" />
                <label for="radio-choice-2">
                    POS机刷卡（仅支持带有银联标识的银行卡）</label>
            </fieldset>
        </div>
        <a id="deliverylist_sub_btn" href="javascript:void(0)" data-role="button" data-theme="a">确认保存</a>
    </div>
    <script id="deliverylist_cont_template" type="text/template">
    <fieldset data-role="controlgroup"><legend>选择配送方式</legend>
        {#foreach $T as dr}
        <input type="radio" name="radio-type-1" id="radio-type-{$T.dr.id}" value="{$T.dr.id}" />
        <label for="radio-type-{$T.dr.id}">{$T.dr.name}({$T.dr.remark})</label>
        {#/for}
    </fieldset>
    </script>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
<script src="<%= MobileSite.BaseLib.WebUrls.JsRoot() %>mobile.checkout.js" type="text/javascript"></script>
</asp:Content>
