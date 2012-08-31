<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="address_edit.aspx.cs" Inherits="MobileSite.CheckOut.address_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.shoppingcart() %>" data-theme="c" data-icon="star"
                >购物车</a> </li>
            <li><a href="<%= MobileSite.BaseLib.WebUrls.orderconfirm() %>" data-theme="c" data-icon="arrow-l"  data-rel="back">返回</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <form id="frmAddress_Add" action="<%= MobileSite.BaseLib.WebUrls.addresslist() %>">
        <input type="hidden" id="acc_id" value="" />
        <div data-role="fieldcontain">
            <fieldset data-role="controlgroup" class="ui-hide-label">
                <label for="email">
                    收货人姓名
                </label>
                <input type="text" id="email" name="email" class="required" placeholder="请填写收货人姓名"
                    value="" />
            </fieldset>
        </div>
        <div data-role="fieldcontain">
            <fieldset id="PCR" data-role="controlgroup" class="ui-hide-label">
                <legend>选择收货地址所在地区</legend>
                <label for="select-choice-province">
                    省份</label>
                <select name="select-choice-province" id="s1s">
                </select>
                <label for="select-choice-city">
                    城市</label>
                <select name="select-choice-city" id="s2s">
                </select>
                <label for="select-choice-district">
                    区县</label>
                <select name="select-choice-district" id="s3s">
                </select>
                <input type="text" id="addr" name="addr" class="required" placeholder="请填写详细的的收货地址"
                    value="" />
            </fieldset>
        </div>
        <div data-role="fieldcontain" class="ui-hide-label">
            <fieldset data-role="controlgroup">
                <label for="zipCode">
                    请填写邮政编码
                </label>
                <input type="text" id="zipCode" name="zipCode" class="required" maxlength="6" placeholder="请填写邮政编码"
                    value="" />
            </fieldset>
        </div>
        <div data-role="fieldcontain">
            <fieldset data-role="controlgroup">
                <legend>地址类型</legend>
                <input type="radio" name="radio-choice-1" id="radio-choice-1" value="0" />
                <label for="radio-choice-1">
                    公司地址</label>
                <input type="radio" name="radio-choice-1" id="radio-choice-2" value="1" />
                <label for="radio-choice-2">
                    家庭地址</label>
                <input type="radio" name="radio-choice-1" id="radio-choice-3" value="2" />
                <label for="radio-choice-3">
                    其他</label>
            </fieldset>
        </div>
        <div data-role="fieldcontain" class="ui-hide-label">
            <fieldset data-role="controlgroup">
                <label for="mobilePhone">
                    请输入手机号码
                </label>
                <input type="text" id="mobilePhone" name="mobilePhone" class="required" maxlength="11" placeholder="请输入手机号码"
                    value="" />
            </fieldset>
        </div>
        <%--<a href="<%= MobileSite.BaseLib.WebUrls.addresslist() %>" data-role="button" data-theme="a">保存修改 </a>--%>
        <input value="保存修改" type="submit" data-theme="e" />
        <span class="error" id="Cah_Mesag" generated="true" style="text-align: center"></span>
        </form>
    </div>
</asp:Content>
