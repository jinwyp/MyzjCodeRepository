
define(function (require, exports, module) {

	exports.initalize = function(){

		var $ = require("jquery");
		var jQuery = $;
		var basejs = require("basejs");
		var camera = require("../jquery-plugin/camera")($);
		require("jquery.templates")($);
		
		var Backbone=require("backbone");
		var _=require("underscore");
				
		var viewContent=Backbone.View.extend({
			initialize: function () {
				this.render();
				basejs.RefreshPage();
			},	
			render: function () {
				basejs.PageChange(this.el,require("/templates/index.tpl"),{});
				return this;
			}
		});
		
		var uView = new viewContent({ el: $("#Index_Content") });
		
		
		//#region 首页动画
		//传递搜索key值
		$("#search-form").submit(function (e) {
			//if(e.keyCode==13){
			var keyval = $("#searchinput1").val();
			if (keyval !== "") {
				window.location.href = "/product/productlist.aspx?key=" + keyval;
			}
			return false;
			//}
		});
		//绑定动画图片
		var bind_Index_pic = function () {
			basejs.GetWcf({
				_api: "Cms.get_columndata_list",
				_url: "B-A1-A1/1/5"
			}, function (jsonString) {
	
				if (jsonString.status == 1 && typeof (jsonString.list) == "object") {
	
					if (jsonString.list.length > 0) {
						var sthH = '';
						for (var i = 0; i < jsonString.list.length; i++) {
							//jsonString.list[i].pic_url = jsonString.list[i].pic_url.toString().replace("http://img.muyingzhijia.com/product/{0}/", "http://m.muyingzhijia.me/");
							sthH += '<div data-src="' + jsonString.list[i].pic_url + '" data-link="/goodstopic.aspx?id=' + jsonString.list[i].id + '"></div>';
						}
	
						if ($.trim($('#foucsPic').html()).length == 0) {
							$('#foucsPic').empty().append(sthH);
							camera.camera();
							camera.cameraStop();
							camera.cameraPause();
							camera.cameraResume();
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
						}
					}
				} else {
					alert(jsonString.msg);
				}
			}, true, { "ref_loading_c": $('#loading_list'), "ref_loading_text_c": '<div style="text-align:center; background:url(../images/loading.gif) no-repeat center center; height:80px;"></div>' });
		} ();
		//产品推荐列表
		var columnlist_index = function () {
			basejs.GetWcf({
				_api: "Cms.get_columndata_list",
				_url: "B-A1-A2/1/5"
			}, function (jsonString) {
				if (jsonString.status == 1 && typeof (jsonString.list) == "object") {
					if (jsonString.list.length > 0) {
						$('#columnlistContent').setTemplate($('#columnlist_jTemplate').html());
						$('#columnlistContent').processTemplate(jsonString, null, { append: false });
						$("#columnlistContent").listview("refresh");
					} else {
						$('#columnlistContent').setTemplate("<li>暂时无公告列表！</li>");
						$('#columnlistContent').processTemplate(jsonString, null, { append: false });
						$("#columnlistContent").listview("refresh");
					}
				} else {
					alert(jsonString.msg);
				}
			}, true, { "ref_loading_c": $('#loading_list'), "ref_loading_text_c": '<div style="text-align:center; background:url(../images/loading.gif) no-repeat center center; height:80px;"></div>' });
		} ();
		//公告列表
		var noticelist_index = function () {
			basejs.GetWcf({
				_api: "Cms.get_notice_list",
				_url: "1/5"
			}, function (jsonString) {
				if (jsonString.status == 1 && typeof (jsonString.list) == "object") {
					if (jsonString.list.length > 0) {
						$('#noticelistContent').setTemplate($('#notice_jTemplate').html());
						$('#noticelistContent').processTemplate(jsonString, null, { append: false });
						$("#noticelistContent").listview("refresh");
					} else {
						$('#noticelistContent').setTemplate("<li>暂时无公告列表！</li>");
						$('#noticelistContent').processTemplate(jsonString, null, { append: false });
						$("#noticelistContent").listview("refresh");
					}
				} else {
					alert(jsonString.msg);
				}
			}, true, { "ref_loading_c": $('#loading_list'), "ref_loading_text_c": '<div style="text-align:center; background:url(../images/loading.gif) no-repeat center center; height:80px;"></div>' });
		} ();
		//#endregion
	
	};

});