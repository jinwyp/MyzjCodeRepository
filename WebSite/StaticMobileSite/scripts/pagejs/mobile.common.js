﻿define(function (require, exports, module) {

    var $ = require("jquery").sub();
    require("cookie")($);

    exports.Debug = window.Debug = true;
    exports.Tasks = window.Tasks = {};
    exports.mobile = window.mobile = {};
    exports.mobile.pages = window.mobile.pages = {};

    //#region 日期函数

    //时间格式转时间戳
    var DateToTimePoke = function (datestr) {

        var result = false;
        var date = new Date(datestr);
        if (date instanceof Date) {
            if (typeof (date) == "object") {
                result = '\/Date(' + date.getTime() + "+0800" + ')\/';
            }
        }
        return result;
    };

    //#endregion

    //#region GUID
    var S4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    };
    var NewGuid = function (format) {
        var result = (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
        switch (format) {
            case "N":
                result = result.replace(/-/g, "");
                break;
            case "B":
                result = "{" + result + "}";
                break;
            case "P":
                result = "(" + result + ")";
                break;
            default:
                break;
        }
        return result;
    };
    /*
    function NewGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
    var n = Math.floor(Math.random() * 16.0).toString(16);
    guid += n;
    if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
    guid += "-";
    }
    return guid;
    }
    */
    //#endregion

    //#region 属性
    function MToken() {
        var token = $.cookie("m_token") || [];
        if (token.length == 0)
            return "null";
        else
            return token[0];
    };
    var MUid = function () {
        var uid = $.cookie("m_uid") || [];
        if (uid.length == 0)
            return "null";
        else
            return uid[0];
    };
    var MUserId = function () {
        var userId = $.cookie("m_user_id") || [];
        if (userId.length == 0)
            return "0";
        else
            return userId[0];
    };
    var MGuid = function () {
        var guid = $.cookie("m_guid") || [];
        if (guid.length == 0) {
            guid = NewGuid("N");
            $.cookie("m_guid", guid);
        } else
            guid = "null";
        return guid;
    };
    //#endregion

    //#region 公用函数

    //#region 先清后绑
    var Unbind_bind = function (objs, eventName, callback) {
        if (typeof callback == "function") {
            jQuery(objs).unbind(eventName).bind(eventName, callback);
        }
    }
    //#endregion

    //#region 跳转页面
    var Change_Url = window.Change_Url = function (diUrl) {
        //window.location.href = diUrl;
        //console.log("跳转页面跳转页面跳转页面跳转页面跳转页面跳转页面跳转页面跳转页面跳转页面跳转页面");
        $.mobile.changePage(diUrl); //, { transition: "slide" }, { reloadPage: true }
    }
    //#endregion

    //#region 输出日志
    var Log = window.Log = function (logstr) {
        if (Debug) {
            if (typeof (window.console) == "object")
                window.console.log(logstr);
            else
                alert(logstr);
        }
    };
    //#endregion

    //#region 获取url中的参数
    var getParameter = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return r[2]; return null;
    }
    //#endregion

    //#region 时间转换
    var WcfDateToJsDate = function (wcfDate) {
        if (typeof wcfDate == 'string' && wcfDate.length > 5) {
            var date = new Date(parseInt(wcfDate.substring(6)));
            return date;
        } else return wcfDate;
    };
    var JsDateToWcfDate = function (jsDate) {
        // \/Date(568310400000+0800)\/
        return "\/Date(" + jsDate.getTime() + "+0000)\/";
    };
    var Dateformat = function (date, format) {
        var o = {

            "M+": date.getMonth() + 1, //month  
            "d+": date.getDate(), //day  
            "h+": date.getHours(), //hour  
            "m+": date.getMinutes(), //minute  
            "s+": date.getSeconds(), //second  
            "q+": Math.floor((date.getMonth() + 3) / 3), //quarter  
            "S": date.getMilliseconds() //millisecond  
        }

        if (/(y+)/.test(format))
            format = format.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(format))
                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        return format;
    }

    var timeDate = function (obj) {
        return Dateformat(WcfDateToJsDate(obj), "yyyy/MM/dd hh:mm:ss");
    }
    //#endregion

    //#region  验证用户Cookie 信息（登录信息）
    var VerifyUserCookie = function () {
        var token = $.cookie("m_token");
        var uid = $.cookie("m_uid");
        var lastUpdateTime = $.cookie("m_lutime");
        var md5 = $.cookie("m_md5");
        var nMd5 = hex_md5(uid + token + lastUpdateTime);
        if (md5 == nMd5) {
            var luTime = lastUpdateTime;
            var toDayTime = new Date().getTime();
            if (((toDayTime - luTime) / 1000 / 60) > 1) {
                if (uid.length > 0 && token.length > 0) {
                    ReLogin(uid, token);
                } else LinkToLogin();
            }
        } else LinkToLogin();
    };
    VerifyUserCookie.loading = false;
    //#endregion

    //#region 重新登录 通过Token
    var ReLogin = function (uid, token) {
        if (VerifyUserCookie.loading == false) {
            VerifyUserCookie.loading = true;
            GetWcf({
                _api: "Member.ReLogin",
                _url: "/" + uid + "/" + token
            }, function (json) {
                if (typeof (json) == "object" && json != null) {
                    if (json.state == 1 && typeof (json.data) == "string" && json.data.length > 0) {
                        UpdateLoginCookie(uid, json.data);
                        VerifyUserCookie.loading = false;
                    } else
                        LinkToLogin();
                } else
                    Log("获取数据失败");
            });
        }
    };
    //#endregion

    //#region localStorage
    var LS = {
        set: function (key, value) {
            if (this.get(key) != null) {
                this.remove(key);
            }
            localStorage.setItem(key, value);
        },
        get: function (key) {
            var v = localStorage.getItem(key);
            return v === undefined ? null : v;
        },
        remove: function (key) {
            localStorage.removeItem(key);
        },
        clear: function () { localStorage.clear(); }
    };
    //#endregion

    //#region 更新（写入）用户信息到Cookie（登录）
    var UpdateLoginCookie = function (user_id, uid, token) {
        $.cookie("m_user_id", user_id);
        $.cookie("m_token", token);
        $.cookie("m_uid", uid);
        var lastUpdateTime = new Date().getTime();
        $.cookie("m_lutime", lastUpdateTime);
        var md5 = hex_md5(user_id + uid + token + lastUpdateTime);
        $.cookie("m_md5", md5);
    };
    //#endregion

    //#region 清除用户信息（退出）
    var RemoveLoginCookie = function () {
        $.cookie("m_user_id", null);
        $.cookie("m_token", null);
        $.cookie("m_uid", null);
        $.cookie("m_lutime", null);
        $.cookie("m_md5", null);
        LS.clear();
        Get_shoppingcartgoodsnum_Fun();
    };
    //#endregion

    //#region 跳转到登录页面
    var LinkToLogin = function () {

        var url = window.location.href;
        if (!/login.aspx|registration.aspx/i.test(url)) {
            RemoveLoginCookie();
            window.location.href = window.WebRoot + "login.aspx";
        }

    };
    //#endregion

    //#region 获取url参数的值
    var GetUrlParam = function (pName) {

        if (typeof (window.location.paramobj) == 'undefined') {
            var paramobj = {};
            var urlSplit = window.location.href.split('?');
            if (urlSplit.length > 1) {
                var searchSplit = urlSplit[1].split('&');
                for (var i = 0; i < searchSplit.length; i++) {
                    var paramSplit = searchSplit[i].split('=');
                    var key = paramSplit[0];
                    var val = paramSplit[1];
                    paramobj[key] = val;
                }
            }
            Log("地址解析后的对象");
            Log(paramobj);

            window.location.paramobj = paramobj;
        }

        if (pName != null && pName.length > 0) {
            return decodeURIComponent(window.location.paramobj[pName]);
        }
    };
    //#endregion

    //#region string format
    String.prototype.format = function (arg) {
        var str = this || "";
        for (var a = 0; a < arg.length; a++) {
            var reg = new RegExp("\\{" + a + "\\}", "g");
            str = str.replace(reg, arg[a]);
        }
        return str;
    };
    //#endregion

    //#endregion

    //#region Wcf 调用

    var GetWcf = function (data, callback, showLoading, options) {

        CallWcf("GET", data, callback, showLoading, options);

    };

    var PostWcf = function (data, callback, showLoading, options) {

        CallWcf("POST", data, callback, showLoading, options);

    };
    //var WcfProxyUrl = "/Api/RequestProxy.ashx";
    //var WcfProxyUrl = "/Api/WebApi.ashx";
    /*
    调用Wcf资源

    Copyright 2012, Zhang Bin 
    Date:2012.7.4

    params:
    method:"GET|POST"               
    data:{
    _api    :"apiName",         
    _params :"/Member/Read/xxx",
    _data   :"{key:value} | key=value",                 POST 必须传入
    }
    callback:<function(result)>

    */
    var CallWcf = function (method, data, callback, showLoading, options) {

        if (typeof (method) == "string" && method.length > 0
        && typeof (data) == "object"
            && typeof (callback) == "function") {

            data._type = method;
            if (typeof (data._data) == "string" && data._data.length > 0) {
                try {
                    JSON.parse(data._data);
                    data._contentType = "application/json; charset=UTF-8";
                } catch (e) {

                }
            }

            var webconfig = require("webconfig");
            //WebSite/token/guid/654/uid
            var RequestUrl = [webconfig.WcfRoot, webconfig.Restful[data._api], webconfig.SystemType, MToken(), MGuid(), MUserId(), MUid(), data._url, ].join("/");

            var OPTIONS = {
                url: RequestUrl,
                data: data,
                type: method,
                dataType: 'jsonp',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                headers: {},
                //jsonp: "callback",
                //jsonpCallback: "jqeurycallback",
                beforeSend: function (XMLHttpRequest) {
                    if (showLoading) {
                        //alert(typeof (options)); //undefined
                        if (typeof (options) != "undefined") {
                            if (typeof (options.ref_loading) == "object" && options.ref_loading.length > 0) {
                                options.ref_loading.html(options.ref_loading_text);
                            }
                            if (typeof (options.ref_loading_c) == "object" && options.ref_loading_c.length > 0) {
                                options.ref_loading_c.show();
                                options.ref_loading_c.html(options.ref_loading_text_c);
                            }
                        } else {
                            //$.mobile.showPageLoadingMsg();
                            $.mobile.loadingMessageTextVisible = false;
                            $.mobile.loadingMessageTheme = 'c';
                            $.mobile.showPageLoadingMsg();
                        }
                    }
                },
                success: function (json) {
                    if (typeof (callback) == "function")
                        callback(json);
                    Log("ajax 返回结果：");
                    Log(json);
                },
                complete: function (request, textStatus) {
                    if (showLoading)
                        if (typeof (options) != "undefined") {
                            if (typeof (options.ref_loading) == "object" && options.ref_loading.length > 0) {
                                //options.ref_loading.empty();
                            }
                            if (typeof (options.ref_loading_c) == "object" && options.ref_loading_c.length > 0) {
                                options.ref_loading_c.hide();
                            }
                        } else {
                            $.mobile.hidePageLoadingMsg();
                        }
                },
                error: function (jqXHR, status) {
                    //alert("网络异常，请稍后重试！");
                },
                statusCode: {
                    404: function () {
                        alert("网络异常，请稍后重试！");
                    }
                }
            };

            //var op = $.extend(true, OPTIONS, options);
            var op = $.extend({}, OPTIONS, options);
            Log("ajax 请求数据：");
            Log(op);

            $.ajax(op);
        }

    };

    //#endregion

    //#region 获取购物车数量
    var Get_shoppingcartgoodsnum_Fun = function () {
        var token = $.cookie("m_token");
        var uid = $.cookie("m_uid");
        var user_id = $.cookie("m_user_id");
        GetWcf({
            _api: "Goods.shoppingcartgoodsnum"
        }, function (json) {
            if (json.status == 1 && typeof (json.info) == "object") {
                if (json.info.goods_count > 0) {
                    $(".Good_Total_Count").css("display", "block").text(json.info.goods_count);
                    $(".Hid_Good_Total_price").val(json.info.goods_total);
                } else {
                    $(".Good_Total_Count").text("").css("display", "none");
                }
            } else {
                Log(json.msg);
            }
        }, false, {
            async: false
        });
    };
    //#endregion

    //#region 检查用户 Token
    var checkUserToken = function () {

        //#region 暂时不用 定时检查cookie与Wcf服务端的一致性和更新状态

        if (typeof (window.Tasks.CheckCookie) != "undefined")
            clearInterval(window.Tasks.CheckCookie);
        window.Tasks.CheckCookie = setInterval(function () {
            if (VerifyUserCookie.loading == false) {
                VerifyUserCookie();
            }
        }, 1000);

        //#endregion
        VerifyUserCookie();
    };
    //#endregion

    exports.GetWcf = GetWcf;
    exports.PostWcf = PostWcf;

});