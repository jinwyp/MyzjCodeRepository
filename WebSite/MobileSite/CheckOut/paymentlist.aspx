<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="paymentlist.aspx.cs" Inherits="MobileSite.paymentlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" data-theme="c" data-icon="star">
                购物车</a></li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.orderconfirm() %>" data-theme="c" data-icon="arrow-l"
                data-rel="back">返回</a></li>
            
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div data-role="fieldcontain" id="field_pay_list">
            <div style="text-align:center; background:#ffffff url(../images/loading.gif) no-repeat center center; height:60px;"></div>
        </div>
        <a href="javascript:void(0)" id="paymentlist_sub_btn" data-role="button" data-theme="a">
            确认保存</a>
    </div>
    <script id="jTemplate" type="text/template">
    <fieldset data-role="controlgroup">
    <legend>请选择付款方式</legend>
    {#foreach $T as dr}
                <input type="radio" name="radio-pay-1" id="radio-choice-{$T.dr.paytype}" value="{$T.dr.paytype}" />
                <label text="{$T.dr.payname}" for="radio-choice-{$T.dr.paytype}">
                    {$T.dr.payname}{$T.dr.remark}</label>
    {#/for}
    </fieldset>
    </script>
</asp:Content>
