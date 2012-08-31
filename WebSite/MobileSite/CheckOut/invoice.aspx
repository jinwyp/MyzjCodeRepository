<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="invoice.aspx.cs" Inherits="MobileSite.invoice" %>

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
        <div data-role="fieldcontain">
            <fieldset data-role="controlgroup">
                <legend>是否需要发票</legend>
                <input type="radio" name="IsNoInvoice" id="radio-need-1" value="0"/>
                <label for="radio-need-1">
                    否</label>
                <input type="radio" name="IsNoInvoice" id="radio-need-2" value="1" />
                <label for="radio-need-2">
                    个人</label>
                <input type="radio" name="IsNoInvoice" id="radio-need-3" value="2" />
                <label for="radio-need-3">
                    公司</label>
                <input type="text" id="FaTHeader" style="display:none" name="company" placeholder="请填写发票抬头"
                    value="" /><span class="error" id="Cah_Mesag" style="display:none"></span>
            </fieldset>
        </div>
        <div id="invoice_Type" data-role="fieldcontain" style="display:none">
            <fieldset data-role="controlgroup">
                <legend>发票类型</legend>
                <input type="radio" name="FaTtype" id="radio-type-1" value="2" />
                <label for="radio-type-1">
                    食品发票</label>
                <input type="radio" name="FaTtype" id="radio-type-2" value="1" />
                <label for="radio-type-2">
                    用品发票</label>
            </fieldset>
        </div>
        <a id="invoice_btn" href="javascript:void(0)" data-role="button" data-theme="a">确认保存</a>
    </div>
</asp:Content>
