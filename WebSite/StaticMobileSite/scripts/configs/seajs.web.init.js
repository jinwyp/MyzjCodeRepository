
/// <reference path="../seajs/sea.js" />

define(function (require, exports, module) {

    var $ = require("jquery");
    require("jqm")($);
    console.log($.mobile.pushStateEnabled);
    $.extend($.mobile, {
            defaultPageTransition: "slide", //转场默认效果，设置 NONE 为 没有转场动画
            loadingMessage: false, //页面加载显示的 文字，false 为不显示
            pageLoadErrorMessage: "数据异常，请重试...", //Ajax 加载出错显示信息
            autoInitializePage: true, //默认渲染 页面 控件，否则手动调用 $.mobile.initializePage()
            ajaxEnabled: true,
            linkBindingEnabled: false,
            pushStateEnabled: false
    });
    $(document).bind("mobileinit", function () {
        alert(0);
        //设置jquerymobile 默认设置
        $.extend($.mobile, {
            defaultPageTransition: "slide", //转场默认效果，设置 NONE 为 没有转场动画
            loadingMessage: false, //页面加载显示的 文字，false 为不显示
            pageLoadErrorMessage: "数据异常，请重试...", //Ajax 加载出错显示信息
            autoInitializePage: true, //默认渲染 页面 控件，否则手动调用 $.mobile.initializePage()
            ajaxEnabled: true,
            linkBindingEnabled: false,
            pushStateEnabled: false
        });
    });
    console.log($.mobile.pushStateEnabled);
    //$.mobile.initializePage();

    var pagejs = require("../pagejs/index");

});