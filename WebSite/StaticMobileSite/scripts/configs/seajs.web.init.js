/// <reference path="../seajs/sea.js" />

define(function(require, exports, module) {

	var $ = require("jquery");
	$.mobile = require("jqm");

	var Backbone = require("backbone");

	var Handlebars = require("handlebars");
	Backbone.Validation = require('backbonevalidation');
	Backbone.ModelBinder = require('backbonemodelbinder');
	var _ = require("underscore");

	window.context = {};
	window.$ = $;
	window.jQuery = $;
	window.Backbone = Backbone;
	window._ = _;
	window.Handlebars = Handlebars;

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
		index : function() {
			window.context["urlparams"] = arguments;
			require.async("../pagejs/index", function(page) {
				page.initalize();
			});
		},
		login : function() {
			window.context["urlparams"] = arguments;
			require.async("../pagejs/login", function(page) {
				page.initalize();
			});
		},
		register : function() {
			window.context["urlparams"] = arguments;
			require.async("../pagejs/register", function(page) {
				page.initalize();
			});
		}
	});

	window.context["approuter"] = new appRouter();

});
