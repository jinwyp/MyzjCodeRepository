<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="addresslist.aspx.cs" Inherits="MobileSite.CheckOut.addresslist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" data-theme="c" data-icon="star">购物车</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.orderconfirm() %>" data-theme="c" data-icon="arrow-l"  data-rel="back">返回</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
    <form id="frmAddress_list">
        <a href="<%= MobileSite.BaseLib.WebUrls.address_add() %>" data-role="button" data-theme="d" data-icon="plus" >新增一个收货地址
        </a>
        <div data-role="fieldcontain">
            <fieldset id="addresslist_cont" data-role="controlgroup">
                
            </fieldset>
        </div>
        <%--<a id="addresslist_btn" href="javascript:void(0)" data-role="button" data-theme="a" >确认保存 </a>--%>
        <input id="addresslist_btn" value="确认保存" type="submit" data-theme="e"/>
        <span class="error" id="Add_mesage"></span>
        </form>
    </div>
    <script id="addresslist_cont_template" type="text/template">
        {#foreach $T as dr}
        <div data-role="controlgroup">
                    <input type="radio" name="radio_choice_add" id="radio_choice_add_{$T.dr.id}" value="{$T.dr.id}" {#if $T.dr.get_def==true} checked="checked" {#/if}  class="required" />
                    <label for="radio_choice_add_{$T.dr.id}">
                        <p>
                            收货人：{$T.dr.contact_name}</p>
                        <p>
                            收货地址：{$T.dr.province}{#if $T.dr.province==$T.dr.city}{#elseif}{$T.dr.city}{#/if}{$T.dr.county},{$T.dr.addr}</p>
                        <p>
                            邮编：{$T.dr.zip}</p>
                        <p>
                            电话：{$T.dr.mobile}</p>
                    </label>
                    <a href='<%= MobileSite.BaseLib.WebUrls.address_edit("{$T.dr.id}") %>' data-role="button" data-theme="d" >修改</a>
                </div>
        {#/for}
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
<script src="<%= MobileSite.BaseLib.WebUrls.JsRoot() %>bk/jquery.validate.min.js" type="text/javascript"></script>
<script src="<%= MobileSite.BaseLib.WebUrls.JsRoot() %>mobile.checkout.js" type="text/javascript"></script>
</asp:Content>
