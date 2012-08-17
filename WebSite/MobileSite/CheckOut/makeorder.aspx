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
                订单号：</label><span class="ui-font-red">5259118</span>
        </div>
        <div data-role="fieldcontain">
            <label>
                应付金额：</label><span class="ui-font-red">576元</span>
        </div>
        <div data-role="fieldcontain">
            <label>
                支付方式：</label>在线支付
        </div>
        <div data-role="fieldcontain">
            <label>
                配送方式：</label>母婴之家快递
        </div>
        <div data-role="fieldcontain">
            <label>
                送货时间：</label>工作日
        </div>
        <a data-role="button" href="<%= MobileSite.BaseLib.WebUrls.onlinepayment() %>" data-theme="a">继续完成在线支付 </a><a data-role="button"
            href="<%= MobileSite.BaseLib.WebUrls.orderlist() %>" data-theme="c">查看订单 </a>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
</asp:Content>
