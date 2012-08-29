﻿//#region 退出登录
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
    console.log("sorts2:" + sorts);
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

//#region 信息
var intervalID_mesage = 0;
function setMesage(mesag, product_id) {
    hideMesage();
    $("#error_" + product_id).text(mesag);
    intervalID_mesage = setTimeout(function () { intervalID_mesage = 0; $("#error_" + product_id).text(""); }, 3000);
}
function hideMesage() {
    if (intervalID_mesage != 0) {
        clearTimeout(intervalID_mesage);
        intervalID_mesage = 0;
    }
}
//#endregion

//#region 购物车

function shoppingcart_Fun() {
    //jQuery.mobile.changePage(window.WebRoot + "CheckOut/shoppingcart.aspx");
    //alert($("#Good_Total_Count").text());
    //alert(goodsTotal);
    var goodsTotal = $("#Hid_Good_Total_price").val() || 0;
    $("#goodsTotal").html(parseFloat(goodsTotal).toFixed(2) + "元");

    //#region 绑定购物车列表
    var BindDate_Shopcart = function () {
        var shoppingCart_list = $('#shoppingCart_list');
        GetWcf({
            _api: "Order.get_shoppingcartgoods_list"
        }, function (jsonString) {
            if (jsonString.status == 1 && typeof (jsonString.info) == "object" && jsonString.info.shoppingcart_list != null && jsonString.info.shoppingcart_list.length > 0) {
                for (var i = 0; i < jsonString.info.shoppingcart_list.length; i++) {
                    jsonString.info.shoppingcart_list[i].vchPicURL = jsonString.info.shoppingcart_list[i].vchPicURL.toString().replace("{0}", "normal");
                }
                $("#good_totals").html($("#Good_Total_Count").text());
                var goodsTotal = $("#Hid_Good_Total_price").val() || 0;

                $("#goodsTotal").html(parseFloat(goodsTotal).toFixed(2) + "元");

                shoppingCart_list.setTemplate($('#shoppingCart_list_template').html());
                shoppingCart_list.processTemplate(jsonString.info.shoppingcart_list, null, { append: false });
                shoppingCart_list.trigger('create').listview("refresh");
            } else {
                $("#good_totals").html("0");
                $("#goodsTotal").html("0.00元");
                shoppingCart_list.setTemplate("<li style='text-align:center;'>暂无商品信息！</li>");
                shoppingCart_list.processTemplate(jsonString.info.shoppingcart_list, null, { append: false });
                shoppingCart_list.listview("refresh");
            }
        }, true, {
            "ref_loading": shoppingCart_list, "ref_loading_text": '<li style="text-align:center; background:url(../images/loading.gif) no-repeat center center; height:80px;"></li>'
        });
    };
    BindDate_Shopcart();
    //#endregion

    //#region 设置购物车数商品量
    var Set_shoppingcartgoodsnum = function (shoppingcarid, gid, num, onum, obj) {
        GetWcf({
            _api: "Order.set_shoppingcartgoodsnum",
            _url: "/" + shoppingcarid + "/" + gid + "/" + num
        }, function (jsonString) {
            if (jsonString.status == 1) {
                $("#error_" + product_id).text("");
                Get_shoppingcartgoodsnum_Fun();
                $("#good_totals").html($("#Good_Total_Count").text());
                var goodsTotal = $("#Hid_Good_Total_price").val() || 0;
                $("#goodsTotal").html(parseFloat(goodsTotal).toFixed(2) + "元");
                if (onum != num) {
                    $(obj).attr("onum", num);
                }

            } else {
                setMesage(jsonString.msg, product_id);
                //$("#error_" + product_id).text(jsonString.msg);
                //alert(onum);
                $(obj).val(onum);
                obj.val(onum).slider("refresh");
                //$("#error_" + product_id).text("");
            }
        }, true);
    }
    //#endregion

    //#region 改变值调用方式
    var product_id = 0; //商品ID
    var p_num = 0; //当前的数量
    var onum = 0; //原来的数量
    var ShopCartID = 0; //购物车ID
    var Change_Project_object = function (obj, product_id, p_num, ShopCartID) {
        //alert(onum+":"+p_num);
        if (onum != p_num) {
            //$(obj).attr("onum", p_num);
            Set_shoppingcartgoodsnum(ShopCartID, product_id, p_num, onum, obj);
        }
    }
    //#endregion

    //#region 改变数量框里的值
    var changeNum = function () {
        $("#shoppingCart_list div[role=application] a").live("touchend mouseup", function () {
            var Find_Number_object = $(this).parent().parent().find("input[type=number]");
            ShopCartID = Find_Number_object.attr("ShopCartID")
            product_id = Find_Number_object.attr("product_id");
            onum = Find_Number_object.attr("onum") || 0;
            p_num = Find_Number_object.val();
            //Log("slider商品ID：" + product_id + "现数量:" + p_num + "原数量:" + onum);
            Change_Project_object(Find_Number_object, product_id, p_num, ShopCartID);
        });

        $("#shoppingCart_list input[type=number]").live("blur", function () {
            ShopCartID = $(this).attr("ShopCartID");
            product_id = $(this).attr("product_id");
            onum = $(this).attr("onum") || 0;
            p_num = $(this).val();

            //Log("input商品ID：" + product_id + "现数量:" + p_num + "原数量:" + onum);
            Change_Project_object($(this), product_id, p_num, ShopCartID);

        });
    } ();
    //#endregion

    //#region 删除购物车
    var del_ShoppingCart = function () {
        $("#shoppingCart_list a.Del_id").live("click", function () {
            var intShopCartID = $(this).attr("delete_id");
            product_id = $(this).attr("product_id");
            GetWcf({
                _api: "Order.del_shoppingcart",
                _url: "/" + intShopCartID
            }, function (jsonString) {
                if (jsonString.status == 1) {
                    Get_shoppingcartgoodsnum_Fun();
                    BindDate_Shopcart();
                } else {
                    $("#error_" + product_id).text(jsonString.msg);
                }
            }, true);
        });
    } ();
    //#endregion

    //#region 提交订单
    $("#ShoppingCart_btn").click(function (e) {
        if (parseInt($("#good_totals").text()) > 0) {
            $('html, body').animate({
                scrollTop: $(document).height()
            },
        1500);

            window.location.href = window.WebRoot + "CheckOut/orderconfirm.aspx";
        } else {
            $("#tis_Tip").append('<li class="error-text">您还没有购买商品！</li>');
        }
    });
    //#endregion
}
//#endregion

//#region 默认收货人信息存储
var De_Con_Object = "";
var Default_Consignee_information_Fun = function (De_object_Info) {
    De_Con_Object = De_object_Info;
    LS.set("county_id", De_object_Info.county_id); //区域ID
    LS.set("current_addressid", De_object_Info.id); //收货人信息ID
}
//#endregion

//#region  支付方式信息存储
var Payment_info_Object = function (payment_text_name, payment_Id) {
    LS.set("payment_text_name", payment_text_name); //付款名称 payment_text_name  如在线支付
    LS.set("payment_Id", payment_Id);
}
//#endregion

//#region  删除支付方式信息存储
var Delete_Payment_info_Object = function (payment_text_name, payment_Id) {
    LS.remove(payment_text_name); //付款名称 payment_text_name  如在线支付
    LS.remove(payment_Id);
}
//#endregion

//#region  配送方式信息存储
var Delivery_info_Object = function (delivery_array) {
    for (var i = 0; i < delivery_array.slides.length; i++) {
        //alert("aa:" + delivery_array.slides[i] + delivery_array.values[i]);
        LS.set(delivery_array.slides[i], delivery_array.values[i]);
    }
}
//#endregion

//#region  配送方式信息删除
var Delelt_Delivery_info_Object = function (delivery_array) {
    for (var i = 0; i < delivery_array.slides.length; i++) {
        LS.remove(delivery_array.slides[i]);
    }
}
//#endregion

//#region 计算总金额
var Final_Price = function () {
    var goodsTotal = $("#Hid_Good_Total_price").val() || 0;
    $("#Total_Price").text(parseFloat(goodsTotal).toFixed(2));
    var _Final_price = parseFloat(goodsTotal) + parseFloat($("#Y_Price").text());
    $("#Final_Price").text(parseFloat(_Final_price).toFixed(2) + "元");
}
//#endregion

//#region 订单实体类
var orderEntity = {
    createNew: function () {
        var order = {};
        order.buyer_uid = $.cookie("m_uid"); //买家字符串ID
        order.addressid = LS.get("current_addressid") || "no_c"; //收货地址ID
        order.payid = LS.get("payment_Id") || "no_c"; //支付方式id
        order.logisticsid = LS.get("delivery_Id") || "no_c"; //配送方式id

        var isorInvice = LS.get("isorInvice") || "0"; //发票抬头类型
        var invoice_Type = LS.get("invoice_Type"); //发票分类
        var FaTHeader = LS.get("FaTHeader"); //发票抬头
        var remark = $("#remark").val(); //订单备注
        var posttimetype = LS.get("delivery_sh_time_id"); //配送时间类型
        //alert(typeof (posttimetype));
        //alert(typeof(isorInvice) + ":" + typeof(invoice_Type) + ":" + typeof(FaTHeader) + ":" + typeof(remark));
        //alert(isorInvice + ":" + invoice_Type + ":" + FaTHeader + ":" + remark);


        order.titletype = isorInvice;

        if (invoice_Type != null) {
            order.invoicecategory = invoice_Type;
        }
        if (FaTHeader != null) {
            order.invoicetitle = FaTHeader;
        }
        if (remark != "") {
            order.remark = remark;
        }
        if (posttimetype != "null") {
            order.posttimetype = posttimetype;
        }

        return order;
    },
    init_Judge: function (orderEntity_c) {
        if (orderEntity_c.addressid === "no_c") {
            $("#Y_Price").text("0");
            return false;
        } else if (orderEntity_c.payid === "no_c") {
            $("#Y_Price").text("0");
            return false;
        } else if (orderEntity_c.logisticsid === "no_c") {
            $("#Y_Price").text("0");
            return false;
        } else {
            return true;
        }
    }
}
//#endregion

//#region 计算运费
var calculation_shipping_costs_Fun = function () {
    var orderEntity_c = orderEntity.createNew();
    //alert(orderEntity.init_Judge(orderEntity_c));
    if (orderEntity.init_Judge(orderEntity_c)) {
        PostWcf({
            _api: "Order.get_temporder_info",
            _data: JSON.stringify(orderEntity_c)
        }, function (json) {
            $("#Y_Price").text(parseFloat(json.info.total_freight).toFixed(2));
            $("#Total_Price").text(parseFloat(json.info.total_goods_fee).toFixed(2));
            $("#Final_Price").text(parseFloat(json.info.total_order_fee).toFixed(2) + "元");
            //Final_Price();
        }, true);
    }
}
//#endregion

//#region 去结算页面
function orderconfirm_Fun() {
    //#region  收货人信息显示
    GetWcf({
        _api: "Member.get_defaultaddress_info"
    }, function (jsonString) {
        var address_default = $('#address_default');
        var address_message = $('#address_message');
        if (jsonString.status == 1 && typeof (jsonString.info) == "object" && jsonString.info.contact_name != null) {
            Default_Consignee_information_Fun(jsonString.info);
            address_default.removeAttr("href").attr("href", window.WebRoot + "CheckOut/addresslist.aspx");
            address_default.setTemplate($('#address_default_template').html());
            address_default.processTemplate(jsonString.info, null, { append: false });
            address_message.listview('refresh');
        } else {
            address_default.removeAttr("href").attr("href", window.WebRoot + "CheckOut/address_add.aspx");
            address_default.setTemplate("<p>暂无收货信息，请添加！</p>");
            address_default.processTemplate(jsonString.info, null, { append: false });
            address_message.listview('refresh');
        }

        var paymentlist_p = $("#paymentlist_p");
        var delivery_text_name_p = $("#delivery_text_name_p");
        var delivery_sh_time_text_p = $("#delivery_sh_time_text_p");
        var delivery_fk_time_text_p = $("#delivery_fk_time_text_p");
        var regionid = LS.get("county_id");
        var paygroupid = LS.get("payment_Id");

        //#region 选支付方式时判断
        $("#paymentlist_a").click(function () {
            if (regionid === null) {
                alert("请选择收货人信息！");
                return;
            } else {
                window.location.href = window.WebRoot + "CheckOut/paymentlist.aspx";
            }
        });
        //#endregion

        //#region  支付方式显示
        if (LS.get("payment_text_name") == null) {
            paymentlist_p.html("请选择支付方式");
        } else {
            paymentlist_p.html(LS.get("payment_text_name"));
        }
        //#endregion

        //#region 配送时判断
        $("#deliverylist_a").click(function () {
            if (paygroupid === null) {
                alert("请选择支付方式！");
                return;
            } else {
                window.location.href = window.WebRoot + "CheckOut/deliverylist.aspx";
            }
        });
        //#endregion

        //#region  配送方式显示
        if (LS.get("delivery_text_name") == null) {
            delivery_text_name_p.html("请选择配送方式");
        } else {
            $("#delivery_text_name_p").html(LS.get("delivery_text_name"));
            //alert(LS.get("delivery_fk_time_text") +":"+ (LS.get("delivery_fk_time_text") === "null"));
            if (LS.get("delivery_sh_time_text") === "null") {
                delivery_sh_time_text_p.html("");
            } else {
                delivery_sh_time_text_p.html("送货日期：" + LS.get("delivery_sh_time_text"));
            }
            if (LS.get("delivery_fk_time_text") != null) {
                delivery_fk_time_text_p.html("付款方式：" + LS.get("delivery_fk_time_text"));
            } else {
                delivery_fk_time_text_p.html("");
            }
        }
        //#endregion
    }, true);
    //#endregion

    //#region  发票信息显示
    //alert(typeof (LS.get("isorInvice")) + LS.get("isorInvice") + ":" + typeof (LS.get("FaTHeader")) + LS.get("FaTHeader"));
    if (LS.get("isorInvice") == null || LS.get("isorInvice") == "0") {
        $("#invoice_Type_Text").html("不需要发票");
    } else {
        $("#invoice_Type_Text").html("发票类型：" + LS.get("invoice_Type_Text"));
    }
    if (LS.get("FaTHeader") != null) {
        if (LS.get("FaTHeader") != "-1") {
            $("#invoice_Theader_Text").html("发票抬头：" + LS.get("FaTHeader"));
        }
    }
    //#endregion

    Final_Price();

    //#region 提交订单

    $("#orderConfirm_btn").click(function () {
        var orderEntity_c = orderEntity.createNew();
        if (orderEntity.init_Judge(orderEntity_c)) {
            PostWcf({
                _api: "Order.add_order_info",
                _data: JSON.stringify(orderEntity_c)
            }, function (json) {
                if (json.status === 1 && typeof (json.info) == "object" && json.info != null) {
                    LS.clear(); //先清再存
                    var delivery_array = {
                        slides: [
                                "make_ocode", //订单号
                                "make_total_order", //应付金额
                                "make_paytype", //支付方式
                                "make_logisticstype", //配送方式
                                "make_posttimetype" //送货时间
                            ],
                        values: [
                                json.info.ocode,
                                json.info.total_order,
                                json.info.paytype,
                                json.info.logisticstype,
                                json.info.posttimetype
                            ]
                    };
                    Delivery_info_Object(delivery_array);
                    window.location.href = window.WebRoot + "CheckOut/makeorder.aspx";
                } else if (json.status == "-2") {
                    alert(json.msg);
                    window.location.href = window.WebRoot + "CheckOut/shoppingcart.aspx";
                } else {
                    alert(json.msg);
                }

            }, true);
        }
        else {
            alert("信息还不完全！");
        }
    });

    //#endregion
}
//#endregion

//#region 收货人列表信息页面
function addresslist_Fun() {

    GetWcf({
        _api: "Member.get_address_list"
    }, function (jsonString) {
        var addresslist_cont = $('#addresslist_cont');
        if (jsonString.status == 1 && typeof (jsonString.list) == "object" && jsonString.list != null && jsonString.list.length > 0) {
            var selected_addressId = LS.get("current_addressid");
            for (var i = 0; i < jsonString.list.length; i++) {
                //alert((jsonString.list[i].id.toString() === selected_addressId.toString()) + ":" + jsonString.list[i].id + ":" + selected_addressId);
                if (jsonString.list[i].id.toString() === selected_addressId.toString()) {
                    jsonString.list[i].get_def = true;
                } else {
                    jsonString.list[i].get_def = false;
                }
            }
            addresslist_cont.setTemplate($('#addresslist_cont_template').html());
            addresslist_cont.processTemplate(jsonString.list, null, { append: false });
            addresslist_cont.trigger('create');
        } else {
            //alert(jsonString.msg);
            addresslist_cont.setTemplate("<p>暂无收货信息！</p>");
            addresslist_cont.processTemplate(jsonString.list, null, { append: false });
            addresslist_cont.trigger('create');
        }

    }, true, {
        "ref_loading": $('#addresslist_cont'), "ref_loading_text": '<div style="text-align:center; background:#ffffff url(../images/loading.gif) no-repeat center center; height:60px;"></div>'
    });

    //#region 点击给radio赋值
    $("[name = radio_choice_add]:radio").live('change', function () {
        $("[name = radio_choice_add]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
        //alert($("[name = radio_choice_add]:radio:checked").attr("value"));

    });
    //#endregion

    //#region 设置默认收货地址
    $("#addresslist_btn").live("click", function () {
        $("#frmAddress_list").validate({
            errorElement: "span",
            rules: {
                radio_choice_add: {
                    required: true
                }
            },
            messages: {
                radio_choice_add: {
                    required: "请选择收货地址"
                }
            },
            submitHandler: function (form) {
                //alert($("[name = radio_choice_add]:radio:checked").attr("value"));
                var address_id = $("[name = radio_choice_add]:radio:checked").attr("value") || 0;
                //alert(address_id);
                $("#Add_mesage").css("display", "none").text("");
                if (address_id == 0) {
                    $("#Add_mesage").css("display", "block").text("请选择收货地址");
                    return;
                }
                GetWcf({
                    _api: "Member.set_defaultaddress",
                    _url: "/" + address_id
                }, function (jsonString) {
                    if (jsonString.status == 1) {
                        Delete_Payment_info_Object("payment_text_name", "payment_Id");
                        var delivery_array = {
                            slides: [
                                "delivery_text_name", //配送方式名称
                                "delivery_Id", //配送方式ID
                                "delivery_sh_time_text", //送货时间的名称
                                "delivery_sh_time_id", //送货时间的值
                                "delivery_fk_text", //付款类型名称  如现金
                                "delivery_fk_id"//付款类型值
                            ]
                        };
                        Delelt_Delivery_info_Object(delivery_array);
                        LS.set("current_addressid", address_id); //设置默认收货地址
                        window.location.href = window.WebRoot + "CheckOut/orderconfirm.aspx";
                    } else {
                        alert(jsonString.msg);
                    }
                }, true);
            }
        });
    });
    //#endregion

}
//#endregion

//#region 支付方式
function paymentlist_Fun() {
    //#region 绑定支付方式信息
    var regionid = LS.get("county_id");
    if (regionid == null) {
        $('#field_pay_list').setTemplate("<p>暂无支付方式信息！</p>");
        $('#field_pay_list').processTemplate(jsonString.list, null, { append: false });
        $('#field_pay_list').trigger('create');
    } else {
        GetWcf({
            _api: "Order.get_payment_list",
            _url: "/" + regionid
        }, function (jsonString) {
            if (jsonString.status == 1 && typeof (jsonString.list) == "object" && jsonString.list.length > 0) {
                $('#field_pay_list').setTemplate($('#jTemplate').html());
                $('#field_pay_list').processTemplate(jsonString.list, null, { append: false });
                $('#field_pay_list').trigger('create');
                $("[name = radio-pay-1]:radio").attr("checked", false).checkboxradio("refresh");
                $("[name = radio-pay-1]:radio:first").attr("checked", true).checkboxradio("refresh");
            } else {
                $('#field_pay_list').setTemplate("<p>暂无支付方式信息！</p>");
                $('#field_pay_list').processTemplate(jsonString.list, null, { append: false });
                $('#field_pay_list').trigger('create');
            }
        }, true);
    }
    //#endregion

    //#region 点击给radio赋值
    $("[name = radio-pay-1]:radio").live('change', function () {
        $("[name = radio-pay-1]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
    });
    //#endregion

    //#region 提交支付方式
    $("#paymentlist_sub_btn").live('click', function () {
        Payment_info_Object($("[name = radio-pay-1]:radio:checked").parent().find("label").attr("text"), $("[name = radio-pay-1]:radio:checked").attr("value"));
        var delivery_array = {
            slides: [
                "delivery_text_name", //配送方式名称
                "delivery_Id", //配送方式ID
                "delivery_sh_time_text", //送货时间的名称
                "delivery_sh_time_id", //送货时间的值
                "delivery_fk_text", //付款类型名称  如现金
                "delivery_fk_id"//付款类型值
                    ]
        };
        Delelt_Delivery_info_Object(delivery_array);
        window.location.href = window.WebRoot + "CheckOut/orderconfirm.aspx";
    });
    //#endregion
}
//#endregion

//#region 配送方式
function deliverylist_Fun() {
    //#region  控制送货时间
    var sh_type_radio_checked = function () {
        var sh_type_radio_value = $("[name = radio-type-1]:radio:checked").attr("value");
        if (sh_type_radio_value === "1") {
            $("#sh_time").css("display", "block");
            $("[name = radio-date-1]:radio").attr("checked", false).checkboxradio("refresh");
            $("[name = radio-date-1]:radio:first").attr("checked", true).checkboxradio("refresh");
        } else {
            $("#sh_time").css("display", "none");
            $("[name = radio-date-1]:radio").attr("checked", false).checkboxradio("refresh");
        }
    };
    //#endregion

    //#region  控制货到付款就有付款方式
    var payment_Id = LS.get("payment_Id");
    if (payment_Id == "0") {
        $("#fk_type").css("display", "block");
        $("[name = radio-pay-1]:radio").attr("checked", false).checkboxradio("refresh");
        $("[name = radio-pay-1]:radio:first").attr("checked", true).checkboxradio("refresh");
    } else {
        $("#fk_type").css("display", "none");
        $("[name = radio-pay-1]:radio").attr("checked", false).checkboxradio("refresh");
    }
    //#endregion

    //#region 绑定配送方式信息
    var regionid = LS.get("county_id");
    var paygroupid = LS.get("payment_Id");
    GetWcf({
        _api: "Order.get_logistics_list",
        _url: "/" + regionid + "/" + paygroupid
    }, function (jsonString) {
        if (jsonString.status == 1 && typeof (jsonString.list) == "object" && jsonString.list.length > 0) {
            $('#deliverylist_cont').setTemplate($('#deliverylist_cont_template').html());
            $('#deliverylist_cont').processTemplate(jsonString.list, null, { append: false });
            $('#deliverylist_cont').trigger('create');
            //没有任何默认值时指定
            $("[name = radio-type-1]:radio").attr("checked", false).checkboxradio("refresh");
            $("[name = radio-type-1]:radio:first").attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
            sh_type_radio_checked();
        } else {
            $('#deliverylist_cont').setTemplate("<p>暂无配送方式信息！</p>");
            $('#deliverylist_cont').processTemplate(jsonString.list, null, { append: false });
            $('#deliverylist_cont').trigger('create');
        }

    }, true);
    //#endregion

    //#region 点击给radio赋值
    $("[name = radio-type-1]:radio").live('change', function () {
        $("[name = radio-type-1]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值

        sh_type_radio_checked();
    });
    $("[name = radio-date-1]:radio").live('change', function () {
        $("[name = radio-date-1]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
    });
    $("[name = radio-pay-1]:radio").live('change', function () {
        $("[name = radio-pay-1]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
    });
    //#endregion

    //#region 提交支付方式
    $("#deliverylist_sub_btn").live('click', function () {
        var delivery_array = {
            slides: [
                "delivery_text_name", //配送方式名称
                "delivery_Id", //配送方式ID
                "delivery_sh_time_text", //送货时间的名称
                "delivery_sh_time_id", //送货时间的值
                "delivery_fk_text", //付款类型名称  如现金
                "delivery_fk_id"//付款类型值
                    ],
            values: [
                $("[name = radio-type-1]:radio:checked").parent().find("span.ui-btn-text").text() || "null",
                $("[name = radio-type-1]:radio:checked").attr("value") || "null",
                $("[name = radio-date-1]:radio:checked").parent().find("span.ui-btn-text").text() || "null",
                $("[name = radio-date-1]:radio:checked").attr("value") || "null",
                $("[name = radio-pay-1]:radio:checked").parent().find("span.ui-btn-text").text() || "null",
                $("[name = radio-pay-1]:radio:checked").attr("value") || "null"
            ]
        };
        Delivery_info_Object(delivery_array);
        window.location.href = window.WebRoot + "CheckOut/orderconfirm.aspx";
    });
    //#endregion
}
//#endregion

//#region 发票信息
function invoice_Fun() {
    var FaTtype_Status = function () {
        $("[name = FaTtype]:radio").attr("checked", false).checkboxradio("refresh");
        $("[name = FaTtype]:radio:first").attr("checked", true).checkboxradio("refresh");
    }

    //#region 给默认赋值
    var invoice_default_status = function () {
        $("[name = IsNoInvoice]:radio").attr("checked", false).checkboxradio("refresh");
        $("[name = IsNoInvoice]:radio:first").attr("checked", true).checkboxradio("refresh");

        $("#FaTHeader,#invoice_Type").css("display", "none");
        $("[name = FaTtype]:radio").attr("checked", false).checkboxradio("refresh");
        $("#FaTHeader").val("");
        $("#Cah_Mesag").css("display", "none").text("");
    };
    //#endregion

    //#region 否
    invoice_default_status();
    //#endregion

    //#region 个人
    var invoice_Personal_Status = function () {
        $("#FaTHeader").css("display", "none").val("");
        $("#Cah_Mesag").css("display", "none").text("");
        FaTtype_Status();
        $("#invoice_Type").css("display", "block");
    }
    //#endregion

    //#region 公司
    var invoice_company_Status = function () {
        FaTtype_Status();
        $("#FaTHeader,#invoice_Type").css("display", "block");
    }
    //#endregion

    $("[name = IsNoInvoice]:radio").click(function () {
        $("[name = IsNoInvoice]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");
        var current_Status = $(this).attr("value");
        if (current_Status === "0") {
            invoice_default_status();
        } else if (current_Status === "1") {
            invoice_Personal_Status();
        } else if (current_Status === "2") {
            invoice_company_Status();
        }
    });

    //#region  提交发票信息
    $("#invoice_btn").click(function () {
        var isorInvice, FaTHeader, invoice_Type; //是否要发票、发票抬头、发票类型
        isorInvice = $("[name = IsNoInvoice]:radio:checked").attr("value") || "-1";
        FaTHeader = $("#FaTHeader").val() || "-1";
        invoice_Type = $("[name = FaTtype]:radio:checked").attr("value") || "-1";
        var di_no = $("#FaTHeader").attr("style").toString();
        if (di_no.indexOf("block") != "-1") {
            if ($("#FaTHeader").val() == "") {
                $("#Cah_Mesag").css("display", "block").text("请填写发票抬头");
                return;
            } else {
                $("#Cah_Mesag").css("display", "none").text("");
            }
        }
        //alert($("[name = FaTtype]:radio:checked").text());
        var delivery_array = {
            slides: [
                "isorInvice", //是否要发票
                "FaTHeader", //发票抬头
                "invoice_Type", //发票类型
                "invoice_Type_Text"
                    ],
            values: [
                isorInvice,
                FaTHeader,
                invoice_Type,
                $("[name = FaTtype]:radio:checked").parent().find("span.ui-btn-text").text()
            ]
        };
        Delivery_info_Object(delivery_array); //发票信息存储
        window.location.href = window.WebRoot + "CheckOut/orderconfirm.aspx";
    });
    //#endregion
}
//#endregion

//#region 获取省市区数据
var proviceA = "", cityA = "", townA = "";
function GetPrCiTownAjaxDate() {
    GetWcf_F({
        _api: "Order.get_allregion_list"
    }, function (jsonString) {
        if (jsonString.status == 1 && typeof (jsonString.info) == "object" && jsonString.info.length > 0) {
            proviceA = jsonString.info[0];
            cityA = jsonString.info[1];
            townA = jsonString.info[2];
        } else {
            alert(jsonString.msg);
        }
    }, true, true);
}
//#endregion

//#region 获取省
function ProViceCityTown_Function() {
    var s1P = [];
    var s1 = jQuery("#PCR select#s1s");
    s1.empty();
    for (var i = 0, len = proviceA.length; i < len; i++) {
        s1P[i] = "<option value='" + proviceA[i].id + "'>" + proviceA[i].name + "</option>";
    }
    $(s1P.join('')).appendTo(s1);
    var myselect = $("#PCR select#s1s");
    myselect[0].selectedIndex = 0;
    myselect.selectmenu("refresh");
    //jQuery("#PCR select#s2s").empty();
    //jQuery("#PCR select#s3s").empty();
    getCity();
}
//#endregion

//#region 获取市
function getCity() {
    var s1 = jQuery("#PCR select#s1s").val();
    var s2 = jQuery("#PCR select#s2s");
    var s2C = [];
    s2.empty();
    for (var i = 0, len = cityA.length; i < len; i++) {
        if (s1 == cityA[i].pid) {
            s2C[i] = "<option value='" + cityA[i].id + "'>" + cityA[i].name + "</option>";
        }
    }
    $(s2C.join('')).appendTo(s2);
    $("#PCR select#s2s").selectmenu('refresh');
    var myselect = $("#PCR select#s2s");
    myselect[0].selectedIndex = 0;
    myselect.selectmenu("refresh");
    getTown();
}
//#endregion

//#region 获取区
function getTown() {
    var s2 = jQuery("#PCR select#s2s").val();
    var s3 = jQuery("#PCR select#s3s");
    var s3C = [];
    s3.empty();
    for (var i = 0, len = townA.length; i < len; i++) {
        //if (s2 == townA[i].pid && townA[i].name != '市辖区') {
        if (s2 == townA[i].pid) {
            s3C[i] = "<option value='" + townA[i].id + "'>" + townA[i].name + "</option>";
        }
    }
    $(s3C.join('')).appendTo(s3);
    $("#PCR select#s3s").selectmenu('refresh');
    var myselect = $("#PCR select#s3s");
    myselect[0].selectedIndex = 0;
    myselect.selectmenu("refresh");
}
//#endregion

var a = 0;
//#region 给省市区绑定事件
function BindClick_Pro_City_Town_Event_Fun() {
    GetPrCiTownAjaxDate();
    ProViceCityTown_Function();
    $("#s1s").live("change", function () {
        getCity();
    });
    $("#s2s").live("change", function () {
        getTown();
    });
}
//#endregion

var address_add_update_Object = {};

//#region 点击给radio赋值
address_add_update_Object.addressRadioChange = function () {
    $("[name = radio-choice-1]:radio").live('change', function () {
        $("[name = radio-choice-1]:radio").attr("checked", false).checkboxradio("refresh");
        $(this).attr("checked", true).checkboxradio("refresh");   // 绑定事件及时更新checkbox的checked值
    });
};
//#endregion

//#region 验证
address_add_update_Object.address_valite_c = function (id_obj) {
    $("#frmAddress_Add").validate({
        errorElement: "span",
        rules: {
            email: {
                required: true
            },
            addr: {
                required: true
            },
            zipCode: {
                required: true,
                number: true,
                rangelength: [6, 6]
            },
            mobilePhone: {
                required: true,
                number: true,
                rangelength: [11, 11]
            }
        },

        messages: {
            email: {
                required: "请填写收货人姓名"
            },
            addr: {
                required: "请填写详细的的收货地址"
            },
            zipCode: {
                required: "请填写邮政编码",
                number: "请填写正确的邮政编码",
                rangelength: "邮政编码只能是{0}位"
            },
            mobilePhone: {
                required: "请填写手机号码",
                number: "请填写正确的手机号码",
                rangelength: "手机号码只能是{0}位"
            }
        },
        submitHandler: function (form) {
            var email = $("#email").val();
            var s1s = $("#s1s option:selected").val(); //省
            var s1s_Text = $("#s1s option:selected").text();
            var s2s = $("#s2s option:selected").val(); //市
            var s2s_Text = $("#s2s option:selected").text();
            var s3s = $("#s3s option:selected").val(); //区
            var s3s_Text = $("#s3s option:selected").text();
            var addr = $("#addr").val();
            var zipCode = $("#zipCode").val();
            var type_add = $("[name = radio-choice-1]:radio:checked").attr("value");
            var mobilePhone = $("#mobilePhone").val();
            //alert(s1s+s1s_Text+s2s+s2s_Text+s3s+s3s_Text+addr+zipCode+type_add+mobilePhone);

            var jobj = { "addr": addr, "city": s2s_Text, "city_id": s2s, "contact_name": email, "county": s3s_Text, "county_id": s3s, "id": id_obj, "mobile": mobilePhone, "phone": "-", "province": s1s_Text, "province_id": s1s, "type": type_add, "zip": zipCode };


            PostWcf({
                _api: "Member.set_address_info",
                _data: JSON.stringify(jobj)
            }, function (json) {
                if (json.status == 1) {
                    if (id_obj > 0) { LS.set("current_addressid", id_obj); } else {
                        LS.set("current_addressid", json.info);
                    }
                    window.location.href = window.WebRoot + "CheckOut/addresslist.aspx";
                } else
                    $("#Cah_Mesag").css("display", "block").text(json.msg);
            }, true);
        }
    });
}
//#endregion

//#region 添加收货人信息
function address_add_Fun() {
    BindClick_Pro_City_Town_Event_Fun();
    address_add_update_Object.addressRadioChange();
    address_add_update_Object.address_valite_c("0");
}
//#endregion

//#region 修改收货人信息
function address_edit_Fun() {
    BindClick_Pro_City_Town_Event_Fun();
    //alert(window.location.search);
    var address_id = window.location.search.toString().replace("?address_id=", "");
    //#region 绑定收货人详细信息
    GetWcf_F({
        _api: "Member.get_address_info",
        _url: "/" + address_id
    }, function (jsonString) {
        if (jsonString.status == 1 && typeof (jsonString.info) == "object" && jsonString.info.contact_name != null) {
            $("#acc_id").val(jsonString.info.id);
            $("#email").val(jsonString.info.contact_name);
            $("#s1s option[value=" + jsonString.info.province_id + "]").attr("selected", true); //省
            $("#s1s").selectmenu('refresh').trigger("change");
            $("#s2s option[value=" + jsonString.info.city_id + "]").attr("selected", true); //市
            $("#s2s").selectmenu('refresh').trigger("change");
            $("#s3s option[value=" + jsonString.info.county_id + "]").attr("selected", true); //区
            $("#s3s").selectmenu('refresh');

            //alert(jsonString.info.city_id);

            $("#addr").val(jsonString.info.addr);
            $("#zipCode").val(jsonString.info.zip);
            $("input[type='radio'][name=radio-choice-1][value=" + jsonString.info.type + "]").attr("checked", true).checkboxradio("refresh");
            $("#mobilePhone").val(jsonString.info.mobile);
        } else {
            $("#Cah_Mesag").text(jsonString.msg);
        }
    }, true, true);
    //#endregion
    address_add_update_Object.addressRadioChange();
    //alert($("#acc_id").val());
    address_add_update_Object.address_valite_c($("#acc_id").val());
}
//#endregion

//#region 订单成功页面
var makeorder_Fun = function () {
    var make_oid = LS.get("make_ocode") || "no_c";
    var make_total_order = LS.get("make_total_order") || "no_c";
    var make_paytype = LS.get("make_paytype") || "no_c";
    var make_logisticstype = LS.get("make_logisticstype") || "no_c";
    var make_posttimetype = LS.get("make_posttimetype") || "no_c";
    var make_Nub = function (canstring, cansb) {
        if (cansb === "no_c") {
            $("#" + canstring).hide().text("");
            $("#" + canstring).prev().hide();
            $("#" + canstring).parent().hide();
        } else {
            if (canstring === "make_total_order") { $("#" + canstring).show().text(parseFloat(cansb).toFixed(2) + "元"); } else {
                $("#" + canstring).show().text(cansb);
            }
        }
    }

    make_Nub("make_oid", make_oid); //订单号
    make_Nub("make_total_order", make_total_order); //应付金额
    make_Nub("make_paytype", make_paytype); //支付方式
    make_Nub("make_logisticstype", make_logisticstype); //配送方式
    make_Nub("make_posttimetype", make_posttimetype); //送货时间

    if (make_paytype === "货到付款") {
        $("#online_pay").css("display", "none");
    }
    $("#checkOrderDetail").click(function () {
        window.location.href = window.WebRoot + "CheckOut/orderdetail.aspx?ocode=" + make_oid;
    });
}
//#endregion

//#region 查看订单详细页
function orderdetail_Fun() {
    var ocode = getParameter('ocode') || LS.get("make_ocode") || "no_c";
    //alert(ocode);
    if (ocode != "no_c") {
        //#region 获取订单用户信息
        var orderdetail_template_container = $("#orderdetail_template_container");
        GetWcf({
            _api: "Order.get_order_info",
            _url: ocode
        }, function (jsonString) {
            if (jsonString.status == 1 && typeof (jsonString.info) == "object" && jsonString.info != null) {
                if (jsonString.info.paystatusid === 0) {//未付款
                    $(".zaixian").css("display", "inline-block");
                }
                if (jsonString.info.statusid == 0 || jsonString.info.statusid == 1) {
                    $(".cancels_btn").css("display", "inline-block");
                }
                orderdetail_template_container.setTemplate($('#orderdetail_template').html());
                orderdetail_template_container.processTemplate(jsonString.info, null, { append: false });
                orderdetail_template_container.listview('refresh');
            } else {
                orderdetail_template_container.setTemplate("<li>暂无用户信息</li>");
                orderdetail_template_container.processTemplate(jsonString.info, null, { append: false });
                orderdetail_template_container.listview('refresh');
            }
        }, true, {
            "ref_loading": orderdetail_template_container, "ref_loading_text": '<li style="text-align:center; background:url(../images/loading.gif) no-repeat center center; height:80px;"></li>'
        });
        //#endregion

        //#region 获取订单商品信息
        var ordergoods_list_template_container = $("#ordergoods_list_template_container");
        GetWcf({
            _api: "Order.get_ordergoods_list",
            _url: ocode
        }, function (jsonString) {
            if (jsonString.status == 1 && typeof (jsonString.list) == "object" && jsonString.list != null) {
                for (var i = 0; i < jsonString.list.length; i++) {
                    jsonString.list[i].pic_url = jsonString.list[i].pic_url.toString().replace("{0}", "normal");
                }
                ordergoods_list_template_container.setTemplate($('#ordergoods_list_template').html());
                ordergoods_list_template_container.processTemplate(jsonString.list, null, { append: false });
                ordergoods_list_template_container.listview('refresh');
            } else {
                ordergoods_list_template_container.setTemplate("<li>赞无商品信息</li>");
                ordergoods_list_template_container.processTemplate(jsonString.info, null, { append: false });
                ordergoods_list_template_container.listview('refresh');
            }
        }, true, {
            "ref_loading": ordergoods_list_template_container, "ref_loading_text": '<li style="text-align:center; background:url(../images/loading.gif) no-repeat center center; height:80px;"></li>'
        });
        //#endregion
    }
}
//#endregion

//#region 近N个月
var nearly_N_month = function (n) {
    var currentDate = new Date();

    var year = currentDate.getFullYear();
    var month = currentDate.getMonth() + 1 - n;
    var day = currentDate.getDate();
    var hour = currentDate.getHours();
    var min = currentDate.getMinutes();

    return year + "-" + month + "-" + day + " " + hour + ":" + min + ":00";
}
//#endregion

//#region 绑定订单列表
var BindOrderlist = function (nc) {
    var begintime = nearly_N_month(nc).toString(), endtime = nearly_N_month(0).toString();
    var orderlist_template_container = $("#orderlist_template_container");
    GetWcf({
        _api: "Order.get_order_list",
        _url: "/" + begintime + "/" + endtime
    }, function (jsonString) {
        if (jsonString.status == 1 && typeof (jsonString.list) == "object" && jsonString.list != null) {
            for (var i = 0; i < jsonString.list.length; i++) {
                jsonString.list[i].created = timeDate(jsonString.list[i].created);
            }
            orderlist_template_container.setTemplate($('#orderlist_template').html());
            orderlist_template_container.processTemplate(jsonString.list, null, { append: false });
            orderlist_template_container.listview('refresh');
        } else {
            orderlist_template_container.setTemplate("<li>暂无用户信息</li>");
            orderlist_template_container.processTemplate(jsonString.list, null, { append: false });
            orderlist_template_container.listview('refresh');
        }
    }, true, {
        "ref_loading": orderlist_template_container, "ref_loading_text": '<li style="text-align:center; background:url(../images/loading.gif) no-repeat center center; height:80px;"></li>'
    });
}
//#endregion

//#region 菜单选中
var changNav = {
    cangeStyle: function (tabs) {
        $("#oneMonth,#twoMonth").removeClass("ui-btn-up-e").addClass("ui-btn-up-c");
        var tabs = tabs || LS.get("tabs_order");
        $("#" + tabs).removeClass("ui-btn-up-c").addClass("ui-btn-up-e");
    },
    cange: function (obj) {
        $("#oneMonth,#twoMonth").removeClass("ui-btn-up-e").addClass("ui-btn-up-c");
        $(obj).removeClass("ui-btn-up-c").addClass("ui-btn-up-e");

    },
    trige: function () {
        LS.set("tabs_order", "oneMonth");
        changNav.cangeStyle(LS.get("tabs_order"));
        $("#oneMonth,#twoMonth").bind("click", function () {
            var id = $(this).attr("id");
            changNav.cangeStyle(id);
            LS.set("tabs_order", id);
            if (id === "oneMonth") {
                BindOrderlist(1);
            } else {
                BindOrderlist(2);
            }
        });
    }
}
//#endregion

//#region 订单列表页
function orderlist_Fun() {
    BindOrderlist(1);
    changNav.trige();
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
        } else if (/shoppingcart.aspx/i.test(url)) {
            shoppingcart_Fun();
        } else if (/orderconfirm.aspx/i.test(url)) {
            orderconfirm_Fun();
            calculation_shipping_costs_Fun();
        } else if (/addresslist.aspx/i.test(url)) {
            addresslist_Fun();
        } else if (/paymentlist.aspx/i.test(url)) {
            paymentlist_Fun();
        } else if (/deliverylist.aspx/i.test(url)) {
            deliverylist_Fun();
        } else if (/invoice.aspx/i.test(url)) {
            invoice_Fun();
        } else if (/address_add.aspx/i.test(url)) {
            address_add_Fun();
        } else if (/address_edit.aspx/i.test(url)) {
            address_edit_Fun();
        } else if (/makeorder.aspx/i.test(url)) {
            makeorder_Fun();
        } else if (/orderdetail.aspx/i.test(url)) {
            orderdetail_Fun();
        } else if (/orderlist.aspx/i.test(url)) {
            orderlist_Fun();
        }
    }

    $('#gotop').tap(function () {
        $.mobile.silentScroll(10);
    });
});
