/// <reference path="../seajs/sea.js" />

define(function(require, exports, module) {

	var $ = require("jquery");
	$.mobile = require("jqm");

	var Backbone = require("backbone");

	var Handlebars = require("handlebars");
	var _ = require("underscore");

	require("backbone-modelbinder");
	require("backbone-validation");
	require("backbone-routefilter");
	require("backbone-localstorage");
	var webconfig = require("webconfig");

	Backbone.Validation.configure({
		forceUpdate : true
	});

	var core = require("corejs");

	window.context = {
		router : {},
		views : {},
		models : {},
		collections : {}
	};

	window.$ = $;
	window.jQuery = $;
	window.Backbone = Backbone;
	window._ = _;
	window.Handlebars = Handlebars;
	window.Core = core;
	window.Config = webconfig;

	$.ajaxPrefilter(function(options, originalOptions, jqXhr) {

		var newApiRouter = [];

		if (options.url.indexOf("http://") > -1 && options.url.indexOf("->") > -1) {
		
			var urlSplit = (options.url || "").replace("http://", "").split("->");

			if (!_.isEmpty(urlSplit)) {
				var apiName = urlSplit[0];
				var apiParam = urlSplit[1];
				var apiRoute = Config.Restful[apiName];
				if (!_.isEmpty(apiRoute)) {
					newApiRouter.push(Config.WcfRoot);
					newApiRouter.push(apiRoute);
					newApiRouter.push(Core.WcfAuth());
					newApiRouter.push(apiParam);
					options.url = newApiRouter.join('');
				} else {
					Log.instance("wcfapi 错误").error(apiName).end();
				}
			}
		}

		return "jsonp";

	});

	$.extend($.mobile, {
		//defaultPageTransition: "slide", //转场默认效果，设置 NONE 为 没有转场动画
		loadingMessage : false, //页面加载显示的 文字，false 为不显示
		pageLoadErrorMessage : "数据异常，请重试...", //Ajax 加载出错显示信息
		autoInitializePage : false, //默认渲染 页面 控件，否则手动调用 $.mobile.initializePage()
		ajaxEnabled : false,
		hashListeningEnabled : false,
		linkBindingEnabled : false,
		pushStateEnabled : false
	});
	$.extend($.mobile.changePage.defaults, {
		allowSamePageTransition : true
	});

	$.mobile.initializePage();
	$(document.body).show(300);

	var appRouter = Backbone.Router.extend({
		initialize : function() {
			Backbone.history.start({
				pushState : false
			});
		},
		routes : {
			'' : 'index',
			'index' : 'index',
			'login' : 'login',
			'login/:url' : 'login',
			'register' : 'register'
		},
		before : function(route, name, args) {
			window.context["urlparams"] = args;
			var pageJsUrl = "../pagejs/controler/" + name;
			require.async(pageJsUrl, function(page) {
				page.initalize();
			});

			return false;
		},
		after : function(route, args) {
		}
	});

	window.context.router = new appRouter();

	//#region 自定义方法

	//路由自定义方法
	_.extend(Backbone.Router, {
		Redirect : function(url) {
			window.context.router.navigate(url, {
				trigger : true
			});
		}
	});

	//#endregion

});
