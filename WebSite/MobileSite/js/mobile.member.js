//var AddScript=function(jsPath) {
//    var urlArray = new Array();
//    urlArray = jsPath.split(',');

//    for (var i = 0; i < urlArray.length; i++) {
//        var ta = document.createElement("script"); ta.type = "text/javascript"; ta.async = true;
//        ta.src = urlArray[i];
//        document.getElementsByTagName("head")[0].appendChild(ta);
//    }
//};
//AddScript("js/jquery-jtemplates.js,js/jquery.cookie.js,js/json2.js,js/md5.js,js/mobile.common.js");

//#region 退出登录
function LoingOut() {
    jQuery("#logout").click(function () {
        var token = $.cookie("m_token");
        var uid = $.cookie("m_uid");
        GetWcf({
            _api: "Member.logout"
        }, function (json) {
            if (json.status == 1) {
                RemoveLoginCookie();
                $.cookie("currentPage", null);
                window.location.href = window.WebRoot + "index.aspx";
            } else
                alert(json.msg);
        }, true);
    });
}
//#endregion

//#region 首页动画
function Index_Fun() {
    //alert("aaa");
    jQuery('#foucsPic').camera({
        thumbnails: false,
        pauseOnClick: false,
        pagination: false,
        loader: 'bar',
        fx: 'scrollHorz',
        playPause: false,
        time: 4000,
        transPeriod: 1500
    });

    //    $('#camera_wrap_1').camera({
    //        thumbnails: false,
    //        pagination: false,
    //        fx: 'scrollHorz',
    //        playPause: false,
    //        mobileNavHover: true,
    //        time: 4000,
    //        transPeriod: 1500
    //    });
}
//#endregion

//#region 登录
function Login_Fun() {
    $("#email").focus(function () {
        jQuery("#ErroMesg").css("display", "none").text("");
    });
    $("#frmLogin").validate({
        rules: {
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 6,
                maxlength: 20
            }
        },

        messages: {
            email: {
                required: "请输入Email地址",
                email: "请输入正确的email地址"
            },
            password: {
                required: "请输入密码",
                minlength: jQuery.validator.format("密码不能小于{0}个字符"),
                maxlength: jQuery.validator.format("密码不能最多超过{0}的字符")
            }
        },
        submitHandler: function (form) {
            var email = $("#email").val();
            var password = $("#password").val();

            var jobj = { uid: email, pwd: password };


            PostWcf({
                _api: "Member.Login",
                _data: JSON.stringify(jobj)
            }, function (json) {
                if (json.status == 1 && typeof (json.data) == "string" && json.data.length > 0) {
                    UpdateLoginCookie(json.info, email, json.data);

                    var wUr = GetUrlParam("returnurl") || window.WebRoot + "Member/myaccount.aspx";
                    if (wUr === "undefined") wUr = window.WebRoot + "Member/myaccount.aspx";
                    LS.clear();
                    window.location.href = wUr;
                } else
                    jQuery("#ErroMesg").css("display", "block").text(json.msg);
            }, true);
        }
    });
}
//#endregion

//#region 注册
function Register_Fun() {
    //alert(/checked_user/i.test(window.location.href));
    if (/checked_user/i.test(window.location.href)) {
        $("#checkbox_agree").attr("checked", true).checkboxradio("refresh");
    }
    $("#email").focus(function () {
        jQuery("#ErroMesg").css("display", "none").text("");
    });
    $("#email").val($.cookie("c_email"));
    $("#password").val($.cookie("c_password"));
    $("#mobile").val($.cookie("c_mobile"));

    $("#select-choice-year").val($.cookie("c_babybirth_year")).selectmenu('refresh');
    $("#select-choice-month").val($.cookie("c_babybirth_month")).selectmenu('refresh');
    InitDate();
    $("#select-choice-day").val($.cookie("c_babybirth_day")).selectmenu('refresh');

    jQuery("#userpolicy").click(function () {
        var email = $("#email").val();
        var password = $("#password").val();
        var mobile = $("#mobile").val();
        var babybirth_year = $("#select-choice-year").val();
        var babybirth_month = $("#select-choice-month").val();
        var babybirth_day = $("#select-choice-day").val();
        $.cookie("c_email", email);
        $.cookie("c_password", password);
        $.cookie("c_mobile", mobile);
        $.cookie("c_babybirth_year", babybirth_year);
        $.cookie("c_babybirth_month", babybirth_month);
        $.cookie("c_babybirth_day", babybirth_day);
    });

    $("#frmReg").validate({
        errorElement: "span",
        rules: {
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 6,
                maxlength: 20
            },
            confirm_password: {
                required: true,
                minlength: 6,
                maxlength: 20,
                equalTo: "#password"
            },
            mobile: {
                required: true,
                number: true,
                rangelength: [11, 11]
            },
            checkbox_agree: "required"
        },

        messages: {
            email: {
                required: "请输入Email地址",
                email: "请输入正确的email地址"
            },
            password: {
                required: "请输入密码",
                minlength: jQuery.validator.format("密码不能小于{0}个字符"),
                maxlength: jQuery.validator.format("密码不能最多超过{0}的字符")
            },
            confirm_password: {
                required: "请输入确认密码",
                minlength: jQuery.validator.format("确认密码不能小于{0}个字符"),
                maxlength: jQuery.validator.format("密码不能最多超过{0}的字符"),
                equalTo: "两次输入密码不一致"
            },
            mobile: {
                required: "请输入手机号",
                number: "请输入有效的手机号码",
                rangelength: "手机号码只能是{0}位"
            },
            checkbox_agree: {
                required: "您还没有同意母婴之家”注册条款“！"
            },
            year: {
                required: "请您填写宝宝生日或预产期"

            },
            month: {
                required: "请您填写宝宝生日或预产期"

            },
            day: {
                required: "请您填写宝宝生日或预产期"

            }
        },
        groups: {
            username: "year month day"
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") == "year" || element.attr("name") == "month" || element.attr("name") == "day")
                error.appendTo("div.error");

            else
                error.insertAfter(element);
        },
        submitHandler: function (form) {
            if (!$("#checkbox_agree").attr("checked")) {
                return;
            }
            var email = $("#email").val();
            var password = $("#password").val();
            var confirm_password = $("#confirm_password").val();
            var mobile = $("#mobile").val();
            var babybirthday = $("#select-choice-year").val() + "/" + $("#select-choice-month").val() + "/" + $("#select-choice-day").val();

            var jsonData = {};
            jsonData.pwd = password;
            jsonData.uid = email;
            jsonData.email = email;
            jsonData.mobile = mobile;
            jsonData.babybirthday = DateToTimePoke(babybirthday);
            jsonData.registertype = 6;

            PostWcf({
                _api: "Member.Register",
                _data: JSON.stringify(jsonData)
            }, function (json) {
                if (json.status == 1 && typeof (json.data) == "string" && json.data.length > 0) {
                    UpdateLoginCookie(json.info, email, json.data);
                    $.cookie("c_email", null);
                    $.cookie("c_password", null);
                    $.cookie("c_mobile", null);
                    $.cookie("c_babybirth_year", null);
                    $.cookie("c_babybirth_month", null);
                    $.cookie("c_babybirth_day", null);
                    window.location.href = window.WebRoot + "Member/myaccount.aspx";
                }
                else
                    jQuery("#ErroMesg").css("display", "block").text(json.msg);
            }, true);
        }
    });
}
//#endregion

//#region 忘记密码
function Forgetpassword() {
    jQuery("#frmgetpassword").validate({
        errorElement: "span",
        rules: {
            email: {
                required: true,
                email: true
            }
        },

        messages: {
            email: {
                required: "请输入Email地址",
                email: "请输入正确的email地址 / 填写的用户名不存在"
            }
        }

    });

}
//#endregion

//#region 用户协议
function userpolicy_Fun() {
    var url = window.location.href.toString().replace("userpolicy.aspx?", "");
    for (var i = 0; i < url.split("&").length; i++) {
        alert(url.split("&")[i]);
    }
}
//#endregion

//#region 购物车页全局变量
var currentPage = $.cookie("currentPage") || 1; //当前页
//alert(":p1"+currentPage);
//if (window.location.href.toString().indexOf('&page') > 0) {
//    if ($.mobile.path.parseUrl(window.location.href).search.toString().split('&')[1].replace("page=", "") > 0) {
//        currentPage = $.mobile.path.parseUrl(window.location.href).search.toString().split('&')[1].replace("page=", "");
//    }
//}
//alert(":p2" + currentPage);
var lastPage = 1; //总页数
var pageSize = 10; //每页显示的条数
//#endregion

//#region 销量、价格、上架时间
var sorts = $.cookie("sorts") || "100";
//var urls_ca = $.cookie("urls_ca") || "productlist.aspx?100";
//if ($.mobile.path.parseUrl(window.location.href).search != "") {
//    urls_ca = "productlist.aspx" + $.mobile.path.parseUrl(window.location.href).search;
//}
//alert(urls_ca);
//#endregion

//#region 销量、价格、上架时间变换过程
function Salec_Price_newTime_Cha_Fun(sorts_cookie, asc_c, aseac_c) {
    if ($.cookie("sorts") != asc_c && $.cookie("sorts") != aseac_c) {
        $.cookie("sorts", asc_c); 
        //urls_ca = "productlist.aspx?" + asc_c;
    } else if ($.cookie("sorts") == asc_c && $.cookie("sorts") != aseac_c) {
        $.cookie("sorts", aseac_c); 
        //urls_ca = "productlist.aspx?" + aseac_c;
    } else if ($.cookie("sorts") == aseac_c && $.cookie("sorts") != asc_c) {
        $.cookie("sorts", asc_c); 
        //urls_ca = "productlist.aspx?" + asc_c;
    }
    //$.cookie("urls_ca", urls_ca);
    $.cookie("currentPage", 1);
    currentPage = 1;
    //DisplayUI(1, urls_ca);
    DisplayUI(1, $.cookie("sorts"));
}
//#endregion

//#region 销量、价格、上架时间调用
function Salec_Price_newTime_Fun() {
    //#region 100升序 101降序
    $("#sales").click(function () {
        $('#productlistContent').empty();
        Salec_Price_newTime_Cha_Fun($.cookie("sorts"), "100", "101");
    });
    //#endregion

    //#region 200升序 201降序
    $("#price").click(function () {
        $('#productlistContent').empty();
        Salec_Price_newTime_Cha_Fun($.cookie("sorts"), "200", "201");
    });
    //#endregion

    //#region 300升序 301降序
    $("#upTime").click(function () {
        $('#productlistContent').empty();
        Salec_Price_newTime_Cha_Fun($.cookie("sorts"), "300", "301");
    });
    //#endregion
}
//#endregion

//#region 改变按钮样式
function Change_DateIcon_Fun(id_c, arrow_c, ui_arrow_1s, ui_arrow_2s, ui_btn_1s, ui_btn_2s) {
    var tabs = $.cookie("tabs");
    $("#sales,#price,#upTime").removeAttr("data-icon").attr("data-icon", "arrow-d");
    $("#sales,#price,#upTime").children().children().next().removeClass("ui-icon-arrow-u").addClass("ui-icon-arrow-d");

    $(id_c).removeAttr("data-icon").attr("data-icon", arrow_c);
    $(id_c).children().children().next().removeClass(ui_arrow_1s).addClass(ui_arrow_2s);

    $("#sales,#price,#upTime").removeClass("ui-btn-up-e").addClass("ui-btn-up-c");
    $("#intro_a,#detail_a").removeClass("ui-btn-up-e").addClass("ui-btn-up-c");
    //$("#intro_a,#detail_a,#comments_a").removeClass("ui-btn-up-e").addClass("ui-btn-up-c");

    if (sorts == "?100" || sorts == "?101") {
        $("#sales").removeClass("ui-btn-up-c").addClass("ui-btn-up-e");
    }
    else if (sorts == "?200" || sorts == "?201") {
        $("#price").removeClass("ui-btn-up-c").addClass("ui-btn-up-e");
    }
    else if (sorts == "?300" || sorts == "?301") {
        $("#upTime").removeClass("ui-btn-up-c").addClass("ui-btn-up-e");
    }
    if (tabs == "intro") {
        $("#intro_a").removeClass("ui-btn-up-c").addClass("ui-btn-up-e");
    }
    else if (tabs == "detail") {
        $("#detail_a").removeClass("ui-btn-up-c").addClass("ui-btn-up-e");
    }
}
//#endregion

//#region 改变a便签样式
function Change_DateIcon_Diao_Fun(sorts) {
    console.log("sorts2:"+sorts);
    switch (sorts) {
        case "?100": Change_DateIcon_Fun("#sales", "arrow-u", "ui-icon-arrow-d", "ui-icon-arrow-u", "ui-btn-up-c", "ui-btn-up-e"); break;
        case "?101": Change_DateIcon_Fun("#sales", "arrow-d", "ui-icon-arrow-u", "ui-icon-arrow-d", "ui-btn-up-c", "ui-btn-up-e"); break;
        case "?200": Change_DateIcon_Fun("#price", "arrow-u", "ui-icon-arrow-d", "ui-icon-arrow-u", "ui-btn-up-c", "ui-btn-up-e"); break;
        case "?201": Change_DateIcon_Fun("#price", "arrow-d", "ui-icon-arrow-u", "ui-icon-arrow-d", "ui-btn-up-c", "ui-btn-up-e"); break;
        case "?300": Change_DateIcon_Fun("#upTime", "arrow-u", "ui-icon-arrow-d", "ui-icon-arrow-u", "ui-btn-up-c", "ui-btn-up-e"); break;
        case "?301": Change_DateIcon_Fun("#upTime", "arrow-d", "ui-icon-arrow-u", "ui-icon-arrow-d", "ui-btn-up-c", "ui-btn-up-e"); break;
        case "intro": Change_DateIcon_Fun("#intro", "", "", "", "ui-btn-up-c", "ui-btn-up-e"); break;
        case "detail": Change_DateIcon_Fun("#detail", "", "", "", "ui-btn-up-c", "ui-btn-up-e"); break;
        default:
    }
}
//#endregion

//#region 改变浏览器地址栏地址值
function Change_browser_Fun(urls) {
    if (window.history && window.history.pushState) {
        history.replaceState("", "", urls);
    }
    if (jQuery.browser.msie) {
        document.location.href = urls;
    }
}
//#endregion

//#region 显示界面数据列表
function DisplayUI(page, urls) {
    var token = $.cookie("m_token");
    var uid = $.cookie("m_uid");
    //var obj = $.mobile.path.parseUrl(urls);
    //obj = obj.search;
    //    if (obj == "" || obj == "undefined") {
    //        obj = "?100";
    //    }
    //sorts = obj.split('&')[0];
    //Change_browser_Fun(urls);
    //console.log("url:" + $.cookie("sorts"));
    var obj = $.cookie("sorts") || "100";
    //console.log("obj:" + obj);
    //console.log("sorts:" + "?" + obj);
    sorts = "?" + obj;
    Change_DateIcon_Diao_Fun(sorts);
    //Change_DateIcon_Diao_Fun(sorts);

    GetWcf({
        _api: "Goods.goodList",
        _url: "0/0/0/0/" + obj + "/" + page + "/" + pageSize
    }, function (jsonString) {
        if (jsonString.status == 1 && typeof (jsonString.list) == "object" && jsonString.list.length > 0) {
            lastPage = Math.ceil(jsonString.total / pageSize);
            for (var i = 0; i < jsonString.list.length; i++) {
                jsonString.list[i].pic_url = jsonString.list[i].pic_url.replace("{0}", "normal");
            }

            ApplyTemplate(jsonString);
            //$("#CurrentPages").text(page + "/" + lastPage);
        } else {
            alert(jsonString.msg);
        }
    }, true, { "ref_loading_c": $('#loading_list'), "ref_loading_text_c": '<div style="text-align:center; background:url(../images/loading.gif) no-repeat center center; height:80px;"></div>' });
}
//#endregion

//#region 给模板赋值
function ApplyTemplate(jsonString) {
    $('#productlistContent').setTemplate($('#jTemplate').html());
    $('#productlistContent').processTemplate(jsonString, null, { append: true });
    $("#productlistContent").listview("refresh");
    UpdatePaging();
}
//#endregion

//#region 显示隐藏上一页下一页
function DisplayProgressIndication() {
    if ($("#productlistContent li").length < 1) {
        //$('.paging').hide();
        //$('.paging').unbind();
        $(".page").hide();
    }
}
//#endregion

//#region 更新分页数据
function UpdatePaging() {
    //if (currentPage != 1) {
        //$('#PrevPage').attr('href', '#');
        //$('#PrevPage').unbind("click").click(PrevPage);
    //} else {
        //$('#PrevPage').removeAttr('href', '#');
        //$('#PrevPage').unbind("click");
    //}
    if (currentPage != lastPage) {
        $("#morePage").unbind("click").click(NextPage); ;
        //$('#NextPage').attr('href', '#');
        //$('#NextPage').unbind("click").click(NextPage);
    } else {
        $("#morePage").unbind("click");
        //$('#NextPage').removeAttr('href', '#');
        //$('#NextPage').unbind("click");
    }
}
//#endregion

//#region 更改地址栏页数
function Change_Location_curret(cp_currentPage) {
    if (urls_ca.indexOf('?') > 0) {
        if (urls_ca.indexOf('&page=') > 0) {
            urls_ca = urls_ca.replace(urls_ca.split('&')[1], "");
            urls_ca = urls_ca + "page=" + cp_currentPage;
        } else {
            urls_ca = urls_ca + "&page=" + cp_currentPage;
        }
    }
    $.cookie("currentPage", urls_ca.split('&')[1].replace("page=", ""));
}
//#endregion

//#region 下一页
function NextPage(evt) {
    evt.preventDefault();
    DisplayProgressIndication();
    currentPage = parseInt(currentPage) + parseInt(1);

    //Change_Location_curret(currentPage);
    //DisplayUI(currentPage, urls_ca);
    DisplayUI(currentPage, $.cookie("sorts"));
}
//#endregion

//#region 上一页
function PrevPage(evt) {
    evt.preventDefault();
    DisplayProgressIndication();
    currentPage = parseInt(currentPage) - parseInt(1);

    //Change_Location_curret(currentPage);
    //DisplayUI(currentPage, urls_ca);
    DisplayUI(currentPage, $.cookie("sorts"));
}
//#endregion

//#region 显示商品列表
function GoodProductList() {
    //Change_Location_curret(currentPage);
    //DisplayUI(currentPage, urls_ca);
    DisplayUI(currentPage, $.cookie("sorts")); //显示第一页
};
//#endregion

//#region 显示用户中心
function myAccount_Fun() {
    var token = $.cookie("m_token");
    var uid = $.cookie("m_uid");
    GetWcf({
        _api: "Member.GetMember.Info"
    }, function (jsonString) {
        if (jsonString.status == 1 && typeof (jsonString.info) == "object") {
            $("#myAccount_content").setTemplate($("#myAccount_Template").html());
            $("#myAccount_content").processTemplate(jsonString.info, null, { append: false });
            $("#myAccount_content").listview("refresh");
        } else {
            alert("msg:" + jsonString.msg);
        }
    }, true);
}
//#endregion

//#region 最小值  
Array.prototype.min = function () {
    var min = this[0];
    var len = this.length;
    for (var i = 1; i < len; i++) {
        if (this[i] < min) {
            min = this[i];
        }
    }
    return min;
}
//#endregion

//#region 最大值  
Array.prototype.max = function () {
    var max = this[0];
    var len = this.length;
    for (var i = 1; i < len; i++) {
        if (this[i] > max) {
            max = this[i];
        }
    }
    return max;
}
//#endregion

//#region 计算字个数
var Font_Num_Fun = function (element) {
    var De_array = new Array();
    for (var i = 0; i < element.attrs.length; i++) {
        //alert(element.attrs[i].key + ":" + element.attrs[i].key.length);
        De_array.push(element.attrs[i].key.length);
    }
    return De_array.max();
}
//#endregion

//#region 字体宽度
var Font_Width_Fun = function (element) {
    var _fontNum = Font_Num_Fun(element);
    //alert(parseInt(_fontNum) * parseInt(10) + "px");
    $(".ui-font-width").css("width", parseInt(_fontNum) * parseInt(14) + "px");
}
//#endregion

//#region 给商品菜单加click事件
function De_controlgroup_Fun() {
    $.cookie("tabs", "intro");
    Change_DateIcon_Diao_Fun($.cookie("tabs"));
    $("#intro_a").click(function () {
        $("#intro").css("display", "block");
        $("#detail").css("display", "none");
        $.cookie("tabs", "intro");
        Change_DateIcon_Diao_Fun($.cookie("tabs"));
    });
    $("#detail_a").click(function () {
        $("#intro").css("display", "none");
        $("#detail").css("display", "block");
        $.cookie("tabs", "detail");
        Change_DateIcon_Diao_Fun($.cookie("tabs"));
    });
}
//#endregion

//#region 商品详细页
function productDetail_Fun() {
    $("#intro").css("display", "block");
    $("#detail").css("display", "none");

    var token = $.cookie("m_token");
    var uid = $.cookie("m_uid");
    var gid = getParameter('gid');

    //#region 商品详细信息
    GetWcf({
        _api: "Goods.GetProductDetail.Info",
        _url: gid
    }, function (jsonString) {
        if (jsonString.status == 1 && typeof (jsonString.info) == "object") {
            $("#AddShop_btn").buttonMarkup({ theme: "a" }).button('enable');
            if (jsonString.info.stock == 0) {
                $("#AddShop_btn").buttonMarkup({ theme: "d" }).button('disable');
            }
            $("#h2_title").text(jsonString.info.title);
            $("#P_desc").html(jsonString.info.desc.toString());
            $('#iteminfo').setTemplate($('#productDetailContent').html());
            $('#iteminfo').processTemplate(jsonString.info, null, { append: false });
            $('#P_attrs').setTemplate($('#P_attrs_Template').html());
            $('#P_attrs').processTemplate(jsonString.info, null, { append: false });
            Font_Width_Fun(jsonString.info);
        } else {
            alert(jsonString.msg);
        }
    }, false);
    //#endregion

    //#region  图片列表
    GetWcf({
        _api: "Goods.goodspic.Info",
        _url: "/" + gid
    }, function (jsonString) {
        if (jsonString.status == 1 && typeof (jsonString.list) == "object" && jsonString.list != null) {
            for (var i = 0; i < jsonString.list.length; i++) {
                jsonString.list[i].url = jsonString.list[i].url.toString().replace("{0}", "org");
                //alert(jsonString.list[i].url);
            }

            $('#imggallery_iteminfo').setTemplate($('#picList').html());
            $('#imggallery_iteminfo').processTemplate(jsonString, null, { append: false });

            ImgSwipegall();
        } else {
            //alert(jsonString.msg);
            $('#imggallery_iteminfo').setTemplate('<li><img src="/images/errorImg_big.jpg" width="300" height="300" /></li>');
            $('#imggallery_iteminfo').processTemplate(jsonString, null, { append: false });
        }
    }, true, {
        "ref_loading": $('#imggallery_iteminfo'), "ref_loading_text": '<li style="text-align:center; background:url(../images/loading.gif) no-repeat center center; height:300px;"></li>'
    });
    //#endregion

    De_controlgroup_Fun();

    //#region 添加购物车
    $("#AddShop_btn").click(function () {
        var area_id = 0;
        var product_id = gid;
        var num = 1;
        GetWcf({
            _api: "Order.add_goodstoshoppingcar",
            _url: "/" + area_id + "/" + product_id + "/" + num
        }, function (jsonString) {
            if (jsonString.status == 1) {
                window.location.href = window.WebRoot + "CheckOut/shoppingcart.aspx";
            } else {
                alert(jsonString.msg);
            }
        }, true);
    });
    //#endregion
}
//#endregion

$(document).ready(function () {
    var url = window.location.pathname;
    //alert(/\//i.test(url));
    if (/\//i.test(url) && url.length == 1) {
        Index_Fun();

    } else {
        if (/index.aspx/i.test(url)) {
            Index_Fun();
        }
        else if (/login.aspx/i.test(url)) {
            Login_Fun();
        } else if (/registration.aspx/i.test(url)) {
            Register_Fun();
        } else if (/userpolicy.aspx/i.test(url)) {
            //userpolicy_Fun();
        } else if (/productlist.aspx/i.test(url)) {
            GoodProductList();
            Salec_Price_newTime_Fun();
        } else if (/myaccount.aspx/i.test(url)) {
            myAccount_Fun();
            LoingOut();
        } else if (/productdetailinfo.aspx/i.test(url)) {
            productDetail_Fun();
        } else if (/forgetpassword.aspx/i.test(url)) {
            Forgetpassword();
        }
    }

    $('#gotop').tap(function () {
        $.mobile.silentScroll(10);
    });
});

