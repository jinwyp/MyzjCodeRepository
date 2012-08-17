<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile.Master" AutoEventWireup="true"
    CodeBehind="detail.aspx.cs" Inherits="MobileSite.detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div data-role="navbar" data-iconpos="left">
        <ul>
            <li><a href="index.html" data-theme="c" data-icon="home">首页</a> </li>
            <li><a href="category.html#category" data-theme="c" data-icon="grid">商品分类</a> </li>
            <li><a href="category.html#subcategory" data-theme="e" data-icon="grid">妈妈食品</a>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="content">
        <div data-role="controlgroup" data-type="horizontal" class="groupbuttonfullwidth">
            <a href="#intro" data-theme="c" data-role="button" data-icon="arrow-d" data-mini="true">
                简介</a> <a href="#detail" data-theme="e" data-role="button" data-icon="arrow-d" data-mini="true">
                    详情</a> <a href="#comments" data-theme="c" data-role="button" data-icon="arrow-d"
                        data-mini="true">评论</a>
        </div>
        <div class="content-primary">
            <h2>
                商品名称 好奇金装贴身舒适纸尿裤超值特惠装L78片(品牌好奇)</h2>
            <div class="iteminfo">
                <fieldset>
                    <label>
                        品牌：</label>好奇</fieldset>
                <fieldset>
                    <label>
                        颜色：</label>无</fieldset>
                <fieldset>
                    <label>
                        产地：</label>韩国</fieldset>
                <fieldset>
                    <label>
                        规格：</label>L78片</fieldset>
                <fieldset>
                    <label>
                        材质：</label>
                </fieldset>
            </div>
            <div class="detailinfo">
                【产地】：韩国 </br>【适合阶段】：10-14KG </br>【产品规格】：78片装 3包/箱 </br>【保质期】：三年 </br>【主要原料】：无纺布，绒毛浆，PE膜，离分子吸收树脂
                </br>【产品特点】： 1、新环抱式加宽弹性腰围及腰贴：贴合宝宝身体曲线，像妈妈双手拥抱一般地舒适服贴。无论宝宝怎么动，都能有效防止侧漏、后漏。超大 弹性无胶腰贴：不粘宝宝皮肤，减少对宝宝肌肤的刺激并可反复粘贴。
                2、三层锁水，瞬吸干爽：新"速渗"导流表层让吸收速度增快，表层瞬间干爽不回渗，配合特有"三层锁水系统"均匀吸收水分，有效减少 小屁屁与尿液接触时间，让宝宝小屁屁加倍干爽。
                3、含天然护肤精华表层：吸收表层含芦荟-绿茶天然护肤精华，保护宝宝肌肤免受尿尿及臭臭的刺激。 4、柔棉感清爽透气外层：具超优透气功能，保持宝宝肌肤干爽，加倍舒适。
                5、立体透气防漏边：再好动的宝宝，也能有效防止腿边侧漏。 6、新增尿湿显示：妈妈不用解开尿裤就能确认是否需要更换，宝宝不受打扰，妈妈更方便。 【保存方法】：置于室内阴凉干燥处即可
                【注意事项】： 1、如有过敏现象，请暂停使用； 2、纸尿裤用完后应将秽物倒进侧内，但请勿将纸尿裤冲入厕内，或随处丢弃，将纸尿裤包装袋放在婴儿无法取到之处，以免引致窒息。
                3、婴孩将任何东西放入口中都有可能导致硬塞，避免发生误食及哽塞的危险，请勿让婴孩撕开纸尿裤及放入口中，撕裂的纸尿裤应丢弃，如一般衣物遇火会燃烧，应让孩子远离火源。
                </br>【厂家官方网站】：www.huggies.com.cn</div>
        </div>
        <a href="shoppingcart.html" data-role="button" data-icon="add" data-iconpos="left"
            data-theme="a">加入购物车</a>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Other" runat="server">
    <script src="js/bk/jquery.mobile.swipegallery.js"></script>
    <script src="js/bk/myapp.js"></script>
</asp:Content>
