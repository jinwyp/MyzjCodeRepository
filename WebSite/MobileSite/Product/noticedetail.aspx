<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="noticedetail.aspx.cs" Inherits="MobileSite.Product.noticedetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.index() %>" data-theme="c" data-icon="home" >首页</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.myaccount() %>" data-theme="c" data-icon="arrow-l"  data-rel="back">返回</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div class="content-primary">
            <h2>
                在线客服试运行</h2>
            <p>
                2012/5/31 10:58:57</p>
        </div>
        <div>
            <p>
                亲爱的会员： 母婴之家在线客服正式上线了！现在您可在首页左侧的在线客服功能窗进行产品、配送相关问题的咨询或留言，我们的客服专员将在第一时间为您解答。 在线客服试运行期间，开放时间为工作日的上午9：00——晚上17：30，周末及节假日暂不开放。
                其它时间段，您可拨打我们的客服热线 400-820-1000 垂询。热线服务时间：每天上午8：00——晚上21：00。 祝您购物愉快！ 特此告知。</p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
</asp:Content>
