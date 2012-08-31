<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true" CodeBehind="category.aspx.cs" Inherits="MobileSite.Product.category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<div data-role="navbar" data-iconpos="left">
                <ul>
                    <li>
                        <a href="<%= MobileSite.BaseLib.WebUrls.category() %>" data-theme="c" data-icon="grid" >商品分类</a>
                    </li>
                    <li>
                        <a data-theme="c" data-icon="arrow-l"  data-rel="back">返回</a>
                    </li>
                </ul>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div data-role="content">
            <div class="content-primary">

                    <div data-role="collapsible" data-collapsed="false" data-theme="e">
                        <h3>妈妈专区</h3>
                        <ul data-role="listview">
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">妈妈食品</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">母乳喂养用品</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">妈妈服饰</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">妈妈洗护</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">祛纹纤体</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">妈妈书籍</a>
                            </li>
                        </ul>
                    </div>
                    <div data-role="collapsible" data-collapsed="true"  data-theme="e">
                        <h3>宝宝食品</h3>
                        <ul data-role="listview">
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">牛奶粉</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">特殊配方奶粉</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">羊奶粉</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">营养保健</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">辅食</a>
                            </li>
                        </ul>
                    </div>
                    <div data-role="collapsible" data-collapsed="true"  data-theme="e">
                        <h3>哺育喂养</h3>
                        <ul data-role="listview">
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">奶瓶/奶嘴</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">安抚奶嘴</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">餐具</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">学饮杯</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">消毒/加温</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">辅食加工器具</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">辅助工具</a>
                            </li>
                        </ul>
                    </div>
                    <div data-role="collapsible" data-collapsed="true"  data-theme="e">
                        <h3>宝宝用品</h3>
                        <ul data-role="listview">
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">纸尿裤/防尿用品</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">洗浴</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">护肤</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">护理</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">安全防护</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">清洁/入厕用品</a>
                            </li>
                        </ul>
                    </div>
                    <div data-role="collapsible" data-collapsed="true"  data-theme="e">
                        <h3>宝宝服饰</h3>
                        <ul data-role="listview">
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">内衣</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">外出服</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">鞋袜帽</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">配件</a>
                            </li>
                        </ul>
                    </div>
                    <div data-role="collapsible" data-collapsed="true"  data-theme="e">
                        <h3>玩具图书音像</h3>
                        <ul data-role="listview">
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">玩具</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">图书音像</a>
                            </li>
                        </ul>
                    </div>
                    <div data-role="collapsible" data-collapsed="true"  data-theme="e">
                        <h3>车床椅/寝具</h3>
                        <ul data-role="listview">
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">安全汽座</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">童车</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">童床</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">餐椅/摇椅</a>
                            </li>
                            <li>
                                <a href="<%= MobileSite.BaseLib.WebUrls.category_sub() %>">寝具</a>
                            </li>
                        </ul>
                    </div>
                </div>

        </div>
</asp:Content>
