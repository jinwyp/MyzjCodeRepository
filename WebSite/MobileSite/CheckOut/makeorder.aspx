<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="makeorder.aspx.cs" Inherits="MobileSite.CheckOut.makeorder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <h1 class="ui-font-green">
            订单已提交</h1>
        <div data-role="fieldcontain">
            <label>
                订单号：</label><span class="ui-font-red" id="make_oid"></span>
        </div>
        <div data-role="fieldcontain">
            <label>
                应付金额：</label><span class="ui-font-red" id="make_total_order"></span>
        </div>
        <div data-role="fieldcontain">
            <label>
                支付方式：</label><span id="make_paytype"></span>
        </div>
        <div data-role="fieldcontain">
            <label>
                配送方式：</label><span id="make_logisticstype"></span>
        </div>
        <div data-role="fieldcontain">
            <label>
                送货时间：</label><span id="make_posttimetype"></span>
        </div>
        <a id="online_pay" style="display: none;" data-role="button" href="<%= MobileSite.BaseLib.WebUrls.onlinepayment("{0}","{1}") %>"
            data-theme="a">继续完成在线支付 </a><a id="checkOrderDetail" data-role="button" href="" data-theme="c">
                查看订单 </a>
        <input type="hidden" id="orderdetail_url" value="<%= MobileSite.BaseLib.WebUrls.orderdetail("") %>" />
    </div>
</asp:Content>
